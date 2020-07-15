using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.Models
{
   public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        [Required]
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public ProductData ProductData { get; set; }
        public Guid ProductDataId { get; set; }
    }
}
