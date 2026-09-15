using ECAPI.Models;

namespace ECAPI.CommerceService;
public class CommerceService
{
    public static void LogIn()
    {
        
    }
    public static bool SingUp(UserClass user)
    {
        string query = "insert into users (Id, Email, Name, Password, Adress, PhoneNumber, BirthDate) Values (@Id, @Email, @Name, @Password, @Adress, @PhoneNumber, @BirthDate);";
        DataBase.Service.NonSqlQueryClass(query,user);
        return true;
    }
}