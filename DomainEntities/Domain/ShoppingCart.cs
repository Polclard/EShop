using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DomainEntities.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public ICollection<ProductInShoppingCart>? ProductInShoppingCarts{ get; set; }
    }
}
