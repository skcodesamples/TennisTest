using System;
using System.Collections.Generic;
using Tennis.Play.Interface;
using Tennis.Service;
using Tennis.Service.Enum;

namespace Tennis.Play
{
    public class PlaySet : IPlaySet
    {
        private readonly IPlayGame _playGame;
        private SetService _set;
        private List<GameScore> gameScores;
        private ITeam teamOne;
        private ITeam teamTwo;

        public PlaySet(IPlayGame playGame)
        {
            _playGame = playGame ?? throw new ArgumentNullException("playGame");
        }

        public PlaySet(ITeam teamOne, ITeam teamTwo) : this(new PlayGame(teamOne, teamTwo))
        {
            this.teamOne = teamOne;
            this.teamTwo = teamTwo;
        }
        public Team Play()
        {
            _set = new SetService();
            gameScores = new List<GameScore>
            {
                new GameScore { Score = _set.GetScore() }
            };
            SetInitialServingTeam();

            while (_set.State != SetState.SetWonByTeamOne && _set.State != SetState.SetWonByTeamTwo)
            {
                var gameWinner = _playGame.Play();
                _set.Win(s => gameWinner);
                ToggleServingTeam();

                gameScores.Add(new GameScore
                {
                    Score = _set.GetScore(),
                    PointScores = _playGame.GetPointScores()
                });
            }

            return _set.State == SetState.SetWonByTeamOne ? Team.One : Team.Two;
        }


        private void ToggleServingTeam()
        {
            if (teamOne != null && teamTwo != null)
            {

                if (teamOne.IsServing)
                {
                    teamTwo.IsServing = true;
                    teamOne.IsServing = false;
                }
                else
                {
                    teamTwo.IsServing = false;
                    teamOne.IsServing = true;
                }
            }
        }

        private void SetInitialServingTeam()
        {
            if (teamOne != null && teamTwo != null)
            {
                Random random = new Random();
                var coinToss = random.Next(1000);

                if (coinToss % 2 == 0)
                {
                    teamTwo.IsServing = true;
                }
                else
                {
                    teamOne.IsServing = true;
                }
            }
        }

        public IEnumerable<GameScore> GetGameScores() => gameScores;
    }

}
