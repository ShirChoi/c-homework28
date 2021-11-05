using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homework28.Models {
    public class ProductCategory {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } 

        public virtual List<Product> Products { get; set; }
    }
}
