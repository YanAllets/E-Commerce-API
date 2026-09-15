using ECAPI.Models;
using MySqlConnector;

namespace ECAPI.DataBase;

public class Service
{
    public static void NonSqlQuery(string query)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        Config.conn.Open();
        command.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static void NonSqlQueryClass(string query,UserClass User)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        command.Parameters.AddWithValue("@id",User.Id);
        command.Parameters.AddWithValue("@Email",User.Email);
        command.Parameters.AddWithValue("@Name",User.Name);
        command.Parameters.AddWithValue("@Password",User.Password);
        command.Parameters.AddWithValue("@Adress",User.Adress);
        command.Parameters.AddWithValue("@PhoneNumber",User.PhoneNumber);
        command.Parameters.AddWithValue("@BirthDate",User.BirthDate);

        Config.conn.Open();
        command.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static int SqlScalar(string query)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);


        Config.conn.Open();
        object ScalarObj = command.ExecuteScalar();
        int i = Convert.ToInt32(ScalarObj);
        Config.conn.Close();
        return i;
    }
}