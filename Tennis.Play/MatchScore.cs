using System.Collections.Generic;
using Tennis.Play.Interface;

namespace Tennis.Play
{
    public class MatchScore
    {
        public IEnumerable<SetScore> SetScores { get; set; }
        public ITeam SideOne { get; set; }
        public ITeam SideTwo { get; set; }
    }
}
