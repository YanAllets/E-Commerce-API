namespace ECAPI.Models;

public class CartItemClass
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int AdvertisementId { get; set; }
    public int Quantity { get; set; }
}