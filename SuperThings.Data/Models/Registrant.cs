using System;
using System.Collections.Generic;
using System.Text;

namespace SuperThings.Data.Models
{
    public class Registrant
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<RegistrantInteger> FavoriteIntegers { get; set; }
        public bool EmailOptIn { get; set; }
        public DateTime RegistrationDateTime { get; set; }
    }

    public class RegistrantInteger
    {
        public int? Id { get; set; }
        public Registrant Registrant { get; set; }
        public int IntegerValue { get; set; }
    }
}
