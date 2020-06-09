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

namespace Wesselink_Evenementen_Desktop_Version.Classes
{
    class UsersConfig
    {
        public SqlConnection sqlConnection = new SqlConnection("SERVER=DESKTOP-D4DVHOE\\SQLEXPRESS;Database=Wesselink_Evenementen;Trusted_Connection=True;MultipleActiveResultSets=True"); 
        public static string Salt { get; set; }
        public static string Hash { get; set; }
        public string Pwd { get; set; }

        //Creates a hash from password and adds a salt of 128bytes to the hash and then encrypts it with sha256.
        public void CreateHash(string password)
        {
            try
            {
                var InputBuffer = new List<byte>(Encoding.Unicode.GetBytes(password));

                var HashOnly = Encoding.Unicode.GetBytes(password);

                Console.WriteLine("This is the HASH ONLY " + BitConverter.ToString(HashOnly).Replace("-", string.Empty));

                var NewSalt = new byte[128];

                using (var RandomGenerator = RandomNumberGenerator.Create())
                {
                    RandomGenerator.GetBytes(NewSalt);
                }
                InputBuffer.AddRange(NewSalt);

                string test = Convert.ToBase64String(NewSalt);

                Console.WriteLine("This is the SALT ONLY " + test.Replace("-", string.Empty));

                byte[] HashedBytes;

                using (var Hasher = new SHA256Managed())
                {
                    HashedBytes = Hasher.ComputeHash(InputBuffer.ToArray());
                }

                Console.WriteLine("This is the SALT AND HASH COMBINED " + BitConverter.ToString(HashedBytes).Replace("-", string.Empty));
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
                    Console.WriteLine(BitConverter.ToString(HashedBytes).Replace("-", string.Empty));
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
                    string query = $"SELECT Name, SaltPwd, HashPwd FROM WesselinkUsers WHERE Name='{Name}' AND SaltPwd='{Salt}' AND HashPwd='{Pwd}'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = query;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        if (Name.Equals(dataReader["Name"].ToString()) && Pwd.Equals(dataReader["HashPwd"].ToString()))
                        {
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
    }
}
