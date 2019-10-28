using System;
using System.Collections.Generic;
using System.Text;

namespace SuperThings.Data.Models
{
    public class RegistrantDto
    {
        //registrant fields
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int[] FavoriteIntegers { get; set; }
        public bool EmailOptIn { get; set; }

        //fields supplied by db, will be null on initial insert
        public int? Id { get; set; }
        public DateTime? RegistrationDateTime { get; set; }
    }
}
