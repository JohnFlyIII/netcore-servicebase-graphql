using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperInc.Api.Models;

namespace SuperInc.Api.Datastores
{
    public interface IHeroesDatastore
    {
        Task<IEnumerable<Hero>> GetHeroes();
        Task<Hero> GetHero(Guid id);
        Task<Hero> AddSuperHero(Hero newSuperHero);
    }
}