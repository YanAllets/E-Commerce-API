using ECAPI.Models;

namespace ECAPI.StoreService;

public class StoreService
{
    public static bool CreateProduct(ProductClass product)
    {
        string queryAddProduct = "insert into products (Name, Brand, Model, Barcode, Description) values (@Name,@Brand,@Model,@Barcode,@Description)";
        DataBase.Service.NonSqlQueryClass(queryAddProduct,product);
        return true;
    }
    public static (bool success,string message) CreateAd(AdvertisementClass ad)
    {
        string errorMessage = "Product not found. Register the product before creating the listing.";
        string queryProductExist = "select count(id) from products where id = @ProductId";
        string queryAddAd = "insert into advertisements (ProductId, StoreId, Price, Discount, StockQuantity, Description) Values (@ProductId, @StoreId, @Price, @Discount, @StockQuantity, @Description);";

        DataBase.Service.NonSqlQueryClass(queryAddAd,ad);
        int i = DataBase.Service.SqlScalarClass(queryProductExist,ad);

        if(i == 1)
        {
            return (true,null);
        }
        else
        {
            return (false,errorMessage);
        }
    }
}