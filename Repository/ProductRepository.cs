namespace Repository;
using Data;
using Microsoft.EntityFrameworkCore;
public class ProductRepository
{
private readonly ComStoreDbContext _context;

public ProductRepository(ComStoreDbContext context)
{
    _context = context;
}
public async Task<IEnumerable<Product>> GetAllProducts()
{
    return await _context.Products.ToListAsync();
}
public async Task<Product> GetProductById(int id)
{
    return await _context.Products.FindAsync(id);
}
public async Task<Product> AddProduct(Product product)
{
    _context.Products.Add(product);
    await _context.SaveChangesAsync();
    return product;
}
public async Task<Product> UpdateProduct(int id, Product product)
{
    var existingProduct = await GetProductById(id);
    _context.Update(existingProduct);
    await _context.SaveChangesAsync();
    return existingProduct;
}
public async Task<bool> DeleteProduct(int id)
{
    var product = await GetProductById(id);
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();
    return true;
}
public async Task<IEnumerable<Product>> CompareProducts(List<int> ids){
    var products = await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
    return products;
}
}

