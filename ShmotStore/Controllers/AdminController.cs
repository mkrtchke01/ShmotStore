using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShmotStore.Models;

namespace ShmotStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private DBContext dB;
        private AdminService productService;
        public AdminController(DBContext context, AdminService service)
        {
            dB = context;
            productService = service;
        }
        public async Task<IActionResult> Data()
        {
            return View(await dB.Products.ToListAsync());
        }

        //Добавление
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product products)
        {

            var addProduct = await productService.AddProductAsync(products);
            return RedirectToAction("Data");
        }


        //Удаление
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? Id)
        {
            if (Id != null)
            {
                var GetProd = await productService.GetProductAsync(Id);
                if (GetProd != null)
                    return View(GetProd);
            }
            return NotFound();
        }
        [Route("Admin/Delete/{Id}")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id != null)
            {
                var deleteProduct = await productService.DeleteProductAsync(Id);
                return RedirectToAction("Data");
            }
            return NotFound();
        }
        // Редактирование
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id != null)
            {
                var GetProd = await productService.GetProductAsync(Id);
                if (GetProd != null)
                    return View(GetProd);
            }
            return NotFound();
        }
        [Route("Admin/Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(Product prod)
        {
            var editProd = await productService.UpdateProductAsync(prod);
            return RedirectToAction("Data");
        }

        public async Task<IActionResult> ListCartUser()
        {
            return View(await dB.UserDatas.ToListAsync());
        }
        public IActionResult OrderUser(string email)
        {
            if (email != null)
            {
                var GetProd = productService.GetOrder(email);
                if (GetProd != null)
                    return View(GetProd);
            }
            return NotFound();
        }

        public async Task<IActionResult> DeleteTimingCart(string email)
        {
            IQueryable<UserData> userData = dB.UserDatas.Where(p => p.Email == email);
            dB.UserDatas.RemoveRange(userData);
            await dB.SaveChangesAsync();

            IQueryable<TimingDB> timings = dB.TimingDBs.Where(p => p.Email == email);
            dB.TimingDBs.RemoveRange(timings);
            await dB.SaveChangesAsync();
            return RedirectToAction("ListCartUser", "Admin");
        }
    }
}
