using ECAPI.Models;
using ECAPI.CommerceService;
using ECAPI.DataBase;

namespace ECAPI.AccountService;

public class AccountService
{
    public static bool ThisUserExist(int id)
    {
        string query = $"select Count(id) from users where id = {id}";
        int i =Service.SqlScalar(query);
        if(i != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static UserClass ViewAccount(int id)
    {
        string query = $"select * from users where Id = {id}";
        UserClass user = Service.SqlReadUser(query);
        return user;
    }
    public static string EditProfile(int userId,UserClass user)
    {
        if (ThisUserExist(userId))
        {
        // NAO TA COMPLETO MT PELO CONTRARIO
        string query = "update users set Id = @Id, Email = @Email, Name = @Name, Password = @Password, Adress = @Adress, PhoneNumber = @PhoneNumber, BirthDate = @BirthDate, balance = @balance, Role = @Role where id = 1";
        Service.NonSqlQueryClass(query,user);
        return "Success";
        }
        else
        {
            return "This account does NOT exist";
        }
    }
}
