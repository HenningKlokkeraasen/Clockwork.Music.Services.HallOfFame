using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Clockwork.Music.Services.HallOfFame.Core
{
    public class FileReader : IFileReader
    {
        public string ReadAsString(string filepath) => File.ReadAllText(filepath);
    }

    public class JsonParser : IJsonParser
    {
        public T Parse<T>(string json) => JsonConvert.DeserializeObject<T>(json);
    }

    public class HallOfFameRepository : IRepository<Hall>
    {
        private readonly IFileReader _fileReader;
        private readonly IJsonParser _jsonParser;
        private readonly string _filePath;

        public HallOfFameRepository(IFileReader fileReader, IJsonParser jsonParser, string filePath)
        {
            _fileReader = fileReader;
            _jsonParser = jsonParser;
            _filePath = filePath;
        }

        public IList<Hall> GetAll()
        {
            var json = _fileReader.ReadAsString(_filePath);
            var data = _jsonParser.Parse<IList<Hall>>(json);
            return data;
        }

        public Hall Get(object id) => throw new NotImplementedException();
    }

    public class HallOfFameService : IService<Hall>
    {
        private readonly string _cacheKey;
        private readonly IRepository<Hall> _repo;
        private readonly ICache<IList<Hall>> _cache;

        public HallOfFameService(IRepository<Hall> repo, ICache<IList<Hall>> cache, string cacheKey)
        {
            _repo = repo;
            _cache = cache;
            _cacheKey = cacheKey;
        }

        public IList<Hall> GetAll()
        {
            var entries = _cache.Get(_cacheKey);

            if (entries.Any())
                return entries;

            entries = _repo.GetAll();

            if (entries.Any())
                _cache.Store(entries, _cacheKey);

            return entries;
        }

        public Hall Get(object id) => !(id is int)
            ? throw new ArgumentException("id must be int")
            : GetAll().FirstOrDefault(hof => hof.Id == (int) id);
    }
}