using System;
using System.Collections.Generic;
using Tennis.Play.Interface;
using Tennis.Service;
using Tennis.Service.Enum;

namespace Tennis.Play
{
    public class PlayMatch : IPlayMatch
    {
        private readonly IPlaySet _playSet;
        private MatchService _match;
        private List<SetScore> _setScores;

        public PlayMatch(IPlaySet playSet, ITeam sideOne, ITeam sideTwo)
        {
            TeamOne = sideOne ?? throw new ArgumentNullException("sideOne");
            TeamTwo = sideTwo ?? throw new ArgumentNullException("sideTwo");
            _playSet = playSet ?? throw new ArgumentNullException("playSet");
        }

        public PlayMatch(ITeam sideOne, ITeam sideTwo)
            : this(new PlaySet(sideOne, sideTwo), sideOne, sideTwo)
        {
        }

        public ITeam Play()
        {
            _match = new MatchService();
            _setScores = new List<SetScore>
            {
                new SetScore { Score = _match.GetScore() }
            };

            //play sets and find winner: it should be upto three sets
            while (_match.State != MatchState.MatchWonByTeamOne
                        && _match.State != MatchState.MatchWonByTeamTwo)
            {
                var setWinner = _playSet.Play();
                _match.Win(s => setWinner);
                _setScores.Add(new SetScore()
                {
                    Score = _match.GetScore()
                            ,
                    GameScores = _playSet.GetGameScores()
                });
            }

            return _match.State == MatchState.MatchWonByTeamOne
                        ? TeamOne : TeamTwo;
        }

        public IEnumerable<SetScore> GetSetScores() => _setScores;

        public ITeam TeamOne { get; }

        public ITeam TeamTwo { get; }
    }

}
