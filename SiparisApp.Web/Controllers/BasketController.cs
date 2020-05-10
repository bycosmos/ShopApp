using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiparisApp.Business.Abstract;
using SiparisApp.Entities;
using SiparisApp.Web.Identity;
using SiparisApp.Web.Models;

namespace SiparisApp.Web.Controllers
{
    //[Authorize]
    public class BasketController : Controller
    {

        private IBasketService _basketService;
        private UserManager<ApplicationUser> _userManager;

        public BasketController(IBasketService basketService, UserManager<ApplicationUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var basket = _basketService.GetBasketByUserId(_userManager.GetUserId(User));
            return View(new BasketModel()
            {
                BasketId = basket.Id,
                BasketDetails = basket.BasketDetails.Select(i => new BasketDetailModel()
                { 
                 BasketDetailId=i.Id,
                 ProductId=i.Product.Id,
                 Name=i.Product.Name,
                 Price=(decimal)i.Product.Price,
                 ImageUrl=i.Product.ImageUrl,
                 Quantity=i.Quantity,
                 TotalPrice=i.TotalPrice
                
                }

                  ).ToList()
                


            }); ;
        }
        
        [HttpPost]
        public IActionResult AddToBasket(int productId, int quantity)
        {
            _basketService.AddToBasket(_userManager.GetUserId(User), productId, quantity);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteFromBasket(int productId)
        {
            _basketService.DeleteFromBasket(_userManager.GetUserId(User), productId);
            return RedirectToAction("Index");
        }
      
    }
}