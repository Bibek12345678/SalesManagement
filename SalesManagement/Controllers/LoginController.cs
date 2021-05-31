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
    public class LoginController : Controller
    {
        LoginDataAccessLayer ldal = new LoginDataAccessLayer();

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
            Register register = new Register();
           ;
            List<Register> registers = new List<Register>();
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpLoginValid", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                /*cmd.Parameters.AddWithValue("@EmailID", userLogin.UserEmail.ToString());
                cmd.Parameters.AddWithValue("@Password", userLogin.Password.ToString());*/
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Register ulogin = new Register();
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

            //if (ModelState.IsValid)
            //{
            //    userLogin.Password = Crypto.Hash(userLogin.Password);
            //   // ldal.AddLogin(userLogin);

            //}

            var user = registers.Where(query => query.EmailID.Equals(userLogin.UserEmail) && query.Password.Equals(userLogin.Password)).ToList();
           // var userxyz = registers.Where(x => x.UserId == 19);
           // var dup = registers.Where(x => x.EmailID == register.EmailID).ToList();
           
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


