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

namespace SalesManagement.Controllers
{
    public class SaleController : Controller
    {
        String CS = "Data Source=DESKTOP-REU4K57; Initial Catalog = SaleTransaction; User ID = sa; Password = bibek;Integrated Security=True";
        SaleDataAccessLayer sdal = new SaleDataAccessLayer();
        // GET: Sale
        public IActionResult Index()
        {
            List<Sale> sales = new List<Sale>();
            sales = sdal.GetAllSaleDetails().ToList();
            return View(sales);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Sale obj = new Sale();
            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
           
            using (SqlConnection con = new SqlConnection(CS))
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
            using (SqlConnection con = new SqlConnection(CS))
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
        public IActionResult Create(Sale sale)
        {
            // sale.SaleDate = DateTime.Now;
            sdal.AddSale(sale);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Sale obj = new Sale();
            List<Product> products = new List<Product>();
            List<Customer> customers = new List<Customer>();
       
            using (SqlConnection con = new SqlConnection(CS))
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
            using (SqlConnection con = new SqlConnection(CS))
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
            Sale sale = sdal.GetSaleByID(id);
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
                sdal.UpdateSale(objSale);
                return RedirectToAction("Index");
            }
            return View(sdal);

        }
    }
}
