using FluentValidation;
using InventoryMaster360.Models;
using InventoryMaster360.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryMaster360.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IValidator<Category> _validator;

        
        public CategoryController(
            IGenericRepository<Category> categoryRepository,
            IValidator<Category> validator) 
        {
            _categoryRepository = categoryRepository;
            _validator = validator; 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetByIdAsync(id.Value);
            if (id == null)
            {
                return NotFound();
            }

            return View(category);

        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComfirmed(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _categoryRepository.Remove(category);
            await _categoryRepository.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            FluentValidation.Results.ValidationResult result = await _validator.ValidateAsync(category);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(category); 
            }

            category.CreatedTime = DateTime.Now;
            category.UpdatedTime = DateTime.Now;
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();

            return RedirectToAction("Index");
        }
    }
}