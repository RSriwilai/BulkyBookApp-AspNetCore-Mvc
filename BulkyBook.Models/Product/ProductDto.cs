using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.Product
{
    public class ProductDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [Range(1, 10000)]
        public double ListPrice { get; set; }

        [Required]
        [Range(1,10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        public string ImageUrl { get; set; }

    }
}
