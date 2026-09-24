using ECAPI.Models;

namespace ECAPI.CommerceService;
public class CommerceService
{
    public List<UserClass> Karts = new List<UserClass>();
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
    public static bool CreateProduct(ProductClass product)
    {
        string query = "insert into products (Name, Value, Store, Description, Reviews,Discount) Values (@Name, @Value, @Store, @Description, @Reviews, @Discount)";
        DataBase.Service.NonSqlQueryClass(query,product);
        return true;
    }
    public static bool CreateStore(StoreClass store)
    {
        string query = "insert into stores (Name, Owner, Balance) Values (@Name, @Owner, @Balance)";
        DataBase.Service.NonSqlQueryClass(query,store);
        return true;
    }

}