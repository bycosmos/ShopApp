using SiparisApp.Business.Abstract;
using SiparisApp.Dal.Abstract;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Business.Concrete
{
    public class BasketManager : IBasketService
    {
        private IBasketDal _basketDal;
        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public void AddToBasket(string userId, int productId, int quantity)
        {
            var basket = GetBasketByUserId(userId);
            if (basket != null)
            {
                var index = basket.BasketDetails.FindIndex(i => i.ProductId == productId);

                if (index < 0)
                {
                    basket.BasketDetails.Add(new BasketDetail()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        BasketId = basket.Id
                    });
                }
                else
                {
                    basket.BasketDetails[index].Quantity += quantity;
                }

                _basketDal.Update(basket);
            }
        }

        public void DeleteFromBasket(string userId, int productId)
        {
            var basket = GetBasketByUserId(userId);
            if (basket != null)
            {
                _basketDal.DeleteFromBasket(basket.Id, productId);
            }
        }

        public Basket GetBasketByUserId(string userId)
        {
            return _basketDal.GetByUserId(userId);
        }

        public void InitializeBasket(string userId)
        {
            _basketDal.Create(new Basket() { UserId = userId });
        }
        public void ClearBasket(string basketId)
        {
            _basketDal.ClearBasket(basketId);
        }
        public void Update(Basket entity)
        {
            _basketDal.Update(entity);
        }

    }
}
