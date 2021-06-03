using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Models;
using SalesManagement.ServiceLayer;
using SalesManagement.Services;

namespace SalesManagement.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly IUtilityServices _utilityServices;
   
       public AdminLoginController(IUtilityServices utilityServices)
        {
            _utilityServices = utilityServices;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AdminLogin adminLogin)
        {
            List<AdminRegister> adminRegisters = new List<AdminRegister>();
            using(SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpAdminLoginValid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AdminRegister alogin = new AdminRegister();
                    {
                        alogin.AdminID = reader.GetInt32(0);
                        alogin.Email = reader.GetString(1);
                        alogin.Password = reader.GetString(2);
                        adminRegisters.Add(alogin);
                    }
                    
                }
                con.Close();
            }
            adminLogin.Password = Crypto.Hash(adminLogin.Password);
            var admin = adminRegisters.Where(query => query.Email.Equals(adminLogin.EmailID) && query.Password.Equals(adminLogin.Password)).ToList();
            if (admin.Count() == 1)
            {
                return RedirectToAction("index", "product");
                //return Ok(new { message = "Login form filled up successfully" });
                //ModelState.AddModelError("EmailExist", $"EmailID {register.EmailID} added successfully");
                //register.EmailID = null;
                //TempData["msg"] = "The Email Id already exist";
                //return BadRequest(new { message = "The Email is Already Added" });

            }
            else {
                ModelState.AddModelError("Error", "Invalid UserName and Password");
                return RedirectToAction("Create");
            }
        }
    }
}
