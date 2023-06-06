using Walmart.Infrastructure.Interfaces;
using Walmart.ApplicationCore.Models;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.DtoModels;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Walmart.Infrastructure.Data
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {

    // private new WalmartContext _context;

        public ApplicationUserRepository(WalmartContext context) : base(context)
        {
            // _context = context;
        }
    public async Task<UserVM> GetUsersByPaging(UserParam request) {
        var users = (from user in _context.ApplicationUsers
            join UserRole in _context.UserRoles on user.Id equals UserRole.UserId
            join role in _context.Roles on UserRole.RoleId equals role.Id
            where (String.IsNullOrEmpty(request.FullName) || user.FullName.Contains(request.FullName))
            && (String.IsNullOrEmpty(request.Role) || role.Name.Contains(request.Role))
            select new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                RoleName = role.Name,
            });
        var total = await users.CountAsync();
        var data = await users.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
        return new UserVM
        {
            DataList = data,
            Total = total
        };
    }
    public async Task<UserVM> GetUsersByPaging2(UserParam request, string rolename) {
        var users = (from user in _context.ApplicationUsers
            join UserRole in _context.UserRoles on user.Id equals UserRole.UserId
            join role in _context.Roles on UserRole.RoleId equals role.Id
            where (String.IsNullOrEmpty(request.FullName) || user.FullName.Contains(request.FullName))
            && (String.IsNullOrEmpty(request.Role) || role.Name.Contains(request.Role)) && role.Name == rolename
            select new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                RoleName = role.Name,
            });
        var total = await users.CountAsync();
        var data = await users.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
        return new UserVM
        {
            DataList = data,
            Total = total
        };
    }
    public string GetUserRole(string name){
        var userrole = (from role in _context.Roles where role.Name == name select role.Id).FirstOrDefault();
        return userrole;
    }
    public string CheckRole(string id){
        var userrole = (from role in _context.UserRoles where role.UserId == id select role.RoleId).FirstOrDefault();
        return userrole;
    }
    }
}