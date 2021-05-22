using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class InvoiceDataAccessLayer
    {
        String CS = "Data Source=DESKTOP-REU4K57; Initial Catalog = SaleTransaction; User ID = sa; Password = bibek;Integrated Security=True";
        public void AddInvoice(Invoice invoice)
        {
           
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpInvoiceIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerID", invoice.CustomerID);
                // cmd.Parameters.AddWithValue("InvoiceNumber", invoice.InvoiceNumber);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public IEnumerable<Invoice> GetAllInvoice()
        {
            List<Invoice> lstInvoices = new List<Invoice>();
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpInvoiceSel", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Invoice invoice = new Invoice();
                    invoice.InvoiceID = Convert.ToInt32(rdr["InvoiceID"]);
                    //invoice.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                    invoice.CustomerName = rdr["CustomerName"].ToString();
                    // invoice.InvoiceNumber = Convert.ToInt32(rdr["InvoiceNumber"]);
                    invoice.InvoiceDate = Convert.ToDateTime(rdr["InvoiceDate"]);
                    invoice.Total = Convert.ToInt32(rdr["Total"]);
                    lstInvoices.Add(invoice);
                }
                con.Close();

            }
            return lstInvoices;
        }
        public Invoice GetInvoiceById(int? id)
        {
            Invoice invoice = new Invoice();
            
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpInvoiceByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceID", id);
                // cmd.Parameters.Clear();
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    invoice.InvoiceID = Convert.ToInt32(rdr["InvoiceID"]);
                    invoice.CustomerName = rdr["CustomerName"].ToString();
                    invoice.Total = Convert.ToInt32(rdr["Total"]);
                }

            }
            return invoice;
        }

    }
}
