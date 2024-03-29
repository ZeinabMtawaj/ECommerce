﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Customer.Models
{
    public class CategorySpecificationValueViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public int CategoryId { get; set; }
        public int SpecificationId { get; set; }
        public virtual CategoryViewModel? Category { get; set; }
        public virtual SpecificationViewModel? Specification { get; set; }
    }
}
