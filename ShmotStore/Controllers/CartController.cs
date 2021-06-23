using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShmotStore.Models;

namespace ShmotStore.Controllers
{
    public class CartController : Controller
    {
        private DBContext dB;
        private readonly CartService cartService;
        public CartController(DBContext context, CartService service)
        {
            dB = context;
            cartService = service;
        }

        public IActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                return View(cartService.GetAllCartProducts(email));
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult AddCartProduct(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var addProduct = cartService.AddCartProduct(id, email);
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Login", "Account");

        }
        [Route("Cart/Cart/{Id}")]
       // [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int? Id)
        {
            if (Id != null)
            {
                var deleteProduct = await cartService.DeleteFromCartAsync(Id);
                return RedirectToAction("Cart");
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteAllFromCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var email = User.Identity.Name;
                var deleteProduct = await cartService.DeleteAllFromCartAsync(email);
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult UserData()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserData(UserData userData)
        {
            var email = User.Identity.Name;
            foreach (var el in dB.Carts.Where(p => p.Email == email))
            {
                dB.TimingDBs.Add(new TimingDB
                {
                    IdForProduct = el.IdForProduct,
                    Email = email
                });
            }
            await dB.SaveChangesAsync();

            var data = await cartService.AddUserDataAsync(userData);

            var deleteProduct = await cartService.DeleteAllFromCartAsync(email);

            return RedirectToAction("Index", "Home");

        }
    }
}
