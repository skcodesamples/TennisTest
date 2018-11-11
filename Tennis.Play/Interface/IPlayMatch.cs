using System.Collections.Generic;

namespace Tennis.Play.Interface
{
    public interface IPlayMatch
    {
        ITeam Play();
        IEnumerable<SetScore> GetSetScores();
        ITeam TeamOne { get; }
        ITeam TeamTwo { get; }
    }
}
