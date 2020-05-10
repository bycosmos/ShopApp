using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SiparisApp.Entities
{
    public class BasketDetail 
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Basket Basket { get; set; }
        public int BasketId { get; set; }

        public int Quantity { get; set; }
        public string TotalPrice { get; set; }
     


    }
}
