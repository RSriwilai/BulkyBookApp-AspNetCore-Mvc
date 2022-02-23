﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.DatabaseModel
{
    public class CoverType
    {
        [Key]
        public int CoverTypeId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Cover Type")]
        public string Name { get; set; }
    }
}
