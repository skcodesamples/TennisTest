using System;
using Tennis.Service.Enum;
using Tennis.Service.Interface;

namespace Tennis.Service
{
    public class SetService : ISetService
    {
        public SetService()
        {
            State = SetState.Playing;
        }

        public void Win(Func<Team, Team> scoring)
        {
            WinGame(scoring);
        }

        private void WinGame(Func<Team, Team> scoring)
        {
            var scoringTeam = scoring(Team.None);
            RefineSetState(scoringTeam);
        }

        public int TeamOneScore { get; private set; }

        public int TeamTwoScore { get; private set; }

        public SetState State { get; private set; }

        public string GetScore()
        {
            return $"{TeamOneScore.ToString()} - {TeamTwoScore.ToString()}";
        }

        private void RefineSetState(Team team)
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

            var diff = TeamOneScore - TeamTwoScore;
            //validate margin is 2 or more
            if (Math.Abs(diff) >= 2)
            {
                //team must have scored 6 or more games to win a set
                if (TeamOneScore >= 6 || TeamTwoScore >= 6)
                {
                    if (diff > 0)
                    {
                        State = SetState.SetWonByTeamOne;
                    }
                    else
                    {
                        State = SetState.SetWonByTeamTwo;
                    }
                }
            }
        }
    }
}

