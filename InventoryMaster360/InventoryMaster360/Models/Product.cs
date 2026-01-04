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
        public int CriticStockQuantity { get; set; }// stok azalınca red'e dönecek bu sayı kaç olsun


        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
