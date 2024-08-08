using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models.DataModel;
using ProductManagementSystem.Models.ViewModel;
using System.Linq.Expressions;

namespace ProductManagementSystem.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        public IActionResult Index()
        {
          
                // Code that might throw an exception
                if (TempData.ContainsKey("success"))
                {
                    ViewBag.message = TempData["success"];
                }
                else
                {
                    ViewBag.message = TempData["error"];
                }
                var products = _context.Products.ToList();
                return View(products);
            
            
        

    }

        [Route("details")]
        public IActionResult Details(string data)
        {
            var details = _context.Products.Where(x => x.ProductId == new Guid(data)).Select(x => new ProductDetailViewModel()
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
            }).FirstOrDefault() ?? new ProductDetailViewModel();
            return View(details);
        }

        //GET: Products/Details/5
        public IActionResult Detail(Guid? id)
        {
            if (id == null) return NotFound();

            var product = _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) return NotFound();

            return View();
        }


        [Route("Add")]
        public IActionResult InsertProduct()
        {
            ViewBag.ErrorMessage = TempData["Message"];
            ViewBag.PriceErrorMessage = TempData["PriceMessage"];
            ViewBag.DescriptionErrorMessage = TempData["DescriptionMessage"];
            return View();
        }

        [Route("AddProductData")]
        public IActionResult AddProductData(string Name, decimal Price, string Description)
        {
            if (Name.Length > 100) {
                TempData["Message"] = "Name length exceeds 100 characters";
                return RedirectToAction("InsertProduct"); // Return to the view to display the error message
            }

            if (Price <= 0 || Price != Math.Floor(Price) || Price > 1000000)
            {
                TempData["PriceMessage"] = "Price must be a positive integer and within the range of 1 to 1,000,000";
                return RedirectToAction("InsertProduct"); // Return to the view to display the error message
            }

            if (Description.Length > 500)
            {
                TempData["DescriptionMessage"] = "Description length exceeds 500 characters";
                return View("InsertProduct"); // Return to the view to display the error message
            }

            Product product = new Product()
            {
                Name = Name,
                Price = Price,
                Description = Description,
                DateCreated = DateTime.Now
                //CreatedBy = 1,
             
            };
            _context.Products.Add(product);

            int result = _context.SaveChanges();

            if (result > 0)
            {
                TempData["success"] = "Record inserted successfully";
            }
            else
            {
                TempData["error"] = "Record insertion failed";
            }
            return RedirectToAction("Index");
        }

        [Route("UpdateProductData")]
        public IActionResult UpdateProductData(string ProductId, string Name, decimal Price, string Description)
        {
            if (Name.Length > 100)
            {
                TempData["Message"] = "Name length exceeds 100 characters";
                return RedirectToAction("InsertProduct"); // Return to the view to display the error message
            }

            if (Price <= 0 || Price != Math.Floor(Price) || Price > 1000000)
            {
                TempData["PriceMessage"] = "Price must be a positive integer and within the range of 1 to 1,000,000";
                return RedirectToAction("InsertProduct"); // Return to the view to display the error message
            }

            if (Description.Length > 500)
            {
                TempData["DescriptionMessage"] = "Description length exceeds 500 characters";
                return View("InsertProduct"); // Return to the view to display the error message
            }
            var product = _context.Products.Where(x => x.ProductId == new Guid(ProductId)).FirstOrDefault();
            if (product != null)
            {
                product.Name = Name;
                product.Price = Price;
                product.Description = Description;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [Route("DeleteProduct")]
        public IActionResult DeleteProduct(string data)
        {
            var product = _context.Products.Where(x => x.ProductId == new Guid(data)).FirstOrDefault();
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
