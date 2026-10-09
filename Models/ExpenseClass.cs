using Microsoft.AspNetCore.Identity;

namespace ECAPI.Models;

public class ExpenseClass
{
    public int UserId { get; set; }
    public string Type { get; set; }
    public decimal Value { get; set; }
    public int Installments { get; set; }
    public string PhoneNumber { get; set;}
    public DateTime Date { get; set; }
}