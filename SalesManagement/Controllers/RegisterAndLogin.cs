﻿using System;
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
    public class RegisterAndLogin : Controller
    {
        RegisterLoginAccessModel rlam = new RegisterLoginAccessModel();
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Register register) 
        {
            List<Register> registers = new List<Register>();
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpEmailEmail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@EmailID", register.EmailID.ToString());
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Register newemail = new Register();
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
                //var isExist = IsEmailExist(register.EmailID);
                //if (isExist)
                //{
                //    ModelState.AddModelError("EmailExist", "Email already exist");
                //    return View(register);
                //}
                register.Password = Crypto.Hash(register.Password);
                register.ConfirmPassword = Crypto.Hash(register.ConfirmPassword);
                rlam.AddLoginForm(register);
                return Ok(new { message = $"The Register is Successfully Done" });

            }
            return BadRequest(new { message = $"Model Is Not Valid" });
        }
        //[NonAction]
        //public bool IsEmailExist(string emailID)
        //{
        //    List<Register> registers = new List<Register>();
        //    using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
        //    {
        //        var v = registers.Where(a => a.EmailID == emailID).FirstOrDefault();
        //        return v != null;
        //    }
        //}
    }
}

