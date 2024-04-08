using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.DomainEntities.Domain;
using EShop.Repository;
using EShop.Service.Interface;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IProductService productService, 
            IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
                return View(this._shoppingCartService.getShoppingCartInfo(userId));
            return NotFound();
        }

        // POST: ShoppingCarts/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shoppingCart = this._shoppingCartService.deleteProductFromSoppingCart(userId, id.Value);
            if (shoppingCart)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._shoppingCartService.order(userId);
            return RedirectToAction("Index", "ShoppingCarts");
        }
    }
}
