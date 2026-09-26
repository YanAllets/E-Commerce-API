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
    public static void NonSqlQueryClass(string query, object obj)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        if (obj is UserClass user)
        {
            command.Parameters.AddWithValue("@id",user.Id);
            command.Parameters.AddWithValue("@Email",user.Email);
            command.Parameters.AddWithValue("@Name",user.Name);
            command.Parameters.AddWithValue("@Password",user.Password);
            command.Parameters.AddWithValue("@Adress",user.Adress);
            command.Parameters.AddWithValue("@PhoneNumber",user.PhoneNumber);
            command.Parameters.AddWithValue("@BirthDate",user.BirthDate);
        }else if (obj is ProductClass product)
        {
            command.Parameters.AddWithValue("@Name",product.Name);
            command.Parameters.AddWithValue("@Brand",product.Brand);
            command.Parameters.AddWithValue("@Model",product.Model);
            command.Parameters.AddWithValue("@Barcode",product.Barcode);
            command.Parameters.AddWithValue("@Description",product.Description);
        }else if (obj is StoreClass store)
        {
            command.Parameters.AddWithValue("@Name",store.Name);
            command.Parameters.AddWithValue("@OwnerId",store.OwnerId);
            command.Parameters.AddWithValue("@Balance",store.Balance);
        }

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
    public static int SqlScalarClass(string query,UserClass User)
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
        object ScalarObj = command.ExecuteScalar();
        int i = Convert.ToInt32(ScalarObj);
        Config.conn.Close();
        return i;
    }
    public static UserClass SqlReadClass()
    {
        MySqlCommand command = new MySqlCommand();
        MySqlDataReader reader = command.ExecuteReader();

        Config.conn.Open();

        UserClass user = new UserClass();
        while (reader.Read())
        {
            user.Email = Convert.ToString(reader["Email"]);
            user.Password = Convert.ToString(reader["Password"]);
        }
        Config.conn.Close();
        return user;
    }
}