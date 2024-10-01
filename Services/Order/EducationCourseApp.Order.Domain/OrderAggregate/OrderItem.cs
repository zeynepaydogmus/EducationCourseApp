using EducationCourseApp.Order.Domain.Core;

namespace EducationCourseApp.Order.Domain.OrderAggregate;

public class OrderItem : Entity
{
    public OrderItem(string productId, string productName, string ımageUrl, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        ImageUrl = ımageUrl;
        Price = price;
    }

    public OrderItem()
    {
        
    }
    public void UpdateOrderItem(string productId, string productName, string imageUrl, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        ImageUrl = imageUrl;
        Price = price;
    }

    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ImageUrl { get; private set; }
    public decimal Price { get; private set; }
}