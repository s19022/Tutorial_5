
using System;
using System.ComponentModel.DataAnnotations;

namespace Tutorial_5.DTOs.Requests
{
    public class EnrollStudentRequest
    { 
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]

        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}
