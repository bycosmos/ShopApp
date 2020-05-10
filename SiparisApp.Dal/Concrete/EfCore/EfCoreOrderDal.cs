using Microsoft.EntityFrameworkCore;
using SiparisApp.Dal.Abstract;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiparisApp.Dal.Concrete.EfCore
{
  public  class EfCoreOrderDal : EfCoreGenericRepository<Order, ShopContext>, IOrderDal
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new ShopContext())
            {
                var orders = context.Orders
                                .Include(i => i.OrderDetails)
                                .ThenInclude(i => i.Product)
                                .AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId);
                }

                return orders.ToList();
            }
        }
        public List<Order> AllOrders()
        {
            using (var context = new ShopContext())
            {
                var orders = context.Orders
                                .Include(i => i.OrderDetails)
                                .ThenInclude(i => i.Product)
                                .AsQueryable();

               

                return orders.ToList();


            }
        }
        

    }
}
