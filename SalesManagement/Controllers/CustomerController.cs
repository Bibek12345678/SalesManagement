
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SalesManagement.Models;
using SalesManagement.Services;

namespace SalesManagement.Controllers
{
    public class CustomerController : Controller
    {
       
        private readonly ICustomerDataAccessLayer _cdal = null;
        private readonly IUtilityServices _utilityServices = null;
        public CustomerController(ICustomerDataAccessLayer icdal , IUtilityServices utilityServices)
        {
            _cdal = icdal;
            _utilityServices = utilityServices;
        }

        public IActionResult Index()
        {
            List<Customer> lstCustomer = new List<Customer>();
            lstCustomer = _cdal.GetAllCustomer().ToList();
            return View(lstCustomer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer objCustomer)
        {
            //String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            // int Count = 0;
            List<Customer> customers = new List<Customer>();
            
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerCustomer ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@CustomerName", objCustomer.CustomerName.ToString());
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer newCus = new Customer();
                    {
                        newCus.CustomerID = reader.GetInt32(0);
                        newCus.CustomerName = reader.GetString(1);
                        customers.Add(newCus);
                    }
                }
                con.Close();
            }
            var dup = customers.Where(x => x.CustomerName == objCustomer.CustomerName).ToList();
            if (dup.Count() > 0)
            {
                return BadRequest(new { message = "The Customer Name has Already been Added" });
            }
            if (ModelState.IsValid)
            {
                _cdal.AddCustomer(objCustomer);
                return Ok(new { message = $"Product {objCustomer.CustomerName} added successfully" });
            }
            return BadRequest(new { message = "Model is not valid" });
        }




        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = _cdal.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _cdal.UpdateCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = _cdal.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _cdal.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}
