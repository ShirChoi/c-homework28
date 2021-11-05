using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Homework28.Models {
    public class Product {
        public int ID { get; set; }
        [Required(ErrorMessage = "can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "can't be empty")]
        public decimal Cost { get; set; }
        public int ProductCategoryID { get; set; }

        [ForeignKey("ProductCategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
