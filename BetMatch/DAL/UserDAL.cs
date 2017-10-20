using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetMatch.Models.User;
using System.Data.SqlClient;

namespace BetMatch.DAL.User
{
    public class UserRepository : Controller
    {
        // Queries Database to get users
        public List<UserModel> GetUsers(int mode)
        {
            // Model to be populated
            List<UserModel> users = new List<UserModel>();

            // Get connection string
            string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);

            // If any part errors, go to catch clause
            try
            {
                // Create Connection
                string query = "sp_User_get";
                var command = new SqlCommand(query, con);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add("@mode", System.Data.SqlDbType.Int);
                command.Parameters["@mode"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@mode"].Value = mode;

                // Execute Query
                con.Open();
                var reader = command.ExecuteReader();

                // Store return data
                while (reader.Read())
                {
                    // Pull values from database
                    var userName = reader["UserName"];
                    var email = reader["Email"];
                    var fullName = reader["FullName"];

                    // Insert into model
                    users.Add(new UserModel()
                    {
                        UserName = userName.ToString(),
                        Email = email.ToString(),
                        FullName = fullName.ToString()
                    });
                }

                // Return model
                return users;
            }
            catch (Exception ex) // If any errors occur while querying database, the following message gets displayed to user
            {
                throw new Exception(string.Format("Exception Thrown Message - {0} Stack Trace - {1}", ex.Message, ex.StackTrace));
            }
            finally // Close connection
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }
    }
}