using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        public ProductService()
        {
            iProductRepository = new ProductRepository();
        }
        private readonly IProductRepository iProductRepository;
        public void DeleteProduct(Product p) => iProductRepository.DeleteProduct(p);

        public Product GetProductById(int id) => iProductRepository.GetProductById(id);

        public List<Product> GetProducts() => iProductRepository.GetProducts();

        public void SaveProduct(Product p) => iProductRepository.SaveProduct(p);

        public void UpdateProduct(Product p) => iProductRepository.UpdateProduct(p);
    }
}
