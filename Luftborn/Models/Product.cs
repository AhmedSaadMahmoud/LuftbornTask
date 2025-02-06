namespace Luftborn.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [Range(0.01, 100000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public byte[]? ProductImage { get; set; }

}