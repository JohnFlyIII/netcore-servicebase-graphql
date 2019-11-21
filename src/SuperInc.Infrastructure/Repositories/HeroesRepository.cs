using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SuperInc.Core.Interfaces;
using SuperInc.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SuperInc.Infrastructure.Repositories
{
    public class HeroesRepository : IHeroesRepository
    {
        private readonly HeroesDbContext _context;
        private readonly IMapper _mapper;

        public HeroesRepository(HeroesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<SuperInc.Core.Entities.Hero> AllHeroes()
        {
            return _context
                .Heroes
                .AsNoTracking()
                .ProjectTo<SuperInc.Core.Entities.Hero>(_mapper.ConfigurationProvider)
                .AsQueryable();
        }

        public async Task CreateHero(Core.Entities.Hero hero)
        {
            var heroDataModel = _mapper.Map<SuperInc.Infrastructure.Models.Hero>(hero);

            await _context.Heroes.AddAsync(heroDataModel).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            var heroToDelete = await _context.Heroes.FirstOrDefaultAsync(hero => hero.Id == id).ConfigureAwait(false);

            _context.Heroes.Remove(heroToDelete);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<SuperInc.Core.Entities.Hero> Find(Guid id)
        {
            var heroDataModel = await _context
                                    .Heroes
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(hero => hero.Id == id)
                                    .ConfigureAwait(false);

            return _mapper.Map<SuperInc.Core.Entities.Hero>(heroDataModel);
        }

        public async Task<SuperInc.Core.Entities.Hero> UpdateHero(SuperInc.Core.Entities.Hero heroEntity)
        {
            var heroDataModel = await _context
                                    .Heroes
                                    .FirstOrDefaultAsync(hero => hero.Id == heroEntity.Id)
                                    .ConfigureAwait(false);

            heroDataModel.Name = heroEntity.Name;

            await _context.SaveChangesAsync();

            return _mapper.Map<SuperInc.Core.Entities.Hero>(heroDataModel);
        }
    }
}