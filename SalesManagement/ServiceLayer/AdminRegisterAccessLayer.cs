using SalesManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class AdminRegisterAccessLayer : IAdminRegisterAccessLayer
    {
        private readonly IUtilityServices _utilityServices;

        public AdminRegisterAccessLayer(IUtilityServices utilityServices)
        {
            _utilityServices = utilityServices;
        }
        public void AddAdminRegister(AdminRegister adminRegister)
        {

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpAdminRegister", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", adminRegister.Name.ToString());
                cmd.Parameters.AddWithValue("@AdminEmail", adminRegister.Email.ToString());
                cmd.Parameters.AddWithValue("@Password", adminRegister.Password.ToString());
                cmd.Parameters.AddWithValue("@Phoneno", adminRegister.phoneno.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}
