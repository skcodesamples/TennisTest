using System;
using Tennis.Service.Enum;
using Tennis.Service.Interface;

namespace Tennis.Service
{
    public class MatchService : IMatchService
    {
        public MatchService()
        {
            State = MatchState.Playing;
        }

        public int TeamOneScore { get; private set; }
        public int TeamTwoScore { get; private set; }
        public MatchState State { get; private set; }



        public void Win(Func<Team, Team> scoring)
        {
            WinSet(scoring);
        }

        private void WinSet(Func<Team, Team> scoring)
        {
            var scoringTeam = scoring(Team.None);
            RefineMatchState(scoringTeam);
        }

        private void RefineMatchState(Team team)
        {
            switch (team)
            {
                case Team.One:
                    TeamOneScore++;
                    break;
                case Team.Two:
                    TeamTwoScore++;
                    break;
                case Team.None:
                default:
                    break;
            }


            if (TeamOneScore >= 2)
                State = MatchState.MatchWonByTeamOne;

            if (TeamTwoScore >= 2)
                State = MatchState.MatchWonByTeamTwo;
        }

        public string GetScore() => $"{TeamOneScore.ToString()} - {TeamTwoScore.ToString()}";
    }
}

