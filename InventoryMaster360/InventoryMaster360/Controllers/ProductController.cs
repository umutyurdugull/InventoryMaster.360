using InventoryMaster360.Models;
using InventoryMaster360.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace InventoryMaster360.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Category> _categoryRepository;

        public ProductController(IGenericRepository<Product> productRepository, IGenericRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //datayı database'e kaydet.
        public static string SKUGenerator(string productName, DateTime createtime)
        {
            // "Samsung" -> "SAM"

            //calisiyor.
            string shortname = productName.Length >= 3 ? productName.Substring(0, 3) : productName;
            string ticks = DateTime.Now.Ticks.ToString();
            string uniquePart = ticks.Substring(ticks.Length - 8);
            string year = createtime.Year.ToString();
            return shortname.ToUpper() + "-" + uniquePart + "-" + year;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
         ModelState.Remove("SKU"); 
    
         ModelState.Remove("Category");

        if (!ModelState.IsValid)
        {
        
             return View(product); 
        }

        product.CreatedTime = DateTime.Now;
        product.UpdatedTime = DateTime.Now;
    
        product.SKU = SKUGenerator(product.Name, product.CreatedTime);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveAsync();
        return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            
            //Telefon'u bilerek alış > satış şeklinde ekledim. Suanda fluent validation ile ilgili herhangi bir şey yok.

            var products = await _productRepository.GetAllAsync();
            return View(products);


        }
    }
}