using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.viewmodel
{
    public class productVM
    {
        public Product   Product { get; set; }
        public ICollection<ProductData> productDatas { get; set; }
    }
}
