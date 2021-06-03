using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagement.Models;
using SalesManagement.Services;

namespace SalesManagement.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleDataAccessLayer _isdal = null;
        private readonly IUtilityServices _utilityServices;
        public SaleController(ISaleDataAccessLayer isdal , IUtilityServices utilityServices)
        {
            _isdal = isdal;
            _utilityServices = utilityServices;
        }
        // GET: Sale
        public IActionResult Index()
        {
            List<Sale> sales = new List<Sale>();
            sales = _isdal.GetAllSaleDetails().ToList();
            return View(sales);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Sale obj = new Sale();
            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
           
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerSel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
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
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product newPro = new Product();
                    {
                        newPro.ProductID = reader.GetInt32(0);
                        newPro.ProductName = reader.GetString(1);
                        products.Add(newPro);
                    }
                }
                con.Close();
            }

            obj.ProductList = products;
            obj.CustomerList = customers;
            ViewBag.Products = new SelectList(products, "ProductID", "ProductName");
            ViewBag.Customers = new SelectList(customers, "CustomerID", "CustomerName");
            return View(obj);

        }

        [HttpPost]
        public IActionResult Create(Sale objSale)
        {
            try{
                _isdal.AddSale(objSale);

                return Ok(new { message = $"Sale  Table added successfully" });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return BadRequest(new { message = "Model is not valid" });
        }
           
                //}
        
      
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Sale obj = new Sale();
            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
       
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerSel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
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
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                // cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product newPro = new Product();
                    {
                        newPro.ProductID = reader.GetInt32(0);
                        newPro.ProductName = reader.GetString(1);
                        products.Add(newPro);
                    }
                }
                con.Close();
            }

            obj.ProductList = products;
            obj.CustomerList = customers;
            ViewBag.Products = new SelectList(products, "ProductID", "ProductName");
            ViewBag.customers = new SelectList(customers, "CustomerID", "CustomerName");
            //ViewBag.Products = new SelectList(products, "ProductID", "ProductName");
            if (id == null)
            {
                return NotFound();
            }
            Sale sale = _isdal.GetSaleByID(id);
            if (sale == null)
            {
                return NotFound();
            }
            return View(sale);
        }
        [HttpPost]
        public IActionResult Edit(int? id, Sale objSale)
        {
            if (id != objSale.SaleID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _isdal.UpdateSale(objSale);
                return RedirectToAction("Index");
            }
            return View(_isdal);

        }
    }
}
