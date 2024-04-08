using EShop.DomainEntities.Identity;
using EShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EShop.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        private DbSet<EShopUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<EShopUser>();
        }

        public void Delete(EShopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public EShopUser Get(string id)
        {
            var strGuid = id.ToString();
            return entities.Include(z => z.ShoppingCart).Include(z => z.ShoppingCart.ProductInShoppingCarts).Include("ShoppingCart.ProductInShoppingCarts.Product").SingleOrDefault(s => s.Id == strGuid);
        }

        public IEnumerable<EShopUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(EShopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(EShopUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _context.SaveChanges();
        }
    }
}
