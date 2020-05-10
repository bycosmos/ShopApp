using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisApp.Web.Models
{
    public class BasketModel
    {
        public int BasketId { get; set; }
        public List<BasketDetailModel> BasketDetails { get; set; }
       
        //public string CustomerAddress { get; set; }
        
        public decimal TotalPrice()
        {
            return BasketDetails.Sum(i => i.Price * i.Quantity);
        }
    }

    public class BasketDetailModel
    {
        public int BasketDetailId { get; set; }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
    }

}
