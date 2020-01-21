using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGNValidator.Models
{
    public class RequestViewModel
    {
        [Required]
        [MaxLength(10, ErrorMessage = "EGN must contain 10 digits!")]
        [MinLength(10, ErrorMessage = "EGN must contain 10 digits!")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "EGN must contain 10 digits!")]
        public string EGN { get; set; }
        public string Ip { get; set; }
        public string Date { get; set; }

    }
}
