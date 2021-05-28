using System.Collections.Generic;

namespace SalesManagement.Models
{
    public interface ISaleDataAccessLayer
    {
        void AddSale(Sale sale);
        IEnumerable<Sale> GetAllSaleDetails();
        Sale GetSaleByID(int? id);
        void UpdateSale(Sale sale);
    }
}