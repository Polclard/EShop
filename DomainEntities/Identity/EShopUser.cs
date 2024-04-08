using EShop.DomainEntities.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DomainEntities.Identity
{
    public class EShopUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
}
