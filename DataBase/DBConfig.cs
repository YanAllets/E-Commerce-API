using MySqlConnector;

namespace ECAPI.DataBase;

public class Config
{
    public static string connection = "Server=localhost;Database=ecommerce;User ID=root;Password=Plutonium1-Mushiness-Said";
    public static MySqlConnection conn = new MySqlConnection(connection);
}