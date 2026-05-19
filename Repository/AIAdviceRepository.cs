namespace Repository;
using Data;
using Microsoft.EntityFrameworkCore;
public class AIAdviceRepository
{
private readonly ComStoreDbContext _context;
public AIAdviceRepository(ComStoreDbContext context)
{
    _context = context;
}
public async Task<AICounseRequest> AddAICounseRequest(AICounseRequest request)
{
    _context.AICounseRequests.Add(request);
    await _context.SaveChangesAsync();
    return request;
}
public async Task<IEnumerable<AICounseRequest>> GetHistoryAICounseRequest(int userId)
{
    return await _context.AICounseRequests
    .Where(r => r.UserId == userId)
    .OrderByDescending(r => r.CreatedAt)
    .ToListAsync();
}
public async Task<IEnumerable<Product>> GetProductByRecommendation(string usageType, int budget, string preferences)
{
    return await _context.Products
    .Where(p => p.Description.Contains(usageType) && p.Price <= budget && p.Brand.Contains(preferences))
    .OrderByDescending(p => p.CreatedAt)
    .ToListAsync();
}
}