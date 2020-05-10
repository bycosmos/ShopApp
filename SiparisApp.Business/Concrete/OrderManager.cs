using SiparisApp.Business.Abstract;
using SiparisApp.Dal.Abstract;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public bool Create(Order entity)
        {
           
                _orderDal.Create(entity);
                return true;
         
        }


        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            _orderDal.Update(entity);
        }
        public List<Order> GetOrders(string userId)
        {
            return _orderDal.GetOrders(userId);
        }
        public List<Order> AllOrders()
        {
            return _orderDal.AllOrders();
        }
    }
}
