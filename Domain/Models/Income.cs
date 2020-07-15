using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domains.Models
{
   public  class Income : BaseEntity
    {
        public Guid Id { get; set; }

        [DisplayName("Income")]
        public string Income_ { get; set; }
        public string Date { get; set; }
        public String Description { get; set; }


    }
}
