using ECAPI.Models;

namespace ECAPI.StoreService;

public class StoreService
{
    public static bool CreateAd(ProductClass product,string Barcode)
    {
        string queryProductExist = "select count(id) from products where Barcode = @Barcode";
        string query = "insert into advertisements (Id, ProductId, StoreId, Price, Discount, StockQuantity, Description) Values (@Id, @ProductId, @StoreId, @Price, @Discount, @StockQuantity, @Description);";

        DataBase.Service.NonSqlQueryClass(query,product);
        return true;
    }
}