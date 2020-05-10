using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Dal.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
    
        Product GetProductDetails(int id);

       
        void Update(Product entity, int[] categoryIds);

    }
}
