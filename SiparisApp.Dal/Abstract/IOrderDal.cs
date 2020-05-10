using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Dal.Abstract
{
   public interface IOrderDal:IRepository<Order>
    {
        List<Order> GetOrders(string userId);
        List<Order> AllOrders();
     

    }
}
