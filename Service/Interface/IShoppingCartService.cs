using EShop.DomainEntities.Domain;
using EShop.DomainEntities.DTO;

namespace EShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);
        bool deleteProductFromSoppingCart(string userId, Guid productId);
        bool order(string userId);
        bool AddToShoppingConfirmed(ProductInShoppingCart model, string userId);
    }
}
