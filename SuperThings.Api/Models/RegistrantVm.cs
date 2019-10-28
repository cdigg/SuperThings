using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperThings.Data.Models
{
    public class RegistrantVm
    {

        //generated property for use when returning data
        public int? Id { get; set; }

        //incoming properties
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public int[] FavoriteFiveIntegers { get; set; }

        [Required]
        public bool? EmailOptIn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? TimeOfRegistration { get; set; }


    }
}
