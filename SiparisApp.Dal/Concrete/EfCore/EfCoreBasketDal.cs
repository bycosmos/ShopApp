using Microsoft.EntityFrameworkCore;
using SiparisApp.Dal.Abstract;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiparisApp.Dal.Concrete.EfCore
{
   public class EfCoreBasketDal : EfCoreGenericRepository<Basket, ShopContext>,IBasketDal
    {
        public override void Update(Basket entity)
        {
            using (var context = new ShopContext())
            {
                context.Baskets.Update(entity);
                context.SaveChanges();
            }
        }

        public Basket GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context
                            .Baskets
                            .Include(i => i.BasketDetails)
                            .ThenInclude(i => i.Product)
                            .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteFromBasket(int basketId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from BasketDetail where BasketId=@p0 And ProductId=@p1";
                context.Database.ExecuteSqlCommand(cmd, basketId, productId);
            }
        }

        public void ClearBasket(string basketId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from BasketDetail where BasketId=@p0";
                context.Database.ExecuteSqlCommand(cmd, basketId);
            }
        }
    }
}
