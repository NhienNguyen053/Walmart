using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;
using static Walmart.ApplicationCore.Models.Product;

namespace Walmart.ApplicationCore.DtoModels;

public class ProductDetailsDto
{
    public Product? product { get; set; }
    public Category? category { get; set; }
    public IEnumerable<ProductPhoto>? productPhotos { get; set; }
    public List<ProductsWithPhotoDto>? RelatedProducts { get; set; }
}