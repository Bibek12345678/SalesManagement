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
    public class AdminRegisterController : Controller
    {
        private readonly IAdminRegisterAccessLayer _ialcl = null;
        private readonly IUtilityServices _utilityServices = null;    
        public AdminRegisterController(IAdminRegisterAccessLayer ialcl, IUtilityServices utilityServices)
        {
            _ialcl = ialcl;
            _utilityServices = utilityServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Models.AdminRegister adminRegister)
        {
            List<Models.AdminRegister> adminregisters = new List<Models.AdminRegister>();
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpAdminEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@AdminEmail", adminRegister.Email.ToString());
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Models.AdminRegister newemail = new Models.AdminRegister();
                    {
                        newemail.AdminID = reader.GetInt32(0);
                        newemail.Email = reader.GetString(1);
                        adminregisters.Add(newemail);
                    }
                }
                con.Close();
            }
            var dup = adminregisters.Where(x => x.Email == adminRegister.Email).ToList();
            if (dup.Count() > 0)
            {
                ModelState.AddModelError("EmailExist", $"EmailID {adminRegister.Email} added successfully");
                adminRegister.Email = null;
                TempData["msg"] = "The Email Id already exist";
                return BadRequest(new { message = "The Email is Already Added" });

            }

            if (ModelState.IsValid)
            {
                adminRegister.Password = Crypto.Hash(adminRegister.Password);
                _ialcl.AddAdminRegister(adminRegister);
                return Ok(new { message = "The Register is successfully Created" });
            }
            else
            {
                return BadRequest(new { message = "Sorry Enter the valid Information" });
            }
           
        }
    }
}
