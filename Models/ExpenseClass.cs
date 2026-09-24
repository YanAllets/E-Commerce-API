namespace ECAPI.Models;

public class ExpenseClass
{
    public string Type { get; set; }
    public decimal Value { get; set; }
    public int Installments { get; set; }
    public DateTime Date { get; set; }
    public ProductClass Product { get; set; }
}