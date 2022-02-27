using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.DatabaseModel
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

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
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price50 { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price100 { get; set; }

        public string ImageUrl { get; set; }


        //foreign key relations
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }
    }
}
