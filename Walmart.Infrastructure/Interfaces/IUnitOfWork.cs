namespace Walmart.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUsers { get; }
        ICategoryRepository Categories { get; }
        ISubCategoryRepository SubCategories { get; }
        IProductRepository Products { get; }
        IProductPhotoRepository ProductPhotos { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        void Save();
    }
}