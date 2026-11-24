using ECAPI.Models;
using ECAPI.CommerceService;
using ECAPI.DataBase;
using ECAPI.AuthenticationService;

namespace ECAPI.AccountService;

public class AccountService
{
    private readonly JwtService _jwtService;

    public AccountService(JwtService jwtService)
    {
        _jwtService = jwtService;
    }
    public (bool success,object? user,string? token) LogIn(UserClass user)
    {
        string queryEmail = "select count(id) from users where Email = @Email;";
        string queryPassword = "select count(id) from users where Email = @Email and Password = @Password;";
        string queryDatabaseId = "select id from users where Email = @Email and Password = @Password;";

        int DataBaseId = Service.SqlScalarClass(queryDatabaseId,user);
        int success1 = Service.SqlScalarClass(queryEmail,user);
        int success2 = Service.SqlScalarClass(queryPassword,user);
        int success = success1 + success2;

        if(success >= 1 )
        {
            if (success == 2)
            {                
                string token = _jwtService.GenerateToken(DataBaseId);
                return (true,user,token);
            }
            else
            {
                return (false,user,null);
            }
        }
        else
        {
            return (false,null,null);
        }
    }
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
        string query = $"update users set Email = @Email, Name = @Name, Password = @Password, Adress = @Adress, PhoneNumber = @PhoneNumber, BirthDate = @BirthDate, Balance = @Balance, Role = @Role where id = {userId}";
        Service.NonSqlQueryClass(query,user);
        return "Success";
        }
        else
        {
            return "This account does NOT exist";
        }
    }
}
