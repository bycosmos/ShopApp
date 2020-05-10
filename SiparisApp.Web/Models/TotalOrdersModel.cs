using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisApp.Web.Models
{
    public class TotalOrdersModel
    {

        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        public List<TotalOrderDetailModel> TotalOrderDetails { get; set; }

        public decimal TotalPrice()
        {
            return TotalOrderDetails.Sum(i => i.Price * i.Quantity);
        }
    }

    public class TotalOrderDetailModel
    {
        public int TotalOrderDetailId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}

