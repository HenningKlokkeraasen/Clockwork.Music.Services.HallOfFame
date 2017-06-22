using System.Collections.Generic;

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
        IList<T> GetAll();
        T Get(int id);
    }

    public interface ICache<T>
    {
        void Store(IList<T> items, string key);
        IList<T> Get(string key);
    }
}