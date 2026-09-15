using MySqlConnector;

namespace ECAPI.DataBase;

public class Config
{
    public static string connection = "Server=localhost;Database=ecommerce;User ID=root;Password=Eskimo-Uptight-Explosion6";
    public static MySqlConnection conn = new MySqlConnection(connection);
}