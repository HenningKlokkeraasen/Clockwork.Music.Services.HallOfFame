using System.Collections.Generic;
using JetBrains.Annotations;

namespace Clockwork.Music.Services.HallOfFame.Core
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InfoUrl { get; set; }

        public static Hall Empty => new Hall
        {
            Id = -1,
            Name = string.Empty,
            InfoUrl = string.Empty
        };
    }

    public interface IFileReader
    {
        string ReadAsString(string filepath);
    }
    
    public interface IJsonParser
    {
        T Parse<T>(string json);
    }

    public interface IRepository<T>
    {
        [NotNull]
        IList<T> GetAll();

        T Get(object id);
    }

    public interface ICache<T>
    {
        void Store(T obj, string key);

        [NotNull]
        T Get(string key);
    }

    public interface IService<T>
    {
        [NotNull]
        IList<T> GetAll();

        T Get(object id);
    }
}