using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework28.Models.ViewModel {
    public class ProductViewModel {
        public int ID { get; set; }
        [Required(ErrorMessage = "can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "can't be empty")]
        public decimal Cost { get; set; }
        [Required]
        public int ProductCategoryID { get; set; }
        public string ProductCategoryName { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
