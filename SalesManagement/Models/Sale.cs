using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class Sale
    {
        public int SaleID { get; set; }

        public int ProductID { get; set; }

        public int CustomerID { get; set; }

        public string ProductName { get; set; }

        public string CustomerName { get; set; }

        public IList<Product> ProductList { get; set; }

        public IList<Customer> CustomerList { get; set; }

        public DateTime? SaleDate { get; set; }
        [Range(1, 50, ErrorMessage = "Value lies outside the 1 to 50 range")]
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int? Total { get; set; }
        public string InvoiceID { get; set; }
    }
}
