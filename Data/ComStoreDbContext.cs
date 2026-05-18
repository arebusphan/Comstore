namespace Data;
using Microsoft.EntityFrameworkCore;
public class ComStoreDbContext : DbContext
{
public ComStoreDbContext(DbContextOptions<ComStoreDbContext> options) : base(options)
{

}
public DbSet<User> Users {get; set;}
}
