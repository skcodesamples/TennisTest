using Tennis.Service.Enum;

namespace Tennis.Service.Interface
{
    public interface IGameService : ITennisServiceBase
    {
        PointState TeamOneScore { get; }
        PointState TeamTwoScore { get; }
        GameState State { get; }
    }
}
