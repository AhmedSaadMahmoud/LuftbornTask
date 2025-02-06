

namespace Luftborn.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category)
                                      .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product, IFormFile? imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                product.ProductImage = memoryStream.ToArray();
            }
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product, IFormFile? imageFile)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null) return;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.CategoryId = product.CategoryId;

        if (imageFile != null && imageFile.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                existingProduct.ProductImage = memoryStream.ToArray();
            }
        }

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<byte[]?> GetProductImageAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product?.ProductImage;
    }
}
