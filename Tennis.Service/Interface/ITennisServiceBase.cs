using System;
using Tennis.Service.Enum;

namespace Tennis.Service.Interface
{
    public interface ITennisServiceBase
    {
        string GetScore();
        void Win(Func<Team, Team> scoring);
    }
}
