using AutoMapper;
using SuperThings.Data.Repository;
using SuperThings.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperThings.Logic.Services
{
    public interface IMonitoringService
    {
        public Task<int> Count();
        public Task<ICollection<FavoriteIntegerDto>> Favorites(int num);
    }
    public class MonitoringService : IMonitoringService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;

        public MonitoringService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }
        public async Task<int> Count()
        {
            return await _registrationRepository.GetCount();
        }

        public async Task<ICollection<FavoriteIntegerDto>> Favorites(int num)
        {
            var stats = await _registrationRepository.GetFavoriteIntegersStats(num);
            return _mapper.Map<ICollection<FavoriteIntegerDto>>(stats);
        }
    }
}
