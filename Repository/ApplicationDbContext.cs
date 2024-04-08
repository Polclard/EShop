using EShop.DomainEntities.Domain;
using EShop.DomainEntities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Repository
{

    public class ApplicationDbContext : IdentityDbContext<EShopUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<ProductsInOrder> ProductsInOrders{ get; set; }
        public virtual DbSet<Order> Orders{ get; set; }
    }
}
