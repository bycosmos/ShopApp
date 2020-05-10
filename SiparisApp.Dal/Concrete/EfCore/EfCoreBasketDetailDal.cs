using SiparisApp.Dal.Abstract;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Dal.Concrete.EfCore
{
   public class EfCoreBasketDetailDal: EfCoreGenericRepository<BasketDetail, ShopContext>, IBasketDetailDal
    {
    }
}
