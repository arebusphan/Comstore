namespace Data;
using Microsoft.EntityFrameworkCore;
public class ComStoreDbContext : DbContext
{
public ComStoreDbContext(DbContextOptions<ComStoreDbContext> options) : base(options)
{

}
public DbSet<User> Users {get; set;}
public DbSet<Product> Products {get; set;}
public DbSet<Category> Categories {get; set;}
public DbSet<Order> Orders {get; set;}
public DbSet<AICounseRequest> AICounseRequests {get; set;}
public DbSet<Review> Reviews {get; set;}
}
