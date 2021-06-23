using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShmotStore.Models
{
    public class HomeService
    {
        private DBContext db;
        public HomeService(DBContext context)
        {
            db = context;
        }
        public async Task<Product> GetProductAsync(int? id)
        {
            Product getprod = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return getprod;
        }

         public IQueryable<Product> GetProduct(string gender, string season)
         {
            IQueryable<Product> products = db.Products.AsQueryable<Product>();
            if (!string.IsNullOrEmpty(season) && !string.IsNullOrEmpty(gender))
            {
                //Если гендер чему то равен, а сезон равен всем
                if (!gender.Equals("Все") && season.Equals("Все"))
                {
                    products = products.Where(p => p.Gender == gender);
                }


                //Если сезон чему то равен, а гендер равен всем
                if (gender.Equals("Все") && !season.Equals("Все"))
                {
                    products = products.Where(p => p.Season == season);

                }
                //Если оба не равны всем
                if (!gender.Equals("Все") && !season.Equals("Все"))
                {
                    products = products.Where(p => p.Season == season && p.Gender == gender);

                }
            }
            return products;
        }
    }
}
