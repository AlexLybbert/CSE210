using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class WikiBibleService
{
    private static readonly string[] ScripturePages = { "The Bible", "Book of Mormon" };
    private const string WikiquoteApiUrl = "https://en.wikiquote.org/w/api.php?action=parse&page={0}&prop=wikitext&format=json";

    // Wikimedia requires a User-Agent header or requests get blocked
    private static readonly HttpClient _client = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(15),
        DefaultRequestHeaders = { { "User-Agent", "ScriptureMemorizer/1.0 (school project)" } }
    };

    private readonly Random _random = new();

    public async Task<Scripture> GetRandomScriptureAsync()
    {
        List<Scripture> all = new();

        foreach (string page in ScripturePages)
        {
            try
            {
                string url = string.Format(WikiquoteApiUrl, Uri.EscapeDataString(page));
                string json = await _client.GetStringAsync(url);
                all.AddRange(ParseScripturesFromJson(json));
            }
            catch
            {
                // skip pages we can't fetch
            }
        }

        // D&C and Pearl of Great Price don't have Wikiquote pages, so we include them directly
        all.AddRange(GetLdsScriptures());

        return all.Count > 0 ? all[_random.Next(all.Count)] : null;
    }

    // Curated well-known scriptures from Doctrine and Covenants and Pearl of Great Price
    private static List<Scripture> GetLdsScriptures()
    {
        return new List<Scripture>
        {
            new Scripture(new Reference("Doctrine and Covenants", 18, 10),
                "Remember the worth of souls is great in the sight of God; For, behold, the Lord your Redeemer suffered death in the flesh; wherefore he suffered the pain of all men, that all men might repent and come unto him."),
            new Scripture(new Reference("Doctrine and Covenants", 88, 118),
                "Seek ye diligently and teach one another words of wisdom; yea, seek ye out of the best books words of wisdom; seek learning, even by study and also by faith."),
            new Scripture(new Reference("Doctrine and Covenants", 130, 18),
                "Whatever principle of intelligence we attain unto in this life, it will rise with us in the resurrection."),
            new Scripture(new Reference("Doctrine and Covenants", 121, 7, 8),
                "My son, peace be unto thy soul; thine adversity and thine afflictions shall be but a small moment; And then, if thou endure it well, God shall exalt thee on high; thou shalt triumph over all thy foes."),
            new Scripture(new Reference("Doctrine and Covenants", 58, 27),
                "Verily I say, men should be anxiously engaged in a good cause, and do many things of their own free will, and bring to pass much righteousness."),
            new Scripture(new Reference("Moses", 1, 39),
                "For behold, this is my work and my glory—to bring to pass the immortality and eternal life of man."),
            new Scripture(new Reference("Articles of Faith", 1, 1),
                "We believe in God, the Eternal Father, and in His Son, Jesus Christ, and in the Holy Ghost."),
            new Scripture(new Reference("Articles of Faith", 1, 13),
                "We believe in being honest, true, chaste, benevolent, virtuous, and in doing good to all men; indeed, we may say that we follow the admonition of Paul—We believe all things, we hope all things, we have endured many things, and hope to be able to endure all things."),
            new Scripture(new Reference("Abraham", 3, 22),
                "Now the Lord had shown unto me, Abraham, the intelligences that were organized before the world was; and among all these there were many of the noble and great ones."),
            new Scripture(new Reference("Moses", 7, 18),
                "And the Lord called his people Zion, because they were of one heart and one mind, and dwelt in righteousness; and there was no poor among them."),
        };
    }

    private static List<Scripture> ParseScripturesFromJson(string json)
    {
        using JsonDocument doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("parse", out JsonElement parseElement))
        {
            return new List<Scripture>();
        }
        if (!parseElement.TryGetProperty("wikitext", out JsonElement wikitextElement))
        {
            return new List<Scripture>();
        }
        if (!wikitextElement.TryGetProperty("*", out JsonElement textElement))
        {
            return new List<Scripture>();
        }

        string wikiText = textElement.GetString() ?? string.Empty;
        return ParseScripturesFromWikitext(wikiText);
    }

    private static List<Scripture> ParseScripturesFromWikitext(string wikiText)
    {
        List<Scripture> scriptures = new();
        string[] lines = wikiText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length - 1; i++)
        {
            string quoteLine = lines[i].Trim();
            if (!quoteLine.StartsWith("* "))
            {
                continue;
            }
            if (quoteLine.StartsWith("** "))
            {
                continue;
            }

            string referenceLine = lines[i + 1].Trim();
            bool looksLikeReferenceLine = referenceLine.StartsWith("** ") ||
                                          referenceLine.StartsWith("*** ") ||
                                          referenceLine.StartsWith(":* ");
            if (!looksLikeReferenceLine)
            {
                continue;
            }

            string quoteText = RemoveWikiMarkup(quoteLine.Substring(2));
            string referenceText = RemoveWikiMarkup(referenceLine.TrimStart('*', ':', ' '));

            if (quoteText.Length < 25)
            {
                continue;
            }

            if (TryParseReference(referenceText, out Reference reference) && quoteText.Length > 0)
            {
                scriptures.Add(new Scripture(reference, quoteText));
            }
        }

        return scriptures;
    }

    private static bool TryParseReference(string input, out Reference reference)
    {
        reference = null;

        string normalizedInput = input
            .Replace("Book of ", "")
            .Replace("Gospel of ", "")
            .Replace("Epistle to the ", "")
            .Replace("Epistle of ", "")
            .Trim();

        Match match = Regex.Match(
            normalizedInput,
            @"(?<book>(?:[1-3]\s+)?[A-Za-z]+(?:\s+[A-Za-z]+)*)\s+(?<chapter>\d+):(?<start>\d+)(?:-(?<end>\d+))?");

        if (!match.Success)
        {
            return false;
        }

        string book = match.Groups["book"].Value.Trim();

        int chapter = int.Parse(match.Groups["chapter"].Value);
        int start = int.Parse(match.Groups["start"].Value);

        if (match.Groups["end"].Success)
        {
            int end = int.Parse(match.Groups["end"].Value);
            reference = new Reference(book, chapter, start, end);
            return true;
        }

        reference = new Reference(book, chapter, start);
        return true;
    }

    private static string RemoveWikiMarkup(string input)
    {
        string text = input;
        text = Regex.Replace(text, @"\[https?://[^\s\]]+\s+([^\]]+)\]", "$1");
        text = Regex.Replace(text, @"\{\{[^{}]*\}\}", " ");
        text = Regex.Replace(text, @"\[\[(?:[^\]|]*\|)?([^\]]+)\]\]", "$1");
        text = Regex.Replace(text, @"<[^>]+>", " ");
        text = text.Replace("'''", " ").Replace("''", " ");
        text = WebUtility.HtmlDecode(text);
        text = Regex.Replace(text, @"\s+", " ").Trim();
        return text;
    }
}
