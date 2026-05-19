namespace Repository;
using Data;
using Microsoft.EntityFrameworkCore;
public class CategoryRepository
{
private readonly ComStoreDbContext _context;

public CategoryRepository(ComStoreDbContext context)
{
    _context = context;
}
public async Task<IEnumerable<Category>> GetAllCategories()
{
    return await _context.Categories.ToListAsync();
}
public async Task<Category> GetCategoryById(int id)
{
    return await _context.Categories.FindAsync(id);
}
public async Task<Category> AddCategory(Category category)
{
    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return category;
}
public async Task<Category> UpdateCategory(int id, Category category)
{
var existingCategory = await _context.Categories.FindAsync(id);
 _context.Update(existingCategory);
await _context.SaveChangesAsync();
return existingCategory;
}
public async Task<bool> DeleteCategory(int id)
{
var category = await _context.Categories.FindAsync(id);
 _context.Categories.Remove(category);
await _context.SaveChangesAsync();
return true;

}
}
