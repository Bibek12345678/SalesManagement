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
    public class UserLoginController : Controller
    {
        private readonly IUtilityServices _utilityServices;
     //   private readonly IUserLoginAccessLayer _ildac;
       public UserLoginController( IUtilityServices utilityServices)
        {
           // _ildac = ildac;
            _utilityServices = utilityServices;
        }
        public IActionResult Valid()
        {
            return View();
        }
            [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(UserLogin userLogin)
        {
            UserRegister register = new UserRegister();
           ;
            List<UserRegister> registers = new List<UserRegister>();
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpLoginValid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserRegister ulogin = new UserRegister();
                    {
                        ulogin.UserId = reader.GetInt32(0);
                        ulogin.EmailID = reader.GetString(1);
                        ulogin.Password = reader.GetString(2);
                        registers.Add(ulogin);
                    }
                }
                con.Close();
            }
            userLogin.Password = Crypto.Hash(userLogin.Password);

            var user = registers.Where(query => query.EmailID.Equals(userLogin.UserEmail) && query.Password.Equals(userLogin.Password)).ToList();
            if (user.Count() == 1)
            {

                 return Ok(new { message = "Login form filled up successfully" });
               
                //ModelState.AddModelError("EmailExist", $"EmailID {register.EmailID} added successfully");
                //register.EmailID = null;
                //TempData["msg"] = "The Email Id already exist";
                //return BadRequest(new { message = "The Email is Already Added" });

            }
            else
            {
                ModelState.AddModelError("Error", "Invalid UserName and Password");
                return RedirectToAction("Create");
            }
             
        }



        }
    }


