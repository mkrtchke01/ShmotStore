using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShmotStore.Models
{
    public class CartService
    {
        private DBContext db;
        public CartService(DBContext context)
        {
            db = context;
        }
        public Cart AddCartProduct(int productId, string email)
        {
            
            var cart =  db.Carts.Add(new Cart()
            {
                IdForProduct = productId,
                Email=email

            }).Entity;

            db.SaveChanges();

            return cart;
        }
        public List<Product> GetAllCartProducts(string email)
        {
            var productId = db.Carts.Where(t => t.Email == email).Select(t => t.IdForProduct).ToList();
            var product = db.Products.Where(t => productId.Contains(t.Id)).ToList();
            return product;
        }
        public async Task<Cart> DeleteFromCartAsync(int? Id)
        {
            Cart prodFromCart = await db.Carts.FirstOrDefaultAsync(p => p.IdForProduct == Id);
            if (prodFromCart != null)
            {
                db.Carts.Remove(prodFromCart);
                await db.SaveChangesAsync();
                return prodFromCart;
            }
            return null;
        }
        public async Task<IQueryable<Cart>> DeleteAllFromCartAsync(string email)
        {
            IQueryable<Cart> carts=  db.Carts.Where(p => p.Email==email);
            if (carts != null)
            {
                db.Carts.RemoveRange(carts);
                await db.SaveChangesAsync();
                return carts;
            }
            return null;
        }

        public async Task<UserData> AddUserDataAsync(UserData userData)
        {
            db.UserDatas.Add(userData);
            await db.SaveChangesAsync();
            return userData;

        }
    }
}
