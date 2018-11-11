using System.Collections.Generic;
using Tennis.Service.Enum;

namespace Tennis.Play.Interface
{
    public interface IPlayGame
    {
        Team Play();
        IEnumerable<string> GetPointScores();
    }
}
