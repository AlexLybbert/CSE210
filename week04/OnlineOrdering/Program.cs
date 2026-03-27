using System;

class Program
{
    static void Main(string[] args)
    {
        Customer usaCustomer = new Customer(
            "Alex Johnson",
            new Address("123 Main St", "Boise", "ID", "USA")
        );

        Order usaOrder = new Order(usaCustomer);
        usaOrder.AddProduct(new Product("Wireless Mouse", "WM-100", 24.99m, 1));
        usaOrder.AddProduct(new Product("USB-C Cable", "UC-220", 9.50m, 2));
        usaOrder.AddProduct(new Product("Keyboard", "KB-310", 45.00m, 1));

        Customer internationalCustomer = new Customer(
            "Sofia Martinez",
            new Address("45 Queen St", "Toronto", "ON", "Canada")
        );

        Order internationalOrder = new Order(internationalCustomer);
        internationalOrder.AddProduct(new Product("Notebook", "NB-010", 4.25m, 5));
        internationalOrder.AddProduct(new Product("Pen Set", "PS-011", 7.75m, 2));

        DisplayOrderDetails("ORDER 1", usaOrder);
        DisplayOrderDetails("ORDER 2", internationalOrder);
    }

    static void DisplayOrderDetails(string title, Order order)
    {
        Console.WriteLine($"===== {title} =====");
        Console.WriteLine("PACKING LABEL:");
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine();

        Console.WriteLine("SHIPPING LABEL:");
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine();

        Console.WriteLine($"TOTAL PRICE: ${order.CalculateTotalCost():0.00}");
        Console.WriteLine();
    }
}