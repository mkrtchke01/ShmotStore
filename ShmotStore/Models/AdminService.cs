using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmotStore.Models
{
    public class AdminService
    {
        private DBContext db;
        public AdminService(DBContext context)
        {
            db = context;
        }


        public async Task<Product> AddProductAsync(Product product)
        {


            db.Products.Add(product);
            await db.SaveChangesAsync();
            return product;

        }



        public async Task<Product> GetProductAsync(int? id)
        {
            Product getprod = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return getprod;
        }
        public async Task<Product> DeleteProductAsync(int? Id)
        {
            Product prod = await db.Products.FirstOrDefaultAsync(p => p.Id == Id);
            if (prod != null)
            {
                db.Products.Remove(prod);
                await db.SaveChangesAsync();
                return prod;
            }
            return null;
        }

        public async Task<Product> UpdateProductAsync(Product prod)
        {
                db.Products.Update(prod);
                await db.SaveChangesAsync();
                return prod;
        }
        public List<Product> GetOrder(string email)
        {
            var productId = db.TimingDBs.Where(t => t.Email == email).Select(t => t.IdForProduct).ToList();
            var product = db.Products.Where(t => productId.Contains(t.Id)).ToList();
            return product;
        }
    }
}
