namespace Luftborn.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {

        context.Database.EnsureCreated();
        if (context.Categories.Any())
        { return; }

        var categories = new Category[]
        {
                new Category { Name = "Electronics" },
                new Category { Name = "Clothing" },
                new Category { Name = "Books" }
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();

        var products = new Product[]
        {
                new Product { Name = "HP Zbook Laptop", Price = 35000.0M, CategoryId = categories[0].Id },
                new Product { Name = "Samsung smart TV 55Inch", Price = 20000.0M, CategoryId = categories[0].Id },
                new Product { Name = "Apple IPhone 14 Pro 265GB Black", Price = 49000.0M, CategoryId = categories[0].Id },
                new Product { Name = "T-Shirt", Price = 350.0M, CategoryId = categories[1].Id },
                new Product { Name = "Adidas Shoes", Price = 3600.0M, CategoryId = categories[1].Id },
                new Product { Name = "C# Nutshell Book", Price = 150.0M, CategoryId = categories[2].Id },
                new Product { Name = "MS-SQL Server 2022 Book", Price = 150.0M, CategoryId = categories[2].Id }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}