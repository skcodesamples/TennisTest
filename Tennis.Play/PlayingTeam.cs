using Tennis.Play.Interface;

namespace Tennis.Play
{
    public class PlayingTeam : ITeam
    {
        public string TeamName { get; set; }
        public bool IsServing { get; set; }
    }

}
