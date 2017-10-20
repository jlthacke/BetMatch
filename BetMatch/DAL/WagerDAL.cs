using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetMatch.Models.Wager;
using System.Data.SqlClient;

namespace BetMatch.DAL.Wager
{
    public class WagerRepository : Controller
    {
        // Queries Database to manage wagers
        public List<WagerModel> GetWagers(int mode, string userName)
        {
            // Model to be populated
            List<WagerModel> wagers = new List<WagerModel>();

            // Get connection string
            string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);

            // If any part errors, go to catch clause
            try
            {
                // Create Connection
                string query = "sp_Wager_get";
                var command = new SqlCommand(query, con);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add("@mode", System.Data.SqlDbType.Int);
                command.Parameters["@mode"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@mode"].Value = mode;

                command.Parameters.Add("@userName", System.Data.SqlDbType.VarChar);
                command.Parameters["@userName"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@userName"].Value = userName;

                // Execute Query
                con.Open();
                var reader = command.ExecuteReader();

                // Store return data
                while (reader.Read())
                {
                    // Pull values from database
                    var id = reader["Id"];
                    var teamName_For = reader["TeamName_For"];
                    var teamName_Against = reader["TeamName_Against"];
                    var spread = reader["Spread"];
                    var gameTime = reader["GameTime"];
                    var wager = reader["Wager"];
                    var createDate = reader["CreateDate"];
                    var createUser = reader["CreateUser"];
                    var createUser_FullName = reader["CreateUser_FullName"];
                    var acceptDate = reader["AcceptDate"] == DBNull.Value ? null : reader["AcceptDate"];
                    var acceptUser = reader["AcceptUser"] == DBNull.Value ? "" : reader["AcceptUser"];
                    var acceptUser_FullName = reader["AcceptUser_FullName"] == DBNull.Value ? "" : reader["AcceptUser_FullName"];
                    var hasCreatorSettled = reader["HasCreatorSettled"] == DBNull.Value ? false : (int)reader["HasCreatorSettled"] == 0 ? false : true; // Posibilities: db null, 0, 1; errored when trying to cast as 0 or 1
                    var hasAcceptorSettled = reader["HasAcceptorSettled"] == DBNull.Value ? false : (int)reader["HasAcceptorSettled"] == 0 ? false : true; // Posibilities: db null, 0, 1; errored when trying to cast as 0 or 1

                    // Insert into model
                    wagers.Add(new WagerModel()
                    {
                        Id = (int) id,
                        TeamName_For = teamName_For.ToString(),
                        TeamName_Against = teamName_Against.ToString(),
                        Spread = (int) spread,
                        GameTime = gameTime.ToString(),
                        Wager = (int) wager,
                        CreateDate = (DateTime) createDate,
                        CreateUser = createUser.ToString(),
                        CreateUser_FullName = createUser_FullName.ToString(),
                        AcceptDate = (DateTime?) acceptDate,
                        AcceptUser = acceptUser.ToString(),
                        AcceptUser_FullName = acceptUser_FullName.ToString(),
                        HasCreatorSettled = (bool) hasCreatorSettled,
                        HasAcceptorSettled = (bool) hasAcceptorSettled
                    });
                }

                // Return model
                return wagers;
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

        // Edits wagers
        public int EditWager(int mode, WagerModel wager, string username)
        {
            int returnValue;

            // Get connection string
            string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);

            try // If any part errors, go to catch clause
            {
                // Create Connection
                string query = "sp_Wager_edit";
                var command = new SqlCommand(query, con);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.Add("@mode", System.Data.SqlDbType.Int);
                command.Parameters["@mode"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@mode"].Value = mode;

                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@id"].Value = wager.Id;

                command.Parameters.Add("@teamNameFor", System.Data.SqlDbType.VarChar);
                command.Parameters["@teamNameFor"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@teamNameFor"].Value = wager.TeamName_For;

                command.Parameters.Add("@teamNameAgainst", System.Data.SqlDbType.VarChar);
                command.Parameters["@teamNameAgainst"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@teamNameAgainst"].Value = wager.TeamName_Against;

                command.Parameters.Add("@spread", System.Data.SqlDbType.Int);
                command.Parameters["@spread"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@spread"].Value = wager.Spread;

                command.Parameters.Add("@gameTime", System.Data.SqlDbType.VarChar);
                command.Parameters["@gameTime"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@gameTime"].Value = wager.GameTime;

                command.Parameters.Add("@wager", System.Data.SqlDbType.Int);
                command.Parameters["@wager"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@wager"].Value = wager.Wager;

                command.Parameters.Add("@userName", System.Data.SqlDbType.VarChar);
                command.Parameters["@userName"].Direction = System.Data.ParameterDirection.Input;
                command.Parameters["@userName"].Value = username;

                command.Parameters.Add("@returnValue", System.Data.SqlDbType.Int);
                command.Parameters["@returnValue"].Direction = System.Data.ParameterDirection.ReturnValue;

                // Execute Query
                con.Open();
                command.ExecuteNonQuery();

                // Return model
                returnValue = (int)command.Parameters["@returnValue"].Value;
            }
            catch (Exception ex) // If any errors occur while querying database, the following message gets displayed to user
            {
                throw new Exception(string.Format("Exception Thrown Message - {0} Stack Trace - {1}", ex.Message, ex.StackTrace));
            }
            finally // Close connection
            {
                returnValue = -1;

                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return returnValue;
        }
    }
}