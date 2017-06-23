using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Clockwork.Music.Services.HallOfFame
{
    public class SimpleDocumentDb : ISimpleDocumentDb
    {
        public T Read<T>(string filepath) => Parse<T>(ReadAllText(filepath));
        private static string ReadAllText(string path) => File.ReadAllText(path);
        private static T Parse<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }

    public class HallOfFameRepository : IRepository<HallOfFame>
    {
        private readonly ISimpleDocumentDb _documentDb;

        public HallOfFameRepository(ISimpleDocumentDb documentDb)
        {
            _documentDb = documentDb;
        }

        public IList<HallOfFame> GetAll() => _documentDb.Read<IList<HallOfFame>>("hallsoffame.json");
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