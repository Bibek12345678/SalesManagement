using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagement.Models;
using SalesManagement.Services;

namespace SalesManagement.Controllers
{
    public class InvoiceController : Controller
    {

        //InvoiceDataAccessLayer idal = new InvoiceDataAccessLayer();
        // GET: Invoice
        private readonly IInvoiceDataAccessLayer _iidal = null;
        private readonly IUtilityServices _utilityServices;
       public InvoiceController(IInvoiceDataAccessLayer iidal , IUtilityServices utilityServices)
        {
            _iidal = iidal;
            _utilityServices = utilityServices;
        }


        public IActionResult Index()
        {
            List<Invoice> invoice = new List<Invoice>();
            invoice = _iidal.GetAllInvoice().ToList();
            return View(invoice);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Invoice invoice = new Invoice();
            List<Customer> customers = new List<Customer>();
      
            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
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
            try
            {
                invoice.InvoiceDate = DateTime.Now;
                _iidal.AddInvoice(invoice);
               return Ok(new { message = $"Invoice of{invoice.CustomerName} added successfully" });
            }
           catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            return BadRequest(new { message = "Model is not valid" });
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Invoice objInvoice = _iidal.GetInvoiceById(id);
            if (objInvoice == null)
            {
                return NotFound();
            }
            return View(objInvoice);
        }
    }
}
