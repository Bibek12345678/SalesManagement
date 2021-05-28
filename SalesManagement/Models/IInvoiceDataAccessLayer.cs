using System.Collections.Generic;

namespace SalesManagement.Models
{
    public interface IInvoiceDataAccessLayer
    {
        void AddInvoice(Invoice invoice);
        IEnumerable<Invoice> GetAllInvoice();
        Invoice GetInvoiceById(int? id);
    }
}