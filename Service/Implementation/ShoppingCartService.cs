using EShop.DomainEntities.Domain;
using EShop.DomainEntities.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ProductsInOrder> _productInOrderRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
            IRepository<ProductInShoppingCart> productInShoppingCartRepository,
            IUserRepository userRepository,
            IRepository<Order> orderRepository,
            IRepository<ProductsInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }

        public bool AddToShoppingConfirmed(ProductInShoppingCart model, string userId)
        {
            var user = this._userRepository.Get(userId);
            var shoppingCart = user.ShoppingCart;
            ProductInShoppingCart itemToAdd = new ProductInShoppingCart
            {
                Id = Guid.NewGuid(),
                Product = model.Product,
                ProductId = model.ProductId,
                ShoppingCart = shoppingCart,
                ShoppingCartId = shoppingCart.Id,
                Quantity = model.Quantity
            };

            _productInShoppingCartRepository.Insert(itemToAdd);
            return true;
        }

        public bool deleteProductFromSoppingCart(string userId, Guid productId)
        {
            if (!string.IsNullOrEmpty(userId) && productId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(productId)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            var user = this._userRepository.Get(userId);
            var shoppingCart = user.ShoppingCart;

            ShoppingCartDTO model = new ShoppingCartDTO
            {
                ProductInShoppingCarts = shoppingCart.ProductInShoppingCarts.ToList() ?? new List<ProductInShoppingCart>(),
                TotalPrice = (double)shoppingCart.ProductInShoppingCarts.Sum(z => z.Quantity * z.Product.Price)
            };
            return model;
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.ShoppingCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    userId = userId
                };
                this._orderRepository.Insert(order);

                List<ProductsInOrder> productInOrders = new List<ProductsInOrder>();

                var result = userCard.ProductInShoppingCarts.Select(z => new ProductsInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.Product.Id,
                    Product = z.Product,
                    OrderId = order.Id,
                    Order = order,
                    Quantity = z.Quantity
                }).ToList();

                double totalPrice = 0.0;

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Quantity * (double)currentItem.Product.Price;
                }

                productInOrders.AddRange(result);

                foreach (var item in productInOrders)
                {
                    this._productInOrderRepository.Insert(item);
                }

                loggedInUser.ShoppingCart.ProductInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                return true;
            }

            return false;
        }
    }
}
