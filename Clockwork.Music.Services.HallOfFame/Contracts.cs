using System.Collections.Generic;
using JetBrains.Annotations;

namespace Clockwork.Music.Services.HallOfFame
{
    public class HallOfFame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InfoUrl { get; set; }

        public static HallOfFame Empty => new HallOfFame
        {
            Id = -1,
            Name = string.Empty,
            InfoUrl = string.Empty
        };
    }
    
    public interface IRepository<T>
    {
        [NotNull]
        IList<T> GetAll();
    }

    public interface ICache<T>
    {
        void Store(IList<T> items, string key);

        [NotNull]
        IList<T> Get(string key);
    }

    public interface IService<T>
    {
        [NotNull]
        IList<T> GetAll();

        T Get(int id);
    }
}