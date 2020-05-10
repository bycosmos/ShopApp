using SiparisApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiparisApp.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
      
        Product GetProductDetails(int id);
        List<Product> GetAll();
      
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        void Update(Product entity, int[] categoryIds);

    }
}
