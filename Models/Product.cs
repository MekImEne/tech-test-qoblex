namespace BundleTree.Models
{
    public class Product
    {
        public Guid ProductId { get; set; } // Primary key
        public string ProductName { get; set; } = string.Empty; // Product name
        public int Stock { get; set; } // Available stock
    }
}
