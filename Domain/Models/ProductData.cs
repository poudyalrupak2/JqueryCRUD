using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domains.Models
{
   public class ProductData : BaseEntity
    {
        public Guid Id { get; set; }
        [DisplayName("Total Price(All Item)")]
        public string TotalPrice { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
        [DisplayName("Total Discount(All Item)")]

        public string Discount { get; set; }

    }
}
