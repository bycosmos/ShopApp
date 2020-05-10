using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Dal.Abstract
{
    public interface IBasketDal : IRepository<Basket>
    {
        Basket GetByUserId(string userId);
        void DeleteFromBasket(int basketId, int productId);
        void ClearBasket(string basketId);
    }
}
