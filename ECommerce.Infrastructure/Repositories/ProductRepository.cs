using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Application.DTOs;
using ECommerce.Application.DTOs.ProductDtos;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedResponse<Product>> GetPagedAsync(ProductQueryRequest request)
    {
        var query = _context.Products.AsNoTracking();
        var search = request.Search?.Trim();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(product => product.Name.Contains(search));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == request.CategoryId.Value);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(product => product.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(product => product.Price <= request.MaxPrice.Value);
        }

        var totalCount = await query.CountAsync();
        var orderedQuery = request.SortBy switch
        {
            "priceAsc" => query.OrderBy(product => product.Price).ThenBy(product => product.Id),
            "priceDesc" => query.OrderByDescending(product => product.Price).ThenBy(product => product.Id),
            _ => query.OrderByDescending(product => product.CreatedAt).ThenBy(product => product.Id)
        };

        var offset = ((long)request.PageNumber - 1) * request.PageSize;
        var items = new List<Product>();
        if (offset < totalCount)
        {
            items = await orderedQuery
                .Include(product => product.Category)
                .Skip((int)offset)
                .Take(request.PageSize)
                .ToListAsync();
        }

        return new PagedResponse<Product>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public override async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
