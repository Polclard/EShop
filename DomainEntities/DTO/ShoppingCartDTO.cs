using EShop.DomainEntities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DomainEntities.DTO
{
    public class ShoppingCartDTO
    {
        public List<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
