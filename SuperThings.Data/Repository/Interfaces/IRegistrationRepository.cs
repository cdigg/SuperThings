using SuperThings.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperThings.Data.Repository.Interfaces
{
    public interface IRegistrationRepository
    {
        public Task<int> CreateRegistration(Registrant entry);
        public Task<Registrant> GetRegistrant(int id);
        public Task<ICollection<Registrant>> GetMostRecent(int num);

        public Task<int> GetCount();

        public Task<ICollection<FavoriteInteger>> GetFavoriteIntegersStats(int num);
    }
}
