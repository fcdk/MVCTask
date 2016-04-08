namespace MVCTask.Models.Game
{
    public class GameDetailsViewModel
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public short? UnitsInStock { get; set; }
        public bool? Discontinued { get; set; }
    }
}