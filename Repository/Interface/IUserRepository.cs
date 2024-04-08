using EShop.DomainEntities.Domain;
using EShop.DomainEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EShopUser> GetAll();
        EShopUser Get (string id);
        void Insert(EShopUser entity);
        void Update(EShopUser entity);
        void Delete(EShopUser entity);
    }
}
