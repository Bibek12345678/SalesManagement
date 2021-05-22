using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Models;

namespace SalesManagement.Controllers
{
    public class CustomerController : Controller
    {
        String CS = "Data Source=DESKTOP-REU4K57; Initial Catalog = SaleTransaction; User ID = sa; Password = bibek;Integrated Security=True";
        CustomerDataAccessLayer cdal = new CustomerDataAccessLayer();
        // GET: Customer
        public IActionResult Index()
        {
            List<Customer> lstCustomer = new List<Customer>();
            lstCustomer = cdal.GetAllCustomer().ToList();
            return View(lstCustomer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }
        [HttpPost]
        public IActionResult Create(Customer objCustomer)
        {
            //String CS = ConfigurationManager.ConnectionStrings["DBMS"].ConnectionString;
            // int Count = 0;
            List<Customer> customers = new List<Customer>();
            
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCustomer where CustomerName = @CustomerName ", con);
                cmd.CommandType = CommandType.Text;
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
                ModelState.AddModelError(" ", "The value is already added");
                objCustomer.CustomerName = null;
                return View(objCustomer);
            }
            if (ModelState.IsValid)
            {
                cdal.AddCustomer(objCustomer);
                return RedirectToAction("Index");
            }
            return View(objCustomer);

        }




        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = cdal.GetCustomerById(id);
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
                cdal.UpdateCustomer(customer);
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
            Customer customer = cdal.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            cdal.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}
