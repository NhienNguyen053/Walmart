using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Walmart.ApplicationCore.DtoModels;
using Walmart.ApplicationCore.Models;
using Walmart.Infrastructure.Data;
using Walmart.Infrastructure.Interfaces;
using Walmart.Web.Models;

namespace Walmart.Web.Controllers;
[Area("Customer")]
public class HomeController : BaseController
{
    private readonly WalmartContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(IUnitOfWork uow, WalmartContext context, SignInManager<ApplicationUser> signInManager) : base(uow)
        {
            _signInManager = signInManager;
            _context = context;
        }
    
    public static List<MainPageCategoriesDto> GetMainPageCategories(IUnitOfWork uow)
    {
    var categoryDto = new List<MainPageCategoriesDto>();
    var categories = uow.Categories.GetAll();
    foreach (var category in categories)
    {
        var subcategories = uow.SubCategories.GetAll(s => s.Category_Id == category.CategoryId);
        var categoryDtoItem = new MainPageCategoriesDto
        {
            CategoryId = category.CategoryId,
            Category_Name = category.Category_Name,
            Category_Image = category.Category_Image,
            SubCategories = subcategories
        };
        categoryDto.Add(categoryDtoItem);
    }
    return categoryDto;
    }

    public IActionResult Index()
    {
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        var products = _uow.Products.GetProductsWithPhoto();
        IEnumerable<ProductsWithPhotoDto> productsWithPhotoDtos = products;
        return View(productsWithPhotoDtos);
    }

    public IActionResult Enter_Email()
    {
        return View();
    }

[HttpPost]
    public IActionResult CheckEmail(string email)
    {
        bool dataExists = _context.ApplicationUsers.Any(u => u.Email == email);
         var cookieOptions = new CookieOptions
        {
        Expires = DateTime.Now.AddDays(1) // Set cookie expiration time
        };
        Response.Cookies.Append("UserEmail", email, cookieOptions);

        if (dataExists)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        else
        {
            return RedirectToPage("/Account/Register", new { area = "Identity" });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult ProductDetails(int id){
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        ProductDetailsDto productDetailsDto = new ProductDetailsDto();
        productDetailsDto.product = _uow.Products.Get(id);
        IEnumerable<Product> products = _uow.Products.GetAll(p => p.SubCategory_Id == productDetailsDto.product.SubCategory_Id && p.ProductId != productDetailsDto.product.ProductId).Take(4);
        List<ProductsWithPhotoDto> productsWithPhotoDtos = new List<ProductsWithPhotoDto>();
        foreach(var product in products){
            var photo = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault();
            var relatedproducts = new ProductsWithPhotoDto
            {
                ProductId = product.ProductId,
                Product_Name = product.Product_Name,
                Product_Price = product.Product_Price,
                Product_Description = product.Product_Description,
                ProductPhotoId = photo!.ProductPhotoId,
                Image_Path = photo.Image_Path
            };
            productsWithPhotoDtos.Add(relatedproducts);
        }
        productDetailsDto.RelatedProducts = productsWithPhotoDtos;
        SubCategory subCategory = _uow.SubCategories.Get(productDetailsDto.product.SubCategory_Id);
        productDetailsDto.category = _uow.Categories.Get(subCategory.Category_Id);
        productDetailsDto.productPhotos = _uow.ProductPhotos.GetAll(p => p.Product_Id == productDetailsDto.product.ProductId);
        return View(productDetailsDto);
    }
    public IActionResult AddtoCart(int productid){
        int count = 0;
        decimal total = 0;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        bool dataExists = _context.ShoppingCarts.Any(p => p.ProductId == productid && p.UserId == userid);
        if(dataExists){
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart = _uow.ShoppingCarts.GetAll(p => p.ProductId == productid && p.UserId == userid).FirstOrDefault()!;
            shoppingCart!.Product_Quantity = shoppingCart.Product_Quantity + 1;
            _uow.ShoppingCarts.Update(shoppingCart);
            _uow.Save();
            var items = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in items)
            {
                count = count + item.Product_Quantity;
            }
            var items2 = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in items2)
            {
                var product = _uow.Products.Get(item.ProductId);
                total = total + (product.Product_Price * item.Product_Quantity);
            }
            CartItemDto cartItemDto = new CartItemDto();
            cartItemDto.items_count = count;
            cartItemDto.total_price = total;
            return new JsonResult(cartItemDto);
        }else{
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.UserId = userid;
            shoppingCart.ProductId = productid;
            shoppingCart.Product_Quantity = 1;
            _uow.ShoppingCarts.Add(shoppingCart);
            _uow.Save();
            var items = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in items)
            {
                count = count + item.Product_Quantity;
            }
            var items2 = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in items2)
            {
                var product = _uow.Products.Get(item.ProductId);
                total = total + (product.Product_Price * item.Product_Quantity);
            }
            CartItemDto cartItemDto = new CartItemDto();
            cartItemDto.items_count = count;
            cartItemDto.total_price = total;
            return new JsonResult(cartItemDto);
        }
    }
    public IActionResult CartDetails(){
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        List<CartDetailsDto> cartDetailsDtos = new List<CartDetailsDto>();
        int count = 0;
        decimal total = 0;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var products = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
        if(products == null){
            return View(cartDetailsDtos);
        }
        foreach (var cartitem in products)
        {
            count = count + cartitem.Product_Quantity;
            var product = _uow.Products.Get(cartitem.ProductId);
            var image = _uow.ProductPhotos.GetAll(p => p.Product_Id == cartitem.ProductId).FirstOrDefault();
            total = total + (product.Product_Price * cartitem.Product_Quantity);
            var p = new CartDetailsDto{
                Product_Id = cartitem.ProductId,
                Product_Name = product.Product_Name,
                Product_Price = product.Product_Price,
                Product_Quantity = cartitem.Product_Quantity,
                Image_Path = image!.Image_Path,
                Total = (product.Product_Price * cartitem.Product_Quantity)
            };
            cartDetailsDtos.Add(p);
        }
        ViewData["Count"] = count;
        ViewData["Total"] = total;
        return View(cartDetailsDtos);
    }
    public IActionResult PlusCart(int cartid){
        decimal total = 0;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        ShoppingCart oldcart = _uow.ShoppingCarts.GetAll(p => p.UserId == userid && p.ProductId == cartid).FirstOrDefault()!;
        oldcart.Product_Quantity = oldcart.Product_Quantity + 1;
        _uow.ShoppingCarts.Update(oldcart);
        _uow.Save();
        UpdateCartDto updateCartDto = new UpdateCartDto();
        updateCartDto.Product_Id = cartid;
        updateCartDto.Product_Quantity = oldcart.Product_Quantity;
        Product product = new Product();
        product = _uow.Products.Get(cartid);
        updateCartDto.Product_Price = product.Product_Price;
        var products = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
        foreach (var item in products)
        {
            var price = _uow.Products.Get(item.ProductId);
            total = total + (price.Product_Price * item.Product_Quantity);
        }
        updateCartDto.Total = total;
        return new JsonResult(updateCartDto);
    }
    public IActionResult MinusCart(int cartid){
        decimal total = 0;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        ShoppingCart oldcart = _uow.ShoppingCarts.GetAll(p => p.UserId == userid && p.ProductId == cartid).FirstOrDefault()!;
        if(oldcart.Product_Quantity == 1){
            _uow.ShoppingCarts.Remove(oldcart);
            _uow.Save();
            UpdateCartDto updateCartDto = new UpdateCartDto();
            updateCartDto.Product_Id = cartid;
            updateCartDto.Product_Quantity = oldcart.Product_Quantity;
            Product product = new Product();
            product = _uow.Products.Get(cartid);
            updateCartDto.Product_Price = product.Product_Price;
            var products = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in products)
            {
                var price = _uow.Products.Get(item.ProductId);
                total = total + (price.Product_Price * item.Product_Quantity);
            }   
            updateCartDto.Total = total;
            var status = _uow.ShoppingCarts.GetAll(p => p.UserId == userid).FirstOrDefault();
            if(status == null){
                updateCartDto.Cart_Status = "Empty";
            }else{
                updateCartDto.Cart_Status = "NotEmpty";
            }
            return new JsonResult(updateCartDto);
        }else{
            oldcart.Product_Quantity = oldcart.Product_Quantity - 1;
            _uow.ShoppingCarts.Update(oldcart);
            _uow.Save();
            UpdateCartDto updateCartDto = new UpdateCartDto();
            updateCartDto.Product_Id = cartid;
            updateCartDto.Product_Quantity = oldcart.Product_Quantity;
            Product product = new Product();
            product = _uow.Products.Get(cartid);
            updateCartDto.Product_Price = product.Product_Price;
            var products = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in products)
            {
                var price = _uow.Products.Get(item.ProductId);
                total = total + (price.Product_Price * item.Product_Quantity);
            }   
            updateCartDto.Total = total;
            var status = _uow.ShoppingCarts.GetAll(p => p.UserId == userid).FirstOrDefault();
            if(status == null){
                updateCartDto.Cart_Status = "Empty";
            }else{
                updateCartDto.Cart_Status = "NotEmpty";
            }
            return new JsonResult(updateCartDto);
        }
    }
    public IActionResult RemoveCart(int id, int quantity){
        decimal total = 0;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        ShoppingCart oldcart = _uow.ShoppingCarts.GetAll(p => p.UserId == userid && p.ProductId == id).FirstOrDefault()!;
        oldcart.UserId = userid;
        oldcart.ProductId = id;
        oldcart.Product_Quantity = quantity;
        _uow.ShoppingCarts.Remove(oldcart);
        _uow.Save();
        UpdateCartDto updateCartDto = new UpdateCartDto();
        var status = _uow.ShoppingCarts.GetAll(p => p.UserId == userid).FirstOrDefault();
            if(status == null){
                updateCartDto.Cart_Status = "Empty";
            }else{
            updateCartDto.Product_Id = id;
            updateCartDto.Product_Quantity = oldcart.Product_Quantity;
            Product product = new Product();
            product = _uow.Products.Get(id);
            updateCartDto.Product_Price = product.Product_Price;
            var products = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
            foreach (var item in products)
            {
                var price = _uow.Products.Get(item.ProductId);
                total = total + (price.Product_Price * item.Product_Quantity);
            }   
            updateCartDto.Total = total;
            updateCartDto.Cart_Status = "NotEmpty";
            }
        return new JsonResult(updateCartDto);
    }
    public IActionResult CheckCart(){
        List<int> larger = new List<int>();
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var cartitems = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
        foreach (var item in cartitems)
        {
            var product = _uow.Products.Get(item.ProductId);
            if(item.Product_Quantity > product.Product_Quantity){
                larger.Add(item.ProductId);
            }
        }
        return new JsonResult(larger);
    }
    public IActionResult Checkout(){
        List<CartDetailsDto> cartDetailsDtos = new List<CartDetailsDto>();
        int count = 0;
        decimal total = 0;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        ViewData["User"] = user;
        var products = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
        foreach (var cartitem in products)
        {
            count = count + cartitem.Product_Quantity;
            var product = _uow.Products.Get(cartitem.ProductId);
            var image = _uow.ProductPhotos.GetAll(p => p.Product_Id == cartitem.ProductId).FirstOrDefault();
            total = total + (product.Product_Price * cartitem.Product_Quantity);
            var p = new CartDetailsDto{
                Product_Id = cartitem.ProductId,
                Product_Name = product.Product_Name,
                Product_Price = product.Product_Price,
                Product_Quantity = cartitem.Product_Quantity,
                Image_Path = image!.Image_Path,
                Total = (product.Product_Price * cartitem.Product_Quantity)
            };
            cartDetailsDtos.Add(p);
        }
        ViewData["Count"] = count;
        ViewData["Total"] = total;
        return View(cartDetailsDtos);
    }
    [HttpPost]
    public IActionResult CreateOrder(string fullname, string phonenumber, string address, string method, decimal totalprice){
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        bool status = true;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var cartitems = _uow.ShoppingCarts.GetAll(p => p.UserId == userid);
        foreach (var item in cartitems)
        {
            var product = _uow.Products.Get(item.ProductId);
            if(product.Product_Quantity < item.Product_Quantity){
                status = false;
                return View(status);
            }
        }
        Order order = new Order();
        order.Customer_Id = userid;
        order.Customer_Name = fullname;
        order.Phone_Number = phonenumber;
        order.Address = address;
        order.CreateDate = DateTime.Now;
        order.Order_Status = "Pending";
        order.Payment_Method = method;
        order.Total_Price = totalprice;
        _uow.Orders.Add(order);
        _uow.Save();
        foreach (var item in cartitems)
        {
            var product = _uow.Products.Get(item.ProductId);
            Product newproduct = new Product();
            newproduct = product;
            newproduct.Product_Quantity = newproduct.Product_Quantity - item.Product_Quantity;
            _uow.Products.Update(newproduct);
            _uow.Save();
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.Order_Id = order.OrderId;
            orderDetail.Product_Id = product.ProductId;
            orderDetail.Product_Name = product.Product_Name;
            orderDetail.Product_Price = product.Product_Price;
            orderDetail.Product_Quantity = item.Product_Quantity;
            orderDetail.Product_Image = _uow.ProductPhotos.GetAll(p => p.Product_Id == item.ProductId).FirstOrDefault()!.Image_Path;
            _uow.OrderDetails.Add(orderDetail);
            _uow.Save();
            _uow.ShoppingCarts.Remove(item);
            _uow.Save();
        }
        return View(status);
    }
    public IActionResult AllCategories(){
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        return View(categoryDto);
    }
    public IActionResult SearchProducts(int categoryid = 0, int subcategoryid = 0){
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        IEnumerable<Product> products = Enumerable.Empty<Product>();
        IEnumerable<ProductsWithPhotoDto> productsWithPhotoDtos = Enumerable.Empty<ProductsWithPhotoDto>();
        if(categoryid != 0){
            var subcategories = _uow.SubCategories.GetAll(p => p.Category_Id == categoryid);
            foreach (var subcategory in subcategories)
            {
                var subproducts = _uow.Products.GetAll(p => p.SubCategory_Id == subcategory.SubCategoryId && p.Product_Status == Product.ProductStatus.Active);
                products = products.Concat(subproducts);
            }
            foreach (var product in products)
            {
                ProductsWithPhotoDto productsWithPhotoDto = new ProductsWithPhotoDto();
                productsWithPhotoDto.ProductId = product.ProductId;
                productsWithPhotoDto.Product_Name = product.Product_Name;
                productsWithPhotoDto.Product_Price = product.Product_Price;
                productsWithPhotoDto.Product_Description = product.Product_Description;
                productsWithPhotoDto.ProductPhotoId = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault()!.ProductPhotoId;
                productsWithPhotoDto.Image_Path = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault()!.Image_Path;
                productsWithPhotoDtos = productsWithPhotoDtos.Concat(new[] { productsWithPhotoDto });
            }
            var cate = _uow.Categories.Get(categoryid);
            ViewData["Name"] = cate.Category_Name;
            ViewData["Method"] = "Category";
            return View(productsWithPhotoDtos);
        }else {
            Console.WriteLine(subcategoryid);
            products = _uow.Products.GetAll(p => p.SubCategory_Id == subcategoryid && p.Product_Status == Product.ProductStatus.Active);
            foreach (var product in products)
            {
                ProductsWithPhotoDto productsWithPhotoDto = new ProductsWithPhotoDto();
                productsWithPhotoDto.ProductId = product.ProductId;
                productsWithPhotoDto.Product_Name = product.Product_Name;
                productsWithPhotoDto.Product_Price = product.Product_Price;
                productsWithPhotoDto.Product_Description = product.Product_Description;
                productsWithPhotoDto.ProductPhotoId = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault()!.ProductPhotoId;
                productsWithPhotoDto.Image_Path = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault()!.Image_Path;
                productsWithPhotoDtos = productsWithPhotoDtos.Concat(new[] { productsWithPhotoDto });
            }
            var subcate = _uow.SubCategories.Get(subcategoryid);
            var cate = _uow.Categories.Get(subcate.Category_Id);
            ViewData["SubName"] = cate.Category_Name;
            ViewData["Name"] = subcate.SubCategory_Name;
            ViewData["Method"] = "SubCategory";
            return View(productsWithPhotoDtos);
    }
    }
    [HttpPost]
    public IActionResult SearchProductsbyName(string productname){
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        IEnumerable<Product> products = Enumerable.Empty<Product>();
        IEnumerable<ProductsWithPhotoDto> productsWithPhotoDtos = Enumerable.Empty<ProductsWithPhotoDto>();
        products = _uow.Products.GetAll(p => p.Product_Name!.Contains(productname) && p.Product_Status == Product.ProductStatus.Active);
        foreach (var product in products)
        {
            ProductsWithPhotoDto productsWithPhotoDto = new ProductsWithPhotoDto();
            productsWithPhotoDto.ProductId = product.ProductId;
            productsWithPhotoDto.Product_Name = product.Product_Name;
            productsWithPhotoDto.Product_Price = product.Product_Price;
            productsWithPhotoDto.Product_Description = product.Product_Description;
            productsWithPhotoDto.ProductPhotoId = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault()!.ProductPhotoId;
            productsWithPhotoDto.Image_Path = _uow.ProductPhotos.GetAll(p => p.Product_Id == product.ProductId).FirstOrDefault()!.Image_Path;
            productsWithPhotoDtos = productsWithPhotoDtos.Concat(new[] { productsWithPhotoDto });
        }
        ViewData["Name"] = productname;
        ViewData["Method"] = "ProductName";
        return View("SearchProducts", productsWithPhotoDtos);
    }
    public IActionResult PurchaseHistory(string status = null!){
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Enter_Email", "Home");
        }
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        List<OrderDetailsDto> orderDetailsDto = new List<OrderDetailsDto>();
        if(status == null! || status == "All"){
            var orders = _uow.Orders.GetAll(p => p.Customer_Id == userid);
            foreach (var item in orders)
            {
                OrderDetailsDto details = new OrderDetailsDto();
                details.order = item;
                details.orderDetails = _uow.OrderDetails.GetAll(p => p.Order_Id == item.OrderId);
                orderDetailsDto.Add(details);
            }
            orderDetailsDto = orderDetailsDto.OrderByDescending(o => o.order!.OrderId).ToList();
            ViewData["SelectedValue"] = status;
        }else{
            var orders = _uow.Orders.GetAll(p => p.Customer_Id == userid && p.Order_Status == status);
            foreach (var item in orders)
            {
                OrderDetailsDto details = new OrderDetailsDto();
                details.order = item;
                details.orderDetails = _uow.OrderDetails.GetAll(p => p.Order_Id == item.OrderId);
                orderDetailsDto.Add(details);
            }
            orderDetailsDto = orderDetailsDto.OrderByDescending(o => o.order!.OrderId).ToList();
            ViewData["SelectedValue"] = status;
        }
        return View(orderDetailsDto);
    }
    public IActionResult PersonalInfo(){
        if (!User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("Enter_Email", "Home");
        }
        var categoryDto = GetMainPageCategories(_uow);
        ViewData["MainPageData"] = categoryDto;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        return View(user);
    }
    public IActionResult CancelOrder(int id){
        var order = _uow.Orders.Get(id);
        if(order.Order_Status == "Pending"){
            order.Order_Status = "Canceled";
            _uow.Orders.Update(order);
            _uow.Save();
            return new JsonResult(true);
        }else{
            return new JsonResult(false);
        }
    }
    public IActionResult EditAddress(string address){
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        user!.Address = address;
        _uow.ApplicationUsers.Update(user);
        _uow.Save();
        return RedirectToAction(nameof(PersonalInfo));
    }
    public IActionResult EditPhonenumber(string phonenumber){
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        user!.PhoneNumber = phonenumber;
        _uow.ApplicationUsers.Update(user);
        _uow.Save();
        return RedirectToAction(nameof(PersonalInfo));
    }
    public IActionResult EditFullname(string firstname, string lastname){
        var fullname = firstname + " " + lastname;
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        user!.FullName = fullname;
        _uow.ApplicationUsers.Update(user);
        _uow.Save();
        return RedirectToAction(nameof(PersonalInfo));
    }
    public async Task<IActionResult> EditEmail(string email){
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        user!.Email = email;
        user!.NormalizedEmail = email.ToUpper();
        _uow.ApplicationUsers.Update(user);
        _uow.Save();
        await _signInManager.SignOutAsync();
        return RedirectToAction("Enter_Email", "Home");
    }
    public IActionResult CheckEmailExist(string email){
        var user = _uow.ApplicationUsers.GetAll(p => p.Email == email).FirstOrDefault();
        if(user == null){
            return new JsonResult(true);
        }else{
            return new JsonResult(false);
        }
    }
    public IActionResult EditPassword(string oldpassword, string newpassword){
        ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
        string userid = identity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = _uow.ApplicationUsers.GetAll(p => p.Id == userid).FirstOrDefault();
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        var result = passwordHasher.VerifyHashedPassword(user!, user!.PasswordHash!, oldpassword);
        if (result != PasswordVerificationResult.Success)
        {
            return new JsonResult(false);
        }
        else{
            var newHashedPassword = passwordHasher.HashPassword(user, newpassword);
            user.PasswordHash = newHashedPassword;
            _uow.ApplicationUsers.Update(user);
            _uow.Save();
            return new JsonResult(true);
        }
    }
}
