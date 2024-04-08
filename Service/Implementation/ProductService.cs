using EShop.DomainEntities.Domain;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IRepository<Product> productRepository,
            ILogger<ProductService> logger,
            IRepository<ProductInShoppingCart> productInShoppingCartRepository,
            IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _logger = logger;
        }


        public void CreateNewProduct(Product product)
        {
            _productRepository.Insert(product);
        }

        public void DeleteProduct(Guid? id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetDetailsForProduct(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public void UpdateExistingProduct(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
