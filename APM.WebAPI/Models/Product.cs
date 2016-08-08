using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APM.WebAPI.Models
{
    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "6 chars or more please")]
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Product name min length is 5")]
        [MaxLength(11)]
        public string ProductName { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}