using SalesManagement.Models;
using SalesManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.ServiceLayer
{
    public class AdminLoginAccessLayer : IAdminLoginAccessLayer
    {
        private readonly IUtilityServices _utilityServices;
        public AdminLoginAccessLayer(IUtilityServices utilityServices)
        {
            _utilityServices = utilityServices;
        }
        public void AddAdmin(AdminLogin adminLogin)
        {
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpLoginIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailID", adminLogin.EmailID.ToString());
                cmd.Parameters.AddWithValue("@Password", adminLogin.Password.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}
