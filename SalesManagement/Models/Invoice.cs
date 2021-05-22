using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int CustomerID { get; set; }
        public int InvoiceNumber { get; set; }
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public IList<Customer> Customerlist { get; set; }
        public DateTime? InvoiceDate { get; set; }
    }
}
