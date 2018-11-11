using System;
using System.Collections.Generic;
using Tennis.Play.Interface;
using Tennis.Service;
using Tennis.Service.Enum;

namespace Tennis.Play
{
    public class PlayGame : IPlayGame
    {
        private readonly IDetermineWinner _determineWinner;
        private GameService _game;
        private List<string> pointScores;

        public PlayGame(IDetermineWinner determineWinner)
        {
            _determineWinner = determineWinner ?? throw new ArgumentNullException("determineWinner");
        }

        public PlayGame(ITeam teamOne, ITeam teamTwo) : this(new DetermineWinner(teamOne, teamTwo))
        {
        }

        public Team Play()
        {
            _game = new GameService();
            pointScores = new List<string>
            {
                _game.GetScore()
            };

            while (_game.State != GameState.GameWonByTeamOne
                        && _game.State != GameState.GameWonByTeamTwo)
            {
                var team = _determineWinner.ForPoint();
                _game.Win(s => team);
                pointScores.Add(_game.GetScore());
            }

            return (_game.State == GameState.GameWonByTeamOne) ? Team.One : Team.Two;
        }

        public IEnumerable<string> GetPointScores() => pointScores;
    }

}
