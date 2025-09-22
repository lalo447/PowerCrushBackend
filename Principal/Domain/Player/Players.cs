namespace NewSystem.Domain.PowerCrushPlayer
{
    /// <summary>
    ///   Player data entity.
    /// </summary>
    public class Players
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Points { get; private set; }


        public static Players? Create(string Name, int Points)
        {
            return new Players
            {
                Name = Name,
                Points = Points
            };
        }

        public void Update(int Points)
        {
            this.Points = Points;
        }
    }
}
