using ECAPI.Models;
using ECAPI.StoreService;

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
    public static bool AddToCart(int AdId,int quantity,int cartId)
    {
        if (ThisExist(AdId))
        {
            string queryAd = $"select * from advertisements where id = {AdId}";
            string queryRead = $"select * from cart_item where CartId = {cartId} and AdvertisementId = {AdId};";
            string query = $"insert into cart_item (CartId, AdvertisementId, Quantity) values (@CartId, @AdvertisementId, @Quantity);";
            string queryEqual = $"select count(id) from cart_item where CartId = {cartId} and AdvertisementId = {AdId};";
            string queryUpdate = $"update cart_item set Id = @Id, CartId = @CartId, AdvertisementId = @AdvertisementId, Quantity = @Quantity where CartId = @CartId and AdvertisementId = @AdvertisementId";
            string queryqq = "select * from cart_item where CartId = 1 and AdvertisementId = 1;";
            CartItemClass cartItemDB = DataBase.Service.SqlReadCartItem(queryRead);
            AdvertisementClass ad = DataBase.Service.SqlReadAd(queryAd);

            CartItemClass cartItemOutput = new CartItemClass()
            {
                CartId = cartId,
                AdvertisementId = ad.Id,
                Quantity = cartItemDB.Quantity + quantity
            };

            int i = DataBase.Service.SqlScalar(queryEqual);
            if (i != 1)
            {    
                DataBase.Service.NonSqlQueryClass(query,cartItemOutput);
                return true;
            }
            else if(i == 1)
            {
                DataBase.Service.NonSqlQueryClass(queryUpdate,cartItemOutput);
                return false;
            }
        }
        return false;
    }
    public static void RemoveFromCart(int quantity,int cartId,int AdId)
    {
        string queryRead = $"select * from cart_item where CartId = {cartId} and AdvertisementId = {AdId};";
        string query = $"update cart_item set quantity = quantity - {quantity} where CartId = {cartId} and AdvertisementId = {AdId};";
        string queryDelete = $"DELETE FROM cart_item WHERE CartId = {cartId} and AdvertisementId = {AdId};";

        CartItemClass cartItem = DataBase.Service.SqlReadCartItem(queryRead);

        if(cartItem.Quantity <= 0)
        {
            DataBase.Service.NonSqlQuery(queryDelete);
        }
        else
        {
            DataBase.Service.NonSqlQuery(query);
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
    public static decimal Buy(int cartId,string paymentMethod,int installments)
    {
        CartClass cart = new CartClass()
        {
            Id = cartId
        };
        string queryTotal = $"SELECT COALESCE(SUM((a.Price - (a.Price * a.Discount / 100)) * ci.Quantity),0) AS Total FROM cart_item ci INNER JOIN advertisements a ON a.Id = ci.AdvertisementId WHERE ci.CartId = @CartId;";
        // faz o total do cart
        string queryGetBalanceStore = $"SELECT COALESCE(SUM((a.Price - (a.Price * a.Discount / 100)) * ci.Quantity),0) AS Total,a.StoreId as StoreId FROM cart_item ci INNER JOIN advertisements a ON a.Id = ci.AdvertisementId WHERE ci.CartId = @CartId group by StoreId;";
        //pega os valores que devem ir pra cada loja respectivamente de acordo com o carrinho
        string querySetBalanceStore = "update stores set balance = @Balance where id = @OwnerId";
        //aplica os devidos valores
        string queryClearCart = "delete from cart_item where CartId = @CartId";
        //limpa o cart
        string queryRead = $"select UserId as Id,PhoneNumber from users u join cart c on c.UserId = u.Id where c.Id = {cartId}";
        //bah sei la pra que eh isso,acho que e pra dar o telefone e o usuario certo pra registrar a order
        System.Console.WriteLine("!");
        List<StoreClass> list = DataBase.Service.sqlReadCart(queryGetBalanceStore,cartId);
        System.Console.WriteLine("!");

        foreach (StoreClass store in list)
        {
            DataBase.Service.NonSqlQueryClass(querySetBalanceStore,store);
        }
        System.Console.WriteLine("!");

        decimal Total = DataBase.Service.SqlScalarClass(queryTotal,cart);
        System.Console.WriteLine("!");
        UserClass user = DataBase.Service.SqlReadUser2(queryRead);
        System.Console.WriteLine("!");

        OrderClass Order = new OrderClass()
        {
            UserId = user.Id,
            Type = paymentMethod,
            Value = Total,
            Installments = installments,
            PhoneNumber = user.PhoneNumber,
            Date = DateTime.Now
        };
        System.Console.WriteLine("!");
        StoreService.StoreService.CreateOrder(Order);
        System.Console.WriteLine("!");

        DataBase.Service.NonSqlQueryClass(queryClearCart,cart);
        System.Console.WriteLine("!");
        return Total;
    }
    public static void MakeReview(ReviewClass review)
    {
        string queryInsert = "insert into reviews (UserId, AdvertisementId, Rating, Comment, Date) values (@UserId, @AdvertisementId, @Rating, @Comment, @Date)";
        DataBase.Service.NonSqlQueryClass(queryInsert,review);
    }
    public static void EditProfile(int userId,UserClass user)
    {
        string query = "update users set Id = @Id, Email = @Email, Name = @Name, Password = @Password, Adress = @Adress, PhoneNumber = @PhoneNumber, BirthDate = @BirthDate, balance = @balance, Role = @Role where id = 1";
        DataBase.Service.NonSqlQueryClass(query,user);
    }
}