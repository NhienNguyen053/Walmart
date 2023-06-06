using Microsoft.AspNetCore.Identity;
using Walmart.ApplicationCore.Models;

namespace Walmart.ApplicationCore.DtoModels;

public class MainPageDto
{
    public List<MainPageCategoriesDto>? MainPageCategoriesDtos { get; set; }
    public IEnumerable<ProductsWithPhotoDto>? ProductsWithPhotoDtos { get; set; }
}