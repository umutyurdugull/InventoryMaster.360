using FluentValidation;
using InventoryMaster360.Models;
using InventoryMaster360.Repositories;
using Microsoft.EntityFrameworkCore; 

namespace InventoryMaster360.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CategoryValidator : AbstractValidator<Category>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryValidator(IGenericRepository<Category> categoryRepository)
        {
           _categoryRepository = categoryRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty.")
                .Length(2, 50).WithMessage("Category name must be between 2 and 50 characters.")
                .MustAsync(IsNameUnique).WithMessage("There is already a category with the same name.");
        }

       private async Task<bool> IsNameUnique(Category category, string name, CancellationToken token)
        {
            
            var query = _categoryRepository.Where(x => x.Name.ToLower() == name.ToLower() && !x.IsDeleted);

            if (category.Id > 0)
            {
                query = query.Where(x => x.Id != category.Id);
            }

            bool isDuplicate = await query.AnyAsync(token);

            return !isDuplicate;
        }
    }
}