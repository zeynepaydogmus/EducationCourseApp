using EducationCourseApp.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace EducationCourse.Order.Infrastructure;

public class OrderDbContext: DbContext
{
    public const string DEFAULT_SCHEMA = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<EducationCourseApp.Order.Domain.OrderAggregate.Order> Orders { get; set; }
    public DbSet<EducationCourseApp.Order.Domain.OrderAggregate.OrderItem> OrdersItems { get; set; }
    //valueObject -> ownerType
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //configuration 
        modelBuilder.Entity<EducationCourseApp.Order.Domain.OrderAggregate.Order>().ToTable("Orders", DEFAULT_SCHEMA);
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
        modelBuilder.Entity<OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<EducationCourseApp.Order.Domain.OrderAggregate.Order>().OwnsOne(o => o.Address).WithOwner();
        base.OnModelCreating(modelBuilder);
    }
}