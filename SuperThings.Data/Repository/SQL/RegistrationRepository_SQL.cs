using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperThings.Data.Models;
using SuperThings.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperThings.Data.Repository.SQL
{

    public class RegistrationRepository_SQL : IRegistrationRepository
    {
        private readonly SuperThingsSqlContext _context;

        public RegistrationRepository_SQL(SuperThingsSqlContext context)
        {
            _context = context;
        }
        public async Task<int> CreateRegistration(Registrant entry)
        {
            //set registration time at tmie of insert
            entry.RegistrationDateTime = DateTime.Now;

            _context.Registrant.Add(entry);
            await _context.SaveChangesAsync();

            return entry.Id;
        }

        public async Task<int> GetCount()
        {
            return await _context.Registrant.CountAsync();
        }

        public async Task<ICollection<FavoriteInteger>> GetFavoriteIntegersStats(int num)
        {
            //get the top <x> favorite values by count
            return await _context.RegistrantInteger.GroupBy(x => x.IntegerValue)
                .Select(g => new
                {
                    intVal = g.Key,
                    count = g.Count(),
                })
                .OrderByDescending(x => x.count)
                .Take(num)
                .Select(x => new FavoriteInteger
                {
                    Count = x.count,
                    IntegerValue = x.intVal
                })
                .ToListAsync();
        }

        public async Task<ICollection<Registrant>> GetMostRecent(int num)
        {
             return await _context.Registrant.Include("FavoriteIntegers").OrderByDescending(x => x.RegistrationDateTime).Take(num).ToListAsync();
        }

        public async Task<Registrant> GetRegistrant(int id)
        {
            return await _context.Registrant.Include("FavoriteIntegers").SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
