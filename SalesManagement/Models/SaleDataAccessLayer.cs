using SalesManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class SaleDataAccessLayer : ISaleDataAccessLayer
    {
        public void AddSale(Sale sale)
        {

            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpSaleIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", sale.ProductName.ToString());
                // cmd.Parameters.AddWithValue("@ProductID", sale.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", sale.CustomerName.ToString());
                // cmd.Parameters.AddWithValue("@CustomerID", sale.CustomerID);
                cmd.Parameters.AddWithValue("@Quantity", sale.Quantity);
                //cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);
                //       cmd.Parameters.AddWithValue("@Rate", sale.Rate);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public IEnumerable<Sale> GetAllSaleDetails()

        {
            List<Sale> lstSales = new List<Sale>();
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpSalesSel", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var sale = new Sale();
                    sale.SaleID = Convert.ToInt32(rdr["SaleId"]);
                    sale.ProductName = rdr["ProductName"].ToString();
                    sale.CustomerName = rdr["CustomerName"].ToString();
                    sale.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    //  sale.SaleDate = Convert.ToDateTime(rdr["SaleDate"]);
                    sale.Rate = Convert.ToInt32(rdr["Rate"]);
                    sale.Total = Convert.ToInt32(rdr["Total"]);
                    sale.InvoiceID = rdr["InvoiceID"].ToString();
                    lstSales.Add(sale);
                }

                con.Close();
            }
            return lstSales;
        }

        public Sale GetSaleByID(int? id)
        {
            Sale sale = new Sale();
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpSaleByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaleID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sale.SaleID = Convert.ToInt32(rdr["SaleID"]);
                    sale.ProductID = Convert.ToInt32(rdr["ProductID"]);
                    sale.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                    sale.Quantity = Convert.ToInt32(rdr["Quantity"]);
                    sale.Rate = Convert.ToInt32(rdr["Rate"]);
                }
            }
            return sale;
        }
        public void UpdateSale(Sale sale)
        {
            using (SqlConnection con = new SqlConnection(UtilityServices.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SpSaleUpd", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SaleID", sale.SaleID);
                cmd.Parameters.AddWithValue("@ProductID", sale.ProductID);
                cmd.Parameters.AddWithValue("@CustomerID", sale.CustomerID);
                cmd.Parameters.AddWithValue("@Quantity", sale.Quantity);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
