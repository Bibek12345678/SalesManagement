using SalesManagement.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SalesManagement.Models
{
    public class ProductDataAccessLayer : IProductDataAccessLayer
    {
        private readonly IUtilityServices _utilityServices ;
        public ProductDataAccessLayer(IUtilityServices utilityServices)
        {
            _utilityServices = utilityServices;
        }

        public void AddProduct(Product product)
        {

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductIns", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductName", product.ProductName.ToString());
                cmd.Parameters.AddWithValue("@Rate", product.Rate);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> lstCustomer = new List<Product>();

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductSel", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var product = new Product();
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Rate = Convert.ToInt32(rdr["Rate"]);
                    lstCustomer.Add(product);
                }
                con.Close();
            }
            return lstCustomer;
        }
        //To Update the records of a particluar Product  
        public void UpdateProduct(Product product)
        {

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductUpd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Rate", product.Rate);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Product GetProductData(int? id)
        {
            Product product = new Product();

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("SpProductByID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    product.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    product.ProductName = rdr["ProductName"].ToString();
                    product.Rate = Convert.ToInt32(rdr["Rate"]);
                }
            }
            return product;
        }
        public void DeleteProduct(int? id)
        {

            using (SqlConnection con = new SqlConnection(_utilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpProductDel", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
      
    }
}
