using EShop.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DomainEntities.Domain
{
    public class Order : BaseEntity
    {
        public string? userId { get; set; }
        public ICollection<ProductsInOrder>? ProductsInOrders{ get; set; }
    }
}
