using System.Data;
using FluentValidation;

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
        public int CriticStockQuantity { get; set; }// Stok sayısı belli bir sınıra gelince o andaki stok sayısını gösteren sayı kırmızıya dönecek 
                                                    //Bunun icin de fluent validation eklemek lazım.


        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsDeleted { get; set; } 
    }


    public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name cannot be empty.")
                .Length(2, 100).WithMessage("Product name must be between 2 and 100 characters.");

            RuleFor(x => x.BuyingPrice)
                .GreaterThan(0).WithMessage("Buying price must be greater than 0.");

            RuleFor(x => x.SalesPrice)
                .GreaterThan(0).WithMessage("Sales price must be greater than 0.")
                .GreaterThan(x => x.BuyingPrice).WithMessage("Sales price cannot be lower than the buying price.");


            
        }
}
    }

