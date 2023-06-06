using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

using Walmart.Infrastructure.Interfaces;
using Walmart.Infrastructure.Data;
using Walmart.ApplicationCore.Models;

namespace Walmart.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalmartContext _context;
        public UnitOfWork(WalmartContext context)
        {
            _context = context;
            ApplicationUsers = new ApplicationUserRepository(_context);
            Categories = new CategoryRepository(_context);
            SubCategories = new SubCategoryRepository(_context);
            Products = new ProductRepository(_context);
            ProductPhotos = new ProductPhotoRepository(_context);
            ShoppingCarts = new ShoppingCartRepository(_context);
            Orders = new OrderRepository(_context);
            OrderDetails = new OrderDetailRepository(_context);
        }

        public IProductPhotoRepository ProductPhotos { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ISubCategoryRepository SubCategories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
        /// <summary>
        /// Here is where we commit a transaction.
        /// </summary>
        /// <returns></returns>

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
