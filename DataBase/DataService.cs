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
        }else if (obj is AdvertisementClass Ad)
        {
            command.Parameters.AddWithValue("@Id",Ad.Id);
            command.Parameters.AddWithValue("@ProductId",Ad.ProductId);
            command.Parameters.AddWithValue("@StoreId",Ad.StoreId);
            command.Parameters.AddWithValue("@Price",Ad.Price);
            command.Parameters.AddWithValue("@Discount",Ad.Discount);
            command.Parameters.AddWithValue("@StockQuantity",Ad.StockQuantity);
            command.Parameters.AddWithValue("@Description",Ad.Description);
        }else if (obj is CartItemClass cartItem)
        {
            command.Parameters.AddWithValue("@Id",cartItem.Id);
            command.Parameters.AddWithValue("@CartId",cartItem.CartId);
            command.Parameters.AddWithValue("@AdvertisementId",cartItem.AdvertisementId);
            command.Parameters.AddWithValue("@Quantity",cartItem.Quantity);
        }else if (obj is CartClass cart)
        {
            command.Parameters.AddWithValue("@CartId",cart.Id);
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
    public static int SqlScalarClass(string query,object obj)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        if(obj is UserClass User)
        {
            command.Parameters.AddWithValue("@id",User.Id);
            command.Parameters.AddWithValue("@Email",User.Email);
            command.Parameters.AddWithValue("@Name",User.Name);
            command.Parameters.AddWithValue("@Password",User.Password);
            command.Parameters.AddWithValue("@Adress",User.Adress);
            command.Parameters.AddWithValue("@PhoneNumber",User.PhoneNumber);
            command.Parameters.AddWithValue("@BirthDate",User.BirthDate);
        }else if (obj is AdvertisementClass Ad)
        {
            command.Parameters.AddWithValue("@Id",Ad.Id);
            command.Parameters.AddWithValue("@ProductId",Ad.ProductId);
            command.Parameters.AddWithValue("@StoreId",Ad.StoreId);
            command.Parameters.AddWithValue("@Price",Ad.Price);
            command.Parameters.AddWithValue("@Discount",Ad.Discount);
            command.Parameters.AddWithValue("@StockQuantity",Ad.StockQuantity);
            command.Parameters.AddWithValue("@Description",Ad.Description);
        }

        Config.conn.Open();
        object ScalarObj = command.ExecuteScalar();
        int i = Convert.ToInt32(ScalarObj);
        Config.conn.Close();
        return i;
    }
    public static UserClass SqlReadUser()
    {
        MySqlCommand command = new MySqlCommand();

        Config.conn.Open();
        UserClass user = new UserClass();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            user.Email = Convert.ToString(reader["Email"]);
            user.Password = Convert.ToString(reader["Password"]);
        }
        Config.conn.Close();
        return user;
    }
    public static AdvertisementClass SqlReadAd(string query)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        Config.conn.Open();

        AdvertisementClass ad = new AdvertisementClass();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ad.Id = Convert.ToInt32(reader["Id"]);
            ad.ProductId = Convert.ToInt32(reader["ProductId"]);
            ad.StoreId = Convert.ToInt32(reader["StoreId"]);
            ad.Price = Convert.ToDecimal(reader["Price"]);
            ad.Discount = Convert.ToDecimal(reader["Discount"]);
            ad.StockQuantity = Convert.ToInt32(reader["StockQuantity"]);
            ad.Description = Convert.ToString(reader["Description"]);
        }

        Config.conn.Close();
        return ad;
    }
    public static CartItemClass SqlReadCartItem(string query)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

        Config.conn.Open();

        CartItemClass cartitem = new CartItemClass();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            cartitem.Id = Convert.ToInt32(reader["Id"]);
            cartitem.CartId = Convert.ToInt32(reader["CartId"]);
            cartitem.AdvertisementId = Convert.ToInt32(reader["AdvertisementId"]);
            cartitem.Quantity = Convert.ToInt32(reader["Quantity"]);
        }

        Config.conn.Close();
        return cartitem;
    }
    public static void sqlReadValue(string query)
    {
        MySqlCommand command = new MySqlCommand(query,Config.conn);

    }
}