using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walmart.ApplicationCore.Utilities;
using Walmart.ApplicationCore.Models;
using Walmart.Infrastructure.Data;
using Walmart.Infrastructure.Interfaces;
using Walmart.ApplicationCore.ParamModels;
using Walmart.ApplicationCore.ViewModels;
using Walmart.ApplicationCore.DtoModels;
using Newtonsoft.Json;
using static Walmart.ApplicationCore.Models.Product;

namespace Walmart.Web.Controllers;
[Area("Manager")]
[Authorize(Roles = WebsiteRole.Manager)]
    public class ManagerDashboardController : BaseController
    {
        #region Constructors
        private IWebHostEnvironment _env;
        private string? serverdestinationpath { get; set; }
        public ManagerDashboardController(IUnitOfWork uow, IWebHostEnvironment env) : base(uow)
        {
            _env = env;
        }
        #endregion
        // GET: Admin/Movies
        public IActionResult Index()
        {   
            DashboardDto dashboardDto = new DashboardDto();
            var totalorder = _uow.Orders.GetAll().Count();
            var pendingorder = _uow.Orders.GetAll(p => p.Order_Status == "Pending").Count();
            var paidorder = _uow.Orders.GetAll(p => p.Order_Status == "Paid").Count();
            var shippingorder = _uow.Orders.GetAll(p => p.Order_Status == "Shipping").Count();
            var users = _uow.ApplicationUsers.GetAll();
            var customerid = _uow.ApplicationUsers.GetUserRole("Customer");
            var customercount = 0;
            var managerid = _uow.ApplicationUsers.GetUserRole("Manager");
            var managercount = 0;
            var adminid = _uow.ApplicationUsers.GetUserRole("Admin");
            var admincount = 0;
            var totaluser = users.Count();
            foreach(var user in users){
                var checkrole = _uow.ApplicationUsers.CheckRole(user.Id);
                if(checkrole == customerid){
                    customercount++;
                }else if(checkrole == managerid){
                    managercount++;
                }else{
                    admincount++;
                }
            }
            var orders = _uow.Orders.GetAll(p => p.Order_Status == "Paid");
            decimal dec = 0;
            foreach (var order in orders)
            {
                dec = (decimal)(dec + order.Total_Price)!;
            }
            int totalearnings = (int)Math.Round(dec);
            var totalcategory = _uow.Categories.GetAll().Count();
            var totalsubcategory = _uow.SubCategories.GetAll().Count();
            var totalproduct = _uow.Products.GetAll().Count();
            dashboardDto.Total_Order = totalorder;
            dashboardDto.Pending_Order = pendingorder;
            dashboardDto.Paid_Order = paidorder;
            dashboardDto.Shipping_Order = shippingorder;
            dashboardDto.Total_User = totaluser;
            dashboardDto.Customer = customercount;
            dashboardDto.Manager = managercount;
            dashboardDto.Admin = admincount;
            dashboardDto.Total_Earnings = totalearnings;
            dashboardDto.Total_Category = totalcategory;
            dashboardDto.Total_Subcategory = totalsubcategory;
            dashboardDto.Total_Product = totalproduct;
            return View(dashboardDto);
        }
        
        public IActionResult ManageUsers(int id = 1, string role = null!){
            if(role == null || role == "All"){
                Console.WriteLine("if");
                UserParam userParam = new UserParam{
                PageIndex = id, PageSize = 10
                };
                Task<UserVM> users = _uow.ApplicationUsers.GetUsersByPaging(userParam);
                ViewData["SelectedValue"] = role;
                return View(users.Result);
            }
            else{
                Console.WriteLine("else");
                UserParam userParam = new UserParam{
                PageIndex = id, PageSize = 10
                };
                Task<UserVM> users = _uow.ApplicationUsers.GetUsersByPaging2(userParam, role);
                ViewData["SelectedValue"] = role;
                return View(users.Result);
            }
        }
        public IActionResult ManageCategories(){
            var categories = _uow.Categories.GetCategories();
            return View(categories);
        }
        public IActionResult CreateCategory(){
            Category category;
            category = _uow.Categories.CreateCategory();
            _uow.Categories.Add(category);
            _uow.Save();
            return RedirectToAction(nameof(ManageCategories));
        }
        public IActionResult AddSubCategory(int id){
            SubCategory subCategory;
            subCategory = _uow.SubCategories.CreateSubCategory(id);
            _uow.SubCategories.Add(subCategory);
            _uow.Save();
            return RedirectToAction(nameof(ManageCategories));
        }
        public IActionResult CategoryDetails(int id){
            Category category;
            category = _uow.Categories.Get(id);
            return View("EditCategory", category);
        }
        [HttpPost]
        public IActionResult EditCategory(int categoryid, string categoryname, IFormFile categoryimage){
                string webRootPath = _env.WebRootPath;
                string inputfilename = String.Format("Category-{0}.jpg", categoryid);
                string uploadsPath = Path.Combine("images", "CategoryImages", inputfilename);
                string filePath = Path.Combine(webRootPath, uploadsPath);
            if(categoryimage == null || categoryimage?.Length == 0){            
                Category category = new Category();
                category = _uow.Categories.Get(categoryid);
                category.CategoryId = categoryid;
                category.Category_Name = categoryname;
                category.Category_Image = category.Category_Image;
                _uow.Categories.Update(category);
                _uow.Save();
                return RedirectToAction(nameof(ManageCategories));
            } else{
                using(FileStream stream = new FileStream(filePath, FileMode.Create)){
                    categoryimage?.CopyTo(stream);
                }
                Category category = new Category();
                category.CategoryId = categoryid;
                category.Category_Name = categoryname;
                string uploadsPath2 = uploadsPath.Replace("\\","/");
                category.Category_Image = uploadsPath2;
                _uow.Categories.Update(category);
                _uow.Save();
                return RedirectToAction(nameof(ManageCategories));
            }
        }
        [HttpPost]
        public IActionResult UpdateSubCategory(int subcategoryid, int categoryid, string subcategoryname){
            SubCategory subCategory = new SubCategory();
            subCategory.SubCategoryId = subcategoryid;
            subCategory.Category_Id = categoryid;
            subCategory.SubCategory_Name = subcategoryname;
            _uow.SubCategories.Update(subCategory);
            _uow.Save();
            return RedirectToAction(nameof(ManageCategories));
        }
        public IActionResult ManageProducts(int id = 1, int categoryid = 0, int subcategoryid = 0){
            var categories = _uow.Categories.GetAll();
            ViewData["Categories"] = categories;
            if(categoryid == 0){
                Param param = new Param{
                    PageIndex = id, PageSize = 10
                };
                Task<ProductVM> products = _uow.Products.GetProductsByPaging(param);
                ViewData["selectedCategory"] = "All";
                ViewData["subcategory"] = null;
                ViewData["selectedSubcategory"] = null;
                return View(products.Result);
            }else if(categoryid != 0 && subcategoryid == 0){
                var categoryname = _uow.Categories.Get(categoryid).Category_Name;
                var subcategories = _uow.SubCategories.GetAll(p => p.Category_Id == categoryid);
                Param param = new Param{
                    PageIndex = id, PageSize = 10
                };
                Task<ProductVM> products = _uow.Products.GetProductsByPaging2(param, categoryid);
                ViewData["selectedCategory"] = categoryname;
                ViewData["subcategory"] = subcategories;
                ViewData["selectedSubcategory"] = null;
                return View(products.Result);
            }else{
                var categoryname = _uow.Categories.Get(categoryid).Category_Name;
                var subcategories = _uow.SubCategories.GetAll(p => p.Category_Id == categoryid);
                string selectedsubcategory = null!;
                foreach (var item in subcategories)
                {
                    if(item.SubCategoryId == subcategoryid){
                        selectedsubcategory = item.SubCategory_Name!;
                        break;
                    }
                }
                Param param = new Param{
                    PageIndex = id, PageSize = 10
                };
                Task<ProductVM> products = _uow.Products.GetProductsByPaging3(param, subcategoryid);
                ViewData["selectedCategory"] = categoryname;
                ViewData["subcategory"] = subcategories;
                ViewData["selectedSubcategory"] = selectedsubcategory;
                return View(products.Result);
            }
        }
        public IActionResult CreateProduct(){
            Product product;
            product = _uow.Products.CreateProduct();
            _uow.Products.Add(product);
            _uow.Save();
            return RedirectToAction(nameof(ManageProducts));
        }
        public IActionResult ProductDetails(int id, bool isPhoto = false){
            var product = _uow.Products.Get(id);
            var subcategory = _uow.SubCategories.Get(product.SubCategory_Id);
            if(subcategory != null && subcategory.SubCategoryId != 0){
            var category = _uow.Categories.Get(subcategory.Category_Id);
            var subcategories = _uow.SubCategories.GetAll(p => p.Category_Id == category.CategoryId);
            var categories = _uow.Categories.GetAll();
            var images = _uow.ProductPhotos.GetAll(p => p.Product_Id == id);
            EditProductDto editProductDto = new EditProductDto();
            editProductDto.ProductId = product.ProductId;
            editProductDto.Product_Number = product.Product_Number;
            editProductDto.Product_Name = product.Product_Name;
            editProductDto.Product_Price = product.Product_Price;
            editProductDto.Product_Quantity = product.Product_Quantity;
            editProductDto.Product_Status = product.Product_Status;
            editProductDto.Category_Id = category.CategoryId;
            editProductDto.Category_Name = category.Category_Name;
            editProductDto.SubCategory_Name = product.SubCategory_Name;
            editProductDto.SubCategory_Id = product.SubCategory_Id;
            editProductDto.Product_Description = product.Product_Description;
            editProductDto.ImagePaths = (ICollection<ProductPhoto>?)images;
            editProductDto.Categories = (ICollection<Category>?)categories;
            editProductDto.SubCategories = (ICollection<SubCategory>)subcategories;
            editProductDto.ShowPhoto = isPhoto;
            return View(editProductDto);
            }else{
            var categories = _uow.Categories.GetAll();
            var images = _uow.ProductPhotos.GetAll(p => p.Product_Id == id);
            EditProductDto editProductDto = new EditProductDto();
            editProductDto.ProductId = product.ProductId;
            editProductDto.Product_Number = product.Product_Number;
            editProductDto.Product_Name = product.Product_Name;
            editProductDto.Product_Price = product.Product_Price;
            editProductDto.Product_Quantity = product.Product_Quantity;
            editProductDto.Product_Status = product.Product_Status;
            editProductDto.SubCategory_Name = product.SubCategory_Name;
            editProductDto.SubCategory_Id = product.SubCategory_Id;
            editProductDto.Product_Description = product.Product_Description;
            editProductDto.ImagePaths = (ICollection<ProductPhoto>?)images;
            editProductDto.Categories = (ICollection<Category>?)categories;
            editProductDto.ShowPhoto = isPhoto;
            return View(editProductDto);
            }
        }
        public IActionResult GetSubcategorybyCategory(int categoryid){
            var subcategories = _uow.SubCategories.GetAll(p => p.Category_Id == categoryid);
            return new JsonResult(subcategories);
        }
        [HttpPost]
        public IActionResult EditProduct(int productid, int productnumber, int subcategoryid, string subcate, string productname, decimal productprice, int productquantity, string productdescription, string productstatus){
            Product product = new Product();
            ProductStatus status = (ProductStatus)Enum.Parse(typeof(ProductStatus), productstatus);
            product.ProductId = productid;
            product.Product_Number = productnumber;
            product.SubCategory_Id = subcategoryid;
            product.SubCategory_Name = subcate;
            product.Product_Name = productname;
            product.Product_Price = productprice;
            if(productstatus == "Sold"){
                product.Product_Quantity = 0;
            }else{
                product.Product_Quantity = productquantity;
            }
            product.Product_Description = productdescription;
            var noimage = _uow.ProductPhotos.GetAll(p => p.Product_Id == productid).FirstOrDefault();
            if(noimage == null){
                var status2 = "Draft";
                status = (ProductStatus)Enum.Parse(typeof(ProductStatus), status2);
                product.Product_Status = status;
            }else{
                product.Product_Status = status;
            }
            Console.WriteLine(product.SubCategory_Id);
            _uow.Products.Update(product);
            _uow.Save();
            return RedirectToAction(nameof(ManageProducts));
        }
        [HttpPost]
        public IActionResult UploadProductImages(int pid, int pnum, IFormFile[] productimages){
            string webRootPath = _env.WebRootPath;
            int start;
            Console.WriteLine(productimages.Length);
            Console.WriteLine(pid);
            Console.WriteLine(pnum);
            ProductPhoto productPhoto = _uow.ProductPhotos.GetProductPhotoWithMaxSequence(pid);
            string foldername = pnum.ToString();
            string folderpath = Path.Combine(webRootPath, "images", "ProductImages", foldername);
            if(productPhoto == null){
                start = 1;
            }else {
                start = productPhoto.Sequence + 1;
            }
            int sequence = start;
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            foreach (var file in productimages)
            {
                string filename = String.Format("{0}-{1:D2}.jpg", pnum, sequence);
                string filepath = Path.Combine(folderpath, filename);
                string uploadsPath = "images/ProductImages/" + foldername + "/" + filename;
                using(FileStream stream = new FileStream(filepath, FileMode.Create)){
                    file.CopyTo(stream);
                }
                ProductPhoto productPhoto1 = new ProductPhoto();
                productPhoto1.Product_Id = pid;
                productPhoto1.Sequence = sequence;
                productPhoto1.Image_Path = uploadsPath.Replace("\\","/");
                _uow.ProductPhotos.Add(productPhoto1);
                _uow.Save();
                sequence++;
            }
            return RedirectToAction(nameof(ProductDetails), new { id = pid, isPhoto = true});
        }
        public IActionResult DeleteProductImage(int id){
            string webRootPath = _env.WebRootPath;
            ProductPhoto productPhoto = _uow.ProductPhotos.Get(id);
            
            string filepath = Path.Combine(webRootPath, productPhoto.Image_Path!);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            _uow.ProductPhotos.Remove(productPhoto);
            _uow.Save();
            var productPhotos = _uow.ProductPhotos.GetAll(p => p.Product_Id == productPhoto.Product_Id);
            if(productPhotos.Count() == 0){
                var product = _uow.Products.Get(productPhoto.Product_Id);
                product.Product_Status = ProductStatus.Draft;
                _uow.Products.Update(product);
                _uow.Save();
            }
            return RedirectToAction(nameof(ProductDetails), new { id = productPhoto.Product_Id, isPhoto = true});
        }
        public IActionResult ManageOrders(int id = 1, string status = null!){
            if(status == null || status == "All"){
                Param param = new Param{
                    PageIndex = id, PageSize = 10
                };
                Task<OrderVM> products = _uow.Orders.GetOrdersByPaging(param);
                ViewData["SelectedValue"] = status;
                return View(products.Result);
            }else{
                Param param = new Param{
                    PageIndex = id, PageSize = 10
                };
                Task<OrderVM> products = _uow.Orders.GetOrdersByPaging2(param, status);
                ViewData["SelectedValue"] = status;
                return View(products.Result);
            }
        }
        public IActionResult OrderDetails(int id){
            var order = _uow.Orders.Get(id);
            var details = _uow.OrderDetails.GetAll(p => p.Order_Id == id);
            OrderDetailsDto orderDetailsDto = new OrderDetailsDto();
            orderDetailsDto.order = order;
            orderDetailsDto.orderDetails = details;
            return View(orderDetailsDto);
        }
        [HttpPost]
        public IActionResult UpdateOrder(int orderid, string orderstatus){
            var oldorder = _uow.Orders.Get(orderid);
            oldorder.Order_Status = orderstatus;
            _uow.Orders.Update(oldorder);
            _uow.Save();
            return RedirectToAction(nameof(ManageOrders));
        }
        public IActionResult CheckCategory(){
            var sub = _uow.SubCategories.GetAll();
            if(sub == null){
                return new JsonResult(false);
            }else{
                return new JsonResult(true);
            }
        }
    }
