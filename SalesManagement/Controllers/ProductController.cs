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
    public class ProductController : Controller
    {
        ProductDataAccessLayer objProductDAL = new ProductDataAccessLayer();
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = objProductDAL.GetAllProducts().ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product());
        }
    


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product objProduct)
        {
            List<Product> products = new List<Product>();
            String CS = "Data Source=DESKTOP-REU4K57; Initial Catalog = SaleTransaction; User ID = sa; Password = bibek;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblProduct where ProductName = @ProductName ", con);
                cmd.CommandType = CommandType.Text;
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
                ModelState.AddModelError(" ", "The value is already added");
                objProduct.ProductName = null;
                return View(objProduct);
            }
            if (ModelState.IsValid)
            {
                objProductDAL.AddProduct(objProduct);
                return RedirectToAction("Index");
            }
            return View(objProduct);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = objProductDAL.GetProductData(id);

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
                objProductDAL.UpdateProduct(objProduct);
                return RedirectToAction("Index");
            }
            return View(objProductDAL);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product objcustomer = objProductDAL.GetProductData(id);

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
            objProductDAL.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
