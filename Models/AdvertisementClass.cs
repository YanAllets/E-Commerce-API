namespace ECAPI.Models;

public class AdvertisementClass
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public decimal Discount { get; set; }
    public UserClass Owner { get; set; }
}