using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

public static class Database
{
    public static IDbConnection GetConnection()
    {
        return new SqliteConnection("Data Source=journal.db");
    }

    private static string GetTableName<T>()
    {
        string name = typeof(T).Name;

        if (name.EndsWith("y") && !"aeiou".Contains(char.ToLower(name[^2])))
            return name[..^1] + "ies"; // e.g., "Category" -> "Categories"
        if (name.EndsWith("s") || name.EndsWith("x") || name.EndsWith("z") || name.EndsWith("ch") || name.EndsWith("sh"))
            return name + "es"; // e.g., "Box" -> "Boxes"

        return name + "s"; // default: just add "s"
    }

    public static void AddEntity<T>(T entity)
    {
        using var connection = GetConnection();
        string tableName = GetTableName<T>();

        var props = typeof(T).GetProperties()
        .Where(x => x.Name != "_id")
        .ToList();

        var columns = string.Join(", ", props.Select(x => x.Name));
        var parameters = string.Join(", ", props.Select(x => "@" + x.Name));

        string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

        connection.Execute(sql, entity);
    }

    public static List<T> GetAll<T>()
    {
        using var connection = GetConnection();
        string tableName = GetTableName<T>();
        return connection.Query<T>($"SELECT * FROM {tableName}").ToList();
    }

    public static void DeleteRecent<T>()
    {
        using var connection = GetConnection();
        string tableName = GetTableName<T>();
        connection.Execute($"DELETE FROM {tableName} WHERE _id = (SELECT MAX(_id) FROM {tableName})");
    }

    public static void DatabaseInit()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Entries (
                        _id INTEGER PRIMARY KEY AUTOINCREMENT,
                        _date TEXT NOT NULL,
                        _promptText TEXT NOT NULL,
                        _entryText TEXT NOT NULL
                    );
                ";
                command.ExecuteNonQuery();
            }
        }
    }
}