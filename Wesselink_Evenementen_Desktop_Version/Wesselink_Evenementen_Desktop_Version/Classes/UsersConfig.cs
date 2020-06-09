using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wesselink_Evenementen_Desktop_Version.Classes
{
    class AccountDetails
    {
        public static string Name { get; set; }
        public static string Surname { get; set; }
        public static string Email { get; set; }
        public static string PhoneNumber { get; set; }
        public static string Barkeeper { get; set; }
        public static string Receptionist { get; set; }
        public static string Waiter { get; set; }
        public static string Host { get; set; }
    }

    class UsersConfig
    {
        public SqlConnection sqlConnection = new SqlConnection("SERVER=DESKTOP-D4DVHOE\\SQLEXPRESS;Database=Wesselink_Evenementen;Trusted_Connection=True;MultipleActiveResultSets=True");
        public static string Salt { get; set; }
        public static string Hash { get; set; }
        public string Pwd { get; set; }
        public static int Id { get; set; }

        //Creates a hash from password and adds a salt of 128bytes to the hash and then encrypts it with sha256.
        public void CreateHash(string password)
        {
            try
            {
                var InputBuffer = new List<byte>(Encoding.Unicode.GetBytes(password));

                var HashOnly = Encoding.Unicode.GetBytes(password);
                var NewSalt = new byte[128];

                using (var RandomGenerator = RandomNumberGenerator.Create())
                {
                    RandomGenerator.GetBytes(NewSalt);
                }
                InputBuffer.AddRange(NewSalt);

                string test = Convert.ToBase64String(NewSalt);

                byte[] HashedBytes;

                using (var Hasher = new SHA256Managed())
                {
                    HashedBytes = Hasher.ComputeHash(InputBuffer.ToArray());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Gets the salt from the database and returns it to a Base64string,
        //Then the input pwd will be converted to bytes "Hash",
        //Then The salt and hashed pwd will be merged together and will be encrypted with SHA256.
        public void GetSaltFromDb(string Name, string Password)
        {
            try
            {
                SqlCommand cmd = new SqlCommand($"SELECT SaltPwd, HashPwd FROM WesselinkUsers WHERE name=@Name", sqlConnection);
                cmd.Parameters.AddWithValue("@Name", Name);
                sqlConnection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Salt = reader.GetString(0);
                        Hash = reader.GetString(1);
                    }
                }
                sqlConnection.Close();

                if (Salt != String.Empty && Password != String.Empty)
                {
                    var InputBuffer = new List<byte>(Encoding.Unicode.GetBytes(Password));
                    InputBuffer.AddRange(Convert.FromBase64String(Salt));

                    byte[] HashedBytes;
                    using (var Hasher = new SHA256Managed())
                    {
                        HashedBytes = Hasher.ComputeHash(InputBuffer.ToArray());
                    }
                    Pwd = BitConverter.ToString(HashedBytes).Replace("-", string.Empty);
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlConnection.Close();
            }
        }

        //Runs the GetSaltFromDb method and returns the enterd password with salt from database.
        //Then a select query runs that search employee with enterd name, salt and password.
        //If login is success it will return true, this method runs to validate login.
        public bool ValidatePassword(string Name, string Password)
        {
            try
            {
                sqlConnection.Close();
                GetSaltFromDb(Name, Password);
                if (Pwd == Hash)
                {
                    sqlConnection.Open();
                    string query = $"SELECT Name, SaltPwd, HashPwd, Id FROM WesselinkUsers WHERE Name=@Name AND SaltPwd=@Salt AND HashPwd=@Pwd";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Salt", Salt);
                    cmd.Parameters.AddWithValue("@Pwd", Pwd);
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = query;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (Name.Equals(dataReader["Name"].ToString()) && Pwd.Equals(dataReader["HashPwd"].ToString()))
                        {
                            Id = dataReader.GetInt32(3);
                            sqlConnection.Close();
                            return true;
                        }
                        else
                        {
                            sqlConnection.Close();
                            return false;
                        }
                    }
                    sqlConnection.Close();
                }
                sqlConnection.Close();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlConnection.Close();
                return false;
            }
        }

        public List<string> GetAccountDetails(int Id)
        {
            try
            {
                sqlConnection.Open();
                string query = $"SELECT Name, Surname, Email, PhoneNumber, Barkeeper, Receptionist, Waiter, Host FROM WesselinkUsers WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Connection = sqlConnection;
                cmd.CommandText = query;

                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        AccountDetails.Name = dataReader.GetString(0);
                        AccountDetails.Surname = dataReader.GetString(1);
                        AccountDetails.Email = dataReader.GetString(2);
                        AccountDetails.PhoneNumber = dataReader.GetString(3);
                        AccountDetails.Barkeeper = dataReader.GetString(4);
                        AccountDetails.Receptionist = dataReader.GetString(5);
                        AccountDetails.Waiter = dataReader.GetString(6);
                        AccountDetails.Host = dataReader.GetString(7);
                    }
                }
                else
                {
                    Console.WriteLine("No data found");
                }
                sqlConnection.Close();
                List<string> accountDetails = new List<string>();
                accountDetails.Add(AccountDetails.Name);
                accountDetails.Add(AccountDetails.Surname);
                accountDetails.Add(AccountDetails.Email);
                accountDetails.Add(AccountDetails.PhoneNumber);
                accountDetails.Add(AccountDetails.Barkeeper.ToString());
                accountDetails.Add(AccountDetails.Receptionist.ToString());
                accountDetails.Add(AccountDetails.Waiter.ToString());
                accountDetails.Add(AccountDetails.Host.ToString());
                return accountDetails;

            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
