using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Business.Abstract
{
  public  interface IBasketService
    {

        void InitializeBasket(string userId);
        Basket GetBasketByUserId(string userId);
        void AddToBasket(string userId, int productId, int quantity);
        void DeleteFromBasket(string userId, int productId);
        void ClearBasket(string basketId);
        void Update(Basket entity);

    }
}
