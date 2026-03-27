using System.Collections.Generic;
using System.Text;

public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _products = new List<Product>();
        _customer = customer;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public List<Product> GetProducts()
    {
        return _products;
    }

    public Customer GetCustomer()
    {
        return _customer;
    }

    public void SetCustomer(Customer customer)
    {
        _customer = customer;
    }

    public decimal CalculateTotalCost()
    {
        decimal productsTotal = 0m;

        foreach (Product product in _products)
        {
            productsTotal += product.GetTotalCost();
        }

        decimal shippingCost = _customer.LivesInUSA() ? 5m : 35m;
        return productsTotal + shippingCost;
    }

    public string GetPackingLabel()
    {
        StringBuilder builder = new StringBuilder();
        foreach (Product product in _products)
        {
            builder.AppendLine($"Product: {product.GetName()} | ID: {product.GetProductId()}");
        }

        return builder.ToString().TrimEnd();
    }

    public string GetShippingLabel()
    {
        return $"{_customer.GetName()}\n{_customer.GetAddress().GetFullAddress()}";
    }
}
