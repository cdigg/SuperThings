using Microsoft.EntityFrameworkCore;
using SuperThings.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperThings.Data.Repository
{
    public class SuperThingsContext : DbContext
    {
        public SuperThingsContext(DbContextOptions<SuperThingsContext> options) : base(options)
        { }


        public DbSet<Registrant> Registrant { get; set; }
        public DbSet<RegistrantInteger> RegistrantInteger { get; set; }
    }
}