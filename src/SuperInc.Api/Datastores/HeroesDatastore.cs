using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperInc.Api.Models;
using SuperInc.Core.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;


namespace SuperInc.Api.Datastores{
    public class HeroesDatastore : IHeroesDatastore
    {

        private readonly IHeroesRepository _heroesRepository;
        private readonly IMapper _mapper;


        public HeroesDatastore(IHeroesRepository heroesRepository, IMapper mapper)
        {
            _heroesRepository = heroesRepository;
            _mapper = mapper;
        }

        public Task<Hero> AddSuperHero(Hero newSuperHero)
        {
            throw new NotImplementedException();
        }

        public async Task<Hero> GetHero(Guid id)
        {
            return _mapper.Map<Core.Entities.Hero, Hero>(await _heroesRepository.Find(id));
        }

        public Task<IEnumerable<Hero>> GetHeroes()
        {
           return Task.FromResult(_heroesRepository.AllHeroes().ProjectTo<Hero>(_mapper.ConfigurationProvider).ToList().AsEnumerable());
        }
    }
}