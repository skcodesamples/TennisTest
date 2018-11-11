using System.Collections.Generic;
using System.Text;

namespace Tennis.Play
{
    public class GameScore
    {
        public string Score { get; set; }
        public IEnumerable<string> PointScores { get; set; }
    }

}
