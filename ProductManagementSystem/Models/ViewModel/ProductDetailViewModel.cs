namespace ProductManagementSystem.Models.ViewModel
{
    public class ProductDetailViewModel
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
       
    }
}
