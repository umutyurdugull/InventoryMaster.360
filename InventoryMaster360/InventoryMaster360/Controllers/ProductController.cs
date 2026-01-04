using InventoryMaster360.Models;
using InventoryMaster360.Repositories;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace InventoryMaster360.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        
        private readonly IValidator<Product> _validator; 

       
        public ProductController(
            IGenericRepository<Product> productRepository, 
            IGenericRepository<Category> categoryRepository,
            IValidator<Product> validator)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public static string SKUGenerator(string productName, DateTime createtime)
        {
            string shortname = productName.Length >= 3 ? productName.Substring(0, 3) : productName;
            string ticks = DateTime.Now.Ticks.ToString();
            string uniquePart = ticks.Substring(ticks.Length - 8);
            string year = createtime.Year.ToString();
            return shortname.ToUpper() + "-" + uniquePart + "-" + year;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            ValidationResult result = await _validator.ValidateAsync(product);

           

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    if (error.PropertyName != "SKU" && error.PropertyName != "Category")
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
                
                if (ModelState.ErrorCount > 0)
                {
                    return View(product);
                }
            }

            // ... Kayıt işlemleri ...
            product.CreatedTime = DateTime.Now;
            product.UpdatedTime = DateTime.Now;
            product.SKU = SKUGenerator(product.Name, product.CreatedTime);

            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Index()
        {
             var products = await _productRepository.GetAllAsync();
             return View(products);
        }
    }
}
