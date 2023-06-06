#nullable disable
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.DtoModels;

namespace Walmart.ApplicationCore.ViewModels{
    public class UserVM{
        public UserVM()
        {
        }
        public UserDto Data { get; set; }
        public IEnumerable<UserDto> DataList { get; set; } = new List<UserDto>();
        public int Total { get; set; }
    }
}