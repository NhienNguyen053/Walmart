#nullable disable
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.DtoModels;

namespace Walmart.ApplicationCore.ViewModels{
    public class ProductVM{
        public ProductVM()
        {
        }
        public ProductDto Data { get; set; }
        public IEnumerable<ProductDto> DataList { get; set; } = new List<ProductDto>();
        public int Total { get; set; }
    }
}