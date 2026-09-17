using ECAPI.Models;

namespace ECAPI.CommerceService;
public class CommerceService
{
    public List<UserClass> Karts = new List<UserClass>();
    public static (bool success,object user) LogIn(UserClass user)
    {
        string query = "select count(id) from users where Email = @Email and Password = @Password;";
        int sucess = DataBase.Service.SqlScalarClass(query,user);
        if(sucess == 1 )
        {
            return (true,user);
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

}