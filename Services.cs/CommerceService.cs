using ECAPI.Models;

namespace ECAPI.CommerceService;
public class CommerceService
{
    public static (bool success,object user) LogIn(UserClass user)
    {
        string queryEmail = "select count(id) from users where Email = @Email;";
        string queryPassword = "select count(id) from users where Email = @Email and Password = @Password;";

        int success1 = DataBase.Service.SqlScalarClass(queryEmail,user);
        int success2 = DataBase.Service.SqlScalarClass(queryPassword,user);
        int success = success1 + success2;

        if(success >= 1 )
        {
            if (success == 2)
            {                
                return (true,user);
            }
            else
            {
                return (false,user);
            }
        }
        else
        {
            return (false,null);
        }
    }
    public static bool SingUp(UserClass user)
    {
        string query = "insert into users (Id, Email, Name, Password, Adress, PhoneNumber, BirthDate) Values (@Id, @Email, @Name, @Password, @Adress, @PhoneNumber, @BirthDate);";
        DataBase.Service.NonSqlQueryClass(query,user);
        return true;
    }
    public static void AddToCart(int AdId,int quantity,int cartId)
    {
        if (ThisExist(AdId))
        {
            string queryAd = $"select * from advertisements where id = {AdId}";
            string query = $"insert into cartitem (CartId, AdvertisementId, Quantity) values (@CartId, @AdvertisementId, @Quantity);";

            AdvertisementClass ad = DataBase.Service.SqlReadAd(queryAd);

            CartItemClass cartItem = new CartItemClass()
            {
                CartId = cartId,
                AdvertisementId = ad.Id,
                Quantity = quantity
            };
            DataBase.Service.NonSqlQueryClass(query,cartItem);
        }
    }
    public static bool CreateStore(StoreClass store)
    {
        string queryStore = "insert into stores (Name, OwnerId, Balance) Values (@Name, @OwnerId, @Balance);";
        DataBase.Service.NonSqlQueryClass(queryStore,store);
        return true;
    }
    public static bool ThisExist(int adId)
    {
        string queryAd = $"select count(id) from advertisements where Id = {adId}";
        int i = DataBase.Service.SqlScalar(queryAd);
        if (i == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}