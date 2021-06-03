using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Models;
using SalesManagement.Services;

namespace SalesManagement.Controllers
{
    public class UserRegisterController : Controller
    {
        private readonly IUtilityServices _utilityServices = null;
        private readonly IUserRegisterAccessLayer _irlam = null;

       public UserRegisterController(IUserRegisterAccessLayer irlam , IUtilityServices utilityServices)
        {
            _utilityServices = utilityServices;
            _irlam = irlam;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserRegister register) 
        {
            List<UserRegister> registers = new List<UserRegister>();
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpEmailEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@EmailID", register.EmailID.ToString());
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserRegister newemail = new UserRegister();
                    {
                        newemail.UserId = reader.GetInt32(0);
                        newemail.EmailID = reader.GetString(1);
                        registers.Add(newemail);
                    }
                }
                con.Close();
            }
            var dup = registers.Where(x => x.EmailID == register.EmailID).ToList();
            if (dup.Count() > 0)
            {
                ModelState.AddModelError("EmailExist", $"EmailID {register.EmailID} added successfully");
                 register.EmailID = null;
                TempData["msg"] = "The Email Id already exist";
                return BadRequest(new { message = "The Email is Already Added" });

            }
            if (ModelState.IsValid)
            {
                register.Password = Crypto.Hash(register.Password);
                register.ConfirmPassword = Crypto.Hash(register.ConfirmPassword);
                _irlam.AddLoginForm(register);
                return Ok(new { message = $"The Register is Successfully Done" });

            }
            return BadRequest(new { message = $"Model Is Not Valid" });
        }
    }
}

