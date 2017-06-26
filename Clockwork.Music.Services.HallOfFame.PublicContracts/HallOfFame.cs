namespace Clockwork.Music.Services.HallOfFame.PublicContracts
{
    public class HallOfFame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InfoUrl { get; set; }
        public int YearEstabllished { get; set; }

        public static HallOfFame Empty => new HallOfFame
        {
            Id = -1,
            Name = string.Empty,
            InfoUrl = string.Empty,
            YearEstabllished = -1
        };
    }
}