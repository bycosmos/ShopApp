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
    //[Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IBasketService _basketService;
        private IOrderService _orderService;
        private UserManager<ApplicationUser> _userManager;

        public AdminController(IOrderService orderService, IBasketService basketService, UserManager<ApplicationUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
            _orderService = orderService;
        }
        public IActionResult TotalOrders()
        {
            List<Order> o = _orderService.AllOrders();



           
            var orderListModel = new List<TotalOrdersModel>();
            TotalOrdersModel orderModel;

            foreach (var order in o)
            {
                orderModel = new TotalOrdersModel();
                orderModel.OrderId = order.Id;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.OrderDate = order.OrderDate;
                orderModel.OrderNote = order.OrderNote;
                orderModel.Phone = order.Phone;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Email = order.Email;
                orderModel.Address = order.Address;
                orderModel.City = order.City;

                orderModel.TotalOrderDetails = order.OrderDetails.Select(i => new TotalOrderDetailModel()
                {
                    TotalOrderDetailId = i.Id,
                    Name = i.Product.Name,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    ImageUrl = i.Product.ImageUrl
                }).ToList();

                orderListModel.Add(orderModel);

            }

            return View(orderListModel);










            
        }
      

    }
}