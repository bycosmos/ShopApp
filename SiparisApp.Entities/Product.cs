using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SiparisApp.Entities
{
   public class Product
    {
        [Key]
        public int Id { get; set; }
        public int ProductCode { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public List<BasketDetail> BasketDetails { get; set; }


    }
}
