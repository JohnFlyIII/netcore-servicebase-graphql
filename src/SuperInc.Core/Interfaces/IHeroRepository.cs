
using System;
using System.Linq;
using System.Threading.Tasks;
using SuperInc.Core.Entities;

namespace SuperInc.Core.Interfaces
{
    public interface IHeroesRepository
    {
        IQueryable<Hero> AllHeroes();

        Task CreateHero(Hero hero);

        Task<Hero> Find(Guid id);

        Task<Hero> UpdateHero(Hero hero);

        Task Delete(Guid id);
    }
}