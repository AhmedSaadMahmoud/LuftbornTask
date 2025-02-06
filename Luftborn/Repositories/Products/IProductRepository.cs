public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product, IFormFile? imageFile);
    Task UpdateAsync(Product product, IFormFile? imageFile);
    Task DeleteAsync(int id);
    Task<byte[]?> GetProductImageAsync(int id);

}
