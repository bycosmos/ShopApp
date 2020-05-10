using Microsoft.EntityFrameworkCore;
using SiparisApp.Dal.Abstract;
using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiparisApp.Dal.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ShopContext>, IProductDal
    {


        public Product GetProductDetails(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                            .Where(i => i.Id == id)
                            
                            .FirstOrDefault();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                                   .FirstOrDefault(i => i.Id == entity.Id);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Description = entity.Description;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;
                    context.SaveChanges();
                }
            }
        }
    }
}
