using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
       // [Required]
       // [StringLength(60, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
       // [Required]
       // [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> SelectedCategories { get; set; }

    }
}
