using System.Collections.Generic;
using Tennis.Service.Enum;

namespace Tennis.Play.Interface
{
    public interface IPlaySet
    {
        Team Play();
        IEnumerable<GameScore> GetGameScores();
    }
}
