using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiparisApp.Business.Abstract;
using SiparisApp.Entities;
using SiparisApp.Web.Identity;
using SiparisApp.Web.Models;

namespace SiparisApp.Web.Controllers
{
    public class OrderController : Controller
    {

        private IBasketService _basketService;
        private IOrderService _orderService;
        private UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IBasketService basketService, UserManager<ApplicationUser> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var basket = _basketService.GetBasketByUserId(_userManager.GetUserId(User));

            var orderModel = new OrderModel();
            orderModel.Address = basket.CustomerAddress;
           



            orderModel.BasketModel = new BasketModel()
            
            {

                BasketId = basket.Id,

                BasketDetails = basket.BasketDetails.Select(i => new BasketDetailModel()
                {
                    BasketDetailId = i.Id,
                    ProductId = i.Product.Id,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity


                }).ToList(),
             
                 
            };

            return View(orderModel);
        }
        [HttpPost]
        public IActionResult Index(OrderModel orderModel)
        { 
            var userId = _userManager.GetUserId(User);
            var basket = _basketService.GetBasketByUserId(userId);

           

            orderModel.BasketModel = new BasketModel()
            {
                
                BasketId = basket.Id,
                 

                BasketDetails = basket.BasketDetails.Select(i => new BasketDetailModel()
                {
                    BasketDetailId = i.Id,
                    ProductId = i.Product.Id,
                    Name = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    ImageUrl = i.Product.ImageUrl,
                    Quantity = i.Quantity
                     



                }).ToList(),
                


            };
            SaveOrder(orderModel, userId);
            ClearBasket(basket.Id.ToString());
            return RedirectToAction("MyOrderList");
        
          



            
        }
        private void ClearBasket(string cartId)
        {
            _basketService.ClearBasket(cartId);
        }
        private void SaveOrder(OrderModel orderModel, string userId)
        {

            Order O = new Order();
            O.OrderNumber = new Random().Next(111111, 999999).ToString();
            O.Address = orderModel.Address;
            O.City = orderModel.City;
            O.Email = orderModel.Email;
            O.FirstName = orderModel.FirstName;
            O.LastName = orderModel.LastName;
            O.OrderDate = DateTime.Now;
            O.Phone = orderModel.Phone;

            O.UserId = userId;
            foreach (var item in orderModel.BasketModel.BasketDetails)
            {
                var orderDetail = new OrderDetail()
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                   
                     
                    
                };
                O.OrderDetails.Add(orderDetail);
            }
            _orderService.Create(O);

        }
        public IActionResult MyOrderList()
        {
            var orders = _orderService.GetOrders(_userManager.GetUserId(User));
            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;

            foreach (var order in orders)
            {
                orderModel = new OrderListModel();
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

                orderModel.OrderDetails = order.OrderDetails.Select(i => new OrderDetailModel()
                {
                    OrderItemId = i.Id,
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




    
