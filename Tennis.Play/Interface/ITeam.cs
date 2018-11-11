namespace Tennis.Play.Interface
{
    public interface ITeam
    {
        string TeamName { get; set; }
        bool IsServing { get; set; }
    }
}
