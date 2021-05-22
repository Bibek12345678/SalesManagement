using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagement.Models;

namespace SalesManagement.Controllers
{
    public class InvoiceController : Controller
    {
        String CS = "Data Source=DESKTOP-REU4K57; Initial Catalog = SaleTransaction; User ID = sa; Password = bibek;Integrated Security=True";
        InvoiceDataAccessLayer idal = new InvoiceDataAccessLayer();
        // GET: Invoice
        public IActionResult Index()
        {
            List<Invoice> invoice = new List<Invoice>();
            invoice = idal.GetAllInvoice().ToList();
            return View(invoice);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Invoice invoice = new Invoice();
            List<Customer> customers = new List<Customer>();
      
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerSel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer customer = new Customer();
                    {
                        customer.CustomerID = rdr.GetInt32(0);
                        customer.CustomerName = rdr.GetString(1);
                        customers.Add(customer);
                    }
                }
                con.Close();
                invoice.Customerlist = customers;
                ViewBag.Customers = new SelectList(customers, "CustomerID", "CustomerName");
                return View(invoice);
            }
        }
        [HttpPost]
        public IActionResult Create(Invoice invoice)
        {
            invoice.InvoiceDate = DateTime.Now;
            idal.AddInvoice(invoice);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Invoice objInvoice = idal.GetInvoiceById(id);
            if (objInvoice == null)
            {
                return NotFound();
            }
            return View(objInvoice);
        }
    }
}
