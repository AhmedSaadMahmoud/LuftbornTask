namespace Luftborn.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ApplicationDbContext _context;

    public ProductController(IProductRepository productRepository, ApplicationDbContext context)
    {
        _productRepository = productRepository;
        _context = context;
    }

    // GET: Product
    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();
        return View(products);
    }

    // GET: Product/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    // POST: Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId")] Product product, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            await _productRepository.AddAsync(product, imageFile);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // GET: Product/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound($"Product with ID {id} not found.");

        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    // POST: Product/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId")] Product product, IFormFile? imageFile)
    {
        if (id != product.Id) return NotFound($"Product with ID {id} not found.");

        if (ModelState.IsValid)
        {
            await _productRepository.UpdateAsync(product, imageFile);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Product/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // POST: Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> GetImage(int id)
    {
        var imageData = await _productRepository.GetProductImageAsync(id);
        if (imageData == null)
            return NotFound();

        return File(imageData, "image/png"); // Change MIME type as needed
    }
}
