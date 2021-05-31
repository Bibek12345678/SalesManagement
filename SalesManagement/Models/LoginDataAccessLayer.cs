using SalesManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class LoginDataAccessLayer
    {
        public void AddLogin(UserLogin userlogin)
        {
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpLoginIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserEmail", userlogin.UserEmail.ToString());
                cmd.Parameters.AddWithValue("@Password", userlogin.Password.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}
