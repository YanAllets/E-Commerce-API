using ECAPI.Models;
using ECAPI.DataBase;
using MySqlConnector;

namespace ECAPI.DataService;

public class DataService
{
    public static void NonSqlQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        Config.conn.Open();
        command.ExecuteNonQuery();
        Config.conn.Close();

    }
}