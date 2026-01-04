using System.Data;
using FluentValidation;
using InventoryMaster360.Repositories;
using InventoryMaster360.Models;

namespace InventoryMaster360.Models
{
   
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int StockQuantity { get; set; }
        public int CriticStockQuantity { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    
    public class ProductValidator : AbstractValidator<Product>
    {
        
        private readonly IGenericRepository<Product> _productRepository;

        public ProductValidator(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.")
                .MustAsync(IsNameUnique).WithMessage("Bu isimde bir ürün zaten mevcut.");

            RuleFor(x => x.BuyingPrice)
                .GreaterThan(0).WithMessage("Buying price must be greater than 0.");

            RuleFor(x => x.SalesPrice)
                .GreaterThan(0).WithMessage("Sales price must be greater than 0.")
                .GreaterThan(x => x.BuyingPrice).WithMessage("Sales price cannot be lower than the buying price.");

            RuleFor(x => x.CriticStockQuantity)
                .GreaterThan(0).WithMessage("Critic stock quantity must be greater than 0")
                .LessThan(x => x.StockQuantity).WithMessage("Critic stock quantity must be less than stock quantity");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Category ID cannot be null or empty");
        }

        private async Task<bool> IsNameUnique(string name, CancellationToken token)
        {
            var allProducts = await _productRepository.GetAllAsync();
            bool exists = allProducts.Any(x => x.Name.ToLower() == name.ToLower());
            return !exists;
        }
    }
}