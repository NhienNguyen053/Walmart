#nullable disable
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.DtoModels;

namespace Walmart.ApplicationCore.ViewModels{
    public class OrderVM{
        public OrderVM()
        {
        }
        public OrderDto Data { get; set; }
        public IEnumerable<OrderDto> DataList { get; set; } = new List<OrderDto>();
        public int Total { get; set; }
    }
}