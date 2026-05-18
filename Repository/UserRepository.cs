namespace Repository;
using Data;
using Microsoft.EntityFrameworkCore;
public class UserRepository
{
    private readonly ComStoreDbContext _context;
public UserRepository(ComStoreDbContext context)
{
_context = context;
}
public async Task<User> Login(string email, string password)
{
var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
return user;
}
public async Task<User> Register(User user)
{
await _context.Users.AddAsync(user);
await _context.SaveChangesAsync();
return user;
}
public async Task<User> GetUserById(int id)
{
var user = await _context.Users.FindAsync(id);
return user;
}
}
