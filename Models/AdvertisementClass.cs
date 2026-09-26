namespace ECAPI.Models;

public class AdvertisementClass
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; }
}