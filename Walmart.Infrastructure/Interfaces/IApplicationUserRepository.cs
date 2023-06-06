using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;

namespace Walmart.Infrastructure.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<UserVM> GetUsersByPaging(UserParam request);
        Task<UserVM> GetUsersByPaging2(UserParam request, string rolename);
        string GetUserRole(string name);
        string CheckRole(string id);
    }
}