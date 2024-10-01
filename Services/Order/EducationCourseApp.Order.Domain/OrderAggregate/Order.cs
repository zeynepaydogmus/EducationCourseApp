using System.Net.Sockets;
using EducationCourseApp.Order.Domain.Core;

namespace EducationCourseApp.Order.Domain.OrderAggregate;

public class Order :Entity,IAggregateRoot
{
    //Ef core features
    //--Owned Types
    //--Shadow Property
    //--Backing Field
    //not:bir aggregate bir entity kullanıyorsa başkası bu entityi kullanmamalı
    //domain hangi orm ile çalıştığını bilmemeli. herhangi bir kütüphaneye bağımlı olmamalı
    public DateTime CreatedTime { get; private set; }
    public Address Address { get; set; }
    public string  BuyerId { get; set; }
    //backing field
    private readonly List<OrderItem> _orderItems; 
    //encapsulation
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Order()
    {
        
    }
    public Order(string buyerId, Address address)
    {
        _orderItems = new List<OrderItem>();
        CreatedTime = DateTime.Now;
        BuyerId = buyerId;
        Address = address;
    }

    public void AddOrderItem(string productId, string productName, decimal price, string imageUrl)
    {
        //business
        var isExistProduct = _orderItems.Any(x => x.ProductId == productId);
        if (!isExistProduct)
        {
            var newOrderItem = new OrderItem(productId, productName, imageUrl, price);
            _orderItems.Add(newOrderItem);
        }
    }

    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
}