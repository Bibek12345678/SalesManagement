using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Models;
using SalesManagement.Services;

namespace SalesManagement.Controllers
{
    public class ProductController : Controller
    {
        //  ProductDataAccessLayer pdal = new ProductDataAccessLayer();
        private readonly IProductDataAccessLayer _ipdal = null;
        public ProductController(IProductDataAccessLayer ipdal)
        {
            _ipdal = ipdal;
        }
     
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = _ipdal.GetAllProducts().ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Product objProduct)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductProduct ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@ProductName", objProduct.ProductName.ToString());
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
            var dup = products.Where(x => x.ProductName == objProduct.ProductName).ToList();
            if (dup.Count() > 0)
            {
               // ModelState.AddModelError(" ", "The value is already added");
                objProduct.ProductName = null;
                return BadRequest(new { message = "The Product is Already Added" });
            }

            if (ModelState.IsValid)
            {
                _ipdal.AddProduct(objProduct);
                return Ok(new { message = $"Product {objProduct.ProductName} added successfully" });
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
            Product product = _ipdal.GetProductData(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] Product objProduct)
        {
            if (id != objProduct.ProductID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _ipdal.UpdateProduct(objProduct);
                return RedirectToAction("Index");
            }
            return View(_ipdal);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product objcustomer = _ipdal.GetProductData(id);

            if (objcustomer == null)
            {
                return NotFound();
            }
            return View(objcustomer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            _ipdal.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
