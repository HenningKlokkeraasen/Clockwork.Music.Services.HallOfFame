using System.Collections.Generic;
using System.Linq;

namespace Clockwork.Music.Services.HallOfFame
{
    public class HallOfFameRepository : IRepository<HallOfFame>
    {
        public IList<HallOfFame> GetAll() => new List<HallOfFame>
        {
            new HallOfFame
            {
                Id = 1,
                Name = "Rock and Roll Hall of Fame",
                InfoUrl = "https://en.wikipedia.org/wiki/Rock_and_Roll_Hall_of_Fame"
            }
        };
    }

    public class HallOfFameService : IService<HallOfFame>
    {
        private readonly string _cacheKey;
        private readonly IRepository<HallOfFame> _repo;
        private readonly ICache<HallOfFame> _cache;

        public HallOfFameService(IRepository<HallOfFame> repo, ICache<HallOfFame> cache, string cacheKey)
        {
            _repo = repo;
            _cache = cache;
            _cacheKey = cacheKey;
        }

        public IList<HallOfFame> GetAll()
        {
            var entries = _cache.Get(_cacheKey);

            if (entries.Any())
                return entries;

            entries = _repo.GetAll();

            if (entries.Any())
                _cache.Store(entries, _cacheKey);

            return entries;
        }

        public HallOfFame Get(int id) => GetAll().FirstOrDefault(hof => hof.Id == id);
    }
}