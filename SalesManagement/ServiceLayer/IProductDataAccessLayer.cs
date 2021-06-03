using System.Collections.Generic;

namespace SalesManagement.Models
{
    public interface IProductDataAccessLayer
    {
        void AddProduct(Product product);
        void DeleteProduct(int? id);
        IEnumerable<Product> GetAllProducts();
        Product GetProductData(int? id);
        void UpdateProduct(Product product);
        
        
    }
}