using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Business.Abstract
{
  public  interface IOrderService
    {
        bool Create(Order entity);
        void Update(Order entity);
        void Delete(Order entity);
        List<Order> GetOrders(string userId);
        List<Order> AllOrders();
    }
}
