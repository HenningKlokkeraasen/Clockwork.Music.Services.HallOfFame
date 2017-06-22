using System;
using System.Collections.Generic;
using System.Linq;

namespace Clockwork.Music.Services.HallOfFame
{
    public class HallOfFameRepository : IRepository<HallOfFame>
    {
        public IList<HallOfFame> GetAll() => new List<HallOfFame>();

        public HallOfFame Get(int id) => HallOfFame.Empty;
    }

    public class HallOfFameService
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
            var entries = _cache.Get(_cacheKey) ?? new List<HallOfFame>();

            if (!entries.Any())
                entries = _repo.GetAll() ?? new List<HallOfFame>();

            if (entries.Any())
                _cache.Store(entries, _cacheKey);

            return entries;
        }

        public HallOfFame Get(int id) => throw new NotImplementedException();
    }
}