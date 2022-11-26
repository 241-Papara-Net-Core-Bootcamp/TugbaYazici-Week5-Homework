using AutoMapper;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Concretes;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaparaThirdWeek.Services.DTOs.FakeUserDto;

namespace PaparaThirdWeek.Services.Concretes
{
    public class FakeUserServices : IFakeUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<FakeUser> _repository;
        private readonly ICacheService _cacheService;
        private const string cacheKey = "FakeUserCacheKey";

        public FakeUserServices(IMapper mapper, IRepository<FakeUser> repository, ICacheService cacheService)
        {
            _mapper = mapper;
            _repository = repository;
            _cacheService = cacheService;
        }

        public void Add(FakeUserDto fakeUserDto)
        {
            var fakeUser = _mapper.Map<FakeUser>(fakeUserDto);
            var cachedList = _repository.Add(fakeUser);
            _cacheService.Remove(cacheKey);
            _cacheService.Set(cacheKey, cachedList);
        }

        public List<FakeUser> GetAllFakeUsers()
        {
            var userList = _repository.GetAll().ToList();
            _cacheService.Set(cacheKey, userList);                   // Cache the user list 
            _cacheService.TryGet<FakeUser>(cacheKey, out userList);       // Get cached user list     
            return userList;
        }
    }
}
