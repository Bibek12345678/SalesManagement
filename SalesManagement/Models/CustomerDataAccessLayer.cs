using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class CustomerDataAccessLayer
    {
        String CS = "Data Source=DESKTOP-REU4K57; Initial Catalog = SaleTransaction; User ID = sa; Password = bibek;Integrated Security=True";
        public void AddCustomer(Customer customer)
        {

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerIns", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        public IEnumerable<Customer> GetAllCustomer()
        {
            List<Customer> lstCustomer = new List<Customer>();

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerSel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                    customer.CustomerName = rdr["CustomerName"].ToString();
                    lstCustomer.Add(customer);
                }
                con.Close();
            }
            return lstCustomer;
        }
        public Customer GetCustomerById(int? id)
        {
            Customer customer = new Customer();

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerGetByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    customer.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                    customer.CustomerName = rdr["CustomerName"].ToString();
                }
                return customer;
            }
        }
        public void UpdateCustomer(Customer customer)
        {

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerUpd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("CustomerName", customer.CustomerName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void DeleteCustomer(int? id)
        {

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SpCustomerDel", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
    }
}

