using AutoMapper;
using SuperThings.Data.Models;
using SuperThings.Data.Repository;
using SuperThings.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperThings.Logic.Services
{
    public interface IRegistrationService
    {
        public Task<int> Register(string fullname, string email, int[] favoriteIntegers, DateTime dateOfBirth, bool emailOptIn, DateTime timeOfRegistration);
        public Task<RegistrantDto> GetRegistrant(int id);
        public Task<List<RegistrantDto>> GetMostRecent(int num);
    }
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;

        public RegistrationService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }

        public async Task<List<RegistrantDto>> GetMostRecent(int num)
        {
            var list = await _registrationRepository.GetMostRecent(num);
            return _mapper.Map<List<RegistrantDto>>(list);
        }

        public async Task<RegistrantDto> GetRegistrant(int id)
        {
            var registrant = await _registrationRepository.GetRegistrant(id);
            return _mapper.Map<RegistrantDto>(registrant);
        }

        public async Task<int> Register(string fullname, string email, int[] favoriteIntegers, DateTime dateOfBirth, bool emailOptIn, DateTime timeOfRegistration)
        {
            var dto = new RegistrantDto
            {
                FullName = fullname,
                Email = email,
                EmailOptIn = emailOptIn,
                DateOfBirth = dateOfBirth,
                FavoriteIntegers = favoriteIntegers,
                RegistrationDateTime = timeOfRegistration
            };

            return await _registrationRepository.CreateRegistration(_mapper.Map<Registrant>(dto));

        }
    }
}
