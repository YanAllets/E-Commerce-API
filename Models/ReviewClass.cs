namespace ECAPI.Models;

public class ReviewClass
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AdvertisementId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
}