namespace ms_product_service.Models
{
    public class ProductFilter
    {
        public string? NomProduit { get; set; }
        public string? Marque { get; set; }
        public double? MinPrix { get; set; }
        public double? MaxPrix { get; set; }
        public List<string>? Categories { get; set; }
        public List<string>? Tags { get; set; }
    }
}
