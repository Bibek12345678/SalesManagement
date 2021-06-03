using SalesManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class UserRegisterAccessLayer : IUserRegisterAccessLayer
    {
        private readonly IUtilityServices _utilityServices;
        public UserRegisterAccessLayer(IUtilityServices utilityServices)
        {
            _utilityServices = utilityServices;
        }
        public void AddLoginForm(UserRegister register)
        {

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpRegisterAndLoginIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", register.FirstName.ToString());
                cmd.Parameters.AddWithValue("@LastName", register.LastName.ToString());
                cmd.Parameters.AddWithValue("@EmailID", register.EmailID.ToString());
                cmd.Parameters.AddWithValue("@DateOfBirth", register.DateOfBirth.ToOADate());
                cmd.Parameters.AddWithValue("@Password", register.Password.ToString());
                cmd.Parameters.AddWithValue("@ConfirmPassword", register.ConfirmPassword.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}
