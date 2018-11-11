using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Service.Enum;

namespace Tennis.Service.Interface
{
    public interface IMatchService : ITennisServiceBase
    {
        int TeamOneScore { get; }
        int TeamTwoScore { get; }
        MatchState State { get; }
    }
}
