using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShmotStore.Models;

namespace ShmotStore.Controllers
{
    public class HomeController : Controller
    {
        private DBContext db;
        private HomeService homeService;
        public HomeController(DBContext context, HomeService service)
        {
            db = context;
            homeService = service;
        }

        public IActionResult Index(string gender, string season)
        {
            ViewBag.Title = "Страница с одеждой";

            var products = homeService.GetProduct(gender,season);

            FilterModel filter = new FilterModel
            {
                products = products.ToList(),
                Genders = new SelectList(new List<string>()
                {
                    "Все",
                    "Мужчина",
                    "Девушка"
                }),
                Seasons = new SelectList(new List<string>()
                {
                    "Все",
                    "Зима",
                    "Весна",
                    "Лето",
                    "Осень",
                })
            };
            return View(filter);
        }
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id != null)
            {
                var GetProd = await homeService.GetProductAsync(Id);
                if (GetProd != null)
                    return View(GetProd);
            }
            return NotFound();
        }
    }

    
}
