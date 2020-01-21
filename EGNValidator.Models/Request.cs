using System;
using System.ComponentModel.DataAnnotations;

namespace EGNValidator.Models
{
    public class Request
    {

        public string Id { get; set; }
        [MinLength(10)]
        [MaxLength(10)]
        public string EGN { get; set; }

        public bool IsValid { get; set; }

        public string RequestIp { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
