namespace Repository;
using Data;
using Microsoft.EntityFrameworkCore;
public class OrderRepository
{
private readonly ComStoreDbContext _context;

public OrderRepository(ComStoreDbContext context)
{
    _context = context;
}
public async Task<Order> AddOrder(Order order)
{
    _context.Orders.Add(order);
    await _context.SaveChangesAsync();
    return order;
}
public async Task<Order> GetOrderById(int id)
{
    return await _context.Orders.FindAsync(id);
}
public async Task<IEnumerable<Order>> GetAllOrders()
{
    return await _context.Orders.ToListAsync();
}
public async Task<Order> UpdateOrder(int id, Order order){
var existingOrder = await GetOrderById(id);
_context.Update(existingOrder);
await _context.SaveChangesAsync();
return existingOrder;

}
public async Task<bool> DeleteOrder(int id)
{var order = await GetOrderById(id);
_context.Orders.Remove(order);
await _context.SaveChangesAsync();
return true;
}
}
