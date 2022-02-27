using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.CoverTypes
{
    public class CoverTypeDto
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "Cover Type")]
        public string Name { get; set; }
    }
}
