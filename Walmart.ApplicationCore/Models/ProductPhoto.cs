using Microsoft.AspNetCore.Identity;

namespace Walmart.ApplicationCore.Models;

public class ProductPhoto
{
    public int ProductPhotoId { get; set; }
    public int Product_Id { get; set; }
    public int Sequence { get; set; }
    public string? Image_Path { get; set; }
    public Product? Product { get; set; }
}