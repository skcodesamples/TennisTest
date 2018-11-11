using Tennis.Service.Enum;

namespace Tennis.Service.Interface
{
    public interface ISetService : ITennisServiceBase
    {
        int TeamOneScore { get; }
        int TeamTwoScore { get; }
        SetState State { get; }
    }
}
