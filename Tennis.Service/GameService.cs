using System;
using Tennis.Service.Enum;
using Tennis.Service.Interface;

namespace Tennis.Service
{
    public class GameService : IGameService
    {
        public GameService()
        {
            InitialGameSetup();
        }

        public PointState TeamOneScore { get; private set; }
        public PointState TeamTwoScore { get; private set; }
        public GameState State { get; private set; }

        public void Win(Func<Team, Team> scoring)
        {
            WinPoint(scoring);
        }

        private void WinPoint(Func<Team, Team> scoring)
        {
            var scoringTeam = scoring(Team.None);
            if (scoringTeam == Team.One)
            {
                RefineGameState(scoringTeam);
            }
            else if (scoringTeam == Team.Two)
            {
                RefineGameState(scoringTeam);
            }
        }

        public string GetScore()
        {
            if (State != GameState.PriorToDeuce)
            {
                return TranslateDeuceOrBetterScore();
            }

            string teamOne = TranslateScore(TeamOneScore);
            string teamTwo = TranslateScore(TeamTwoScore);
            return teamOne + " - " + teamTwo;
        }

        private string TranslateScore(PointState pointState)
        {
            switch (pointState)
            {
                case PointState.Love:
                    return nameof(PointState.Love);
                case PointState.Fifteen:
                    return nameof(PointState.Fifteen);
                case PointState.Thirty:
                    return nameof(PointState.Thirty);
                case PointState.Fourty:
                    return nameof(PointState.Fourty);
                default:
                    return String.Empty;
            }
        }

        private string TranslateDeuceOrBetterScore()
        {
            if (State == GameState.GameWonByTeamOne)
            {
                return "game - team one";
            }
            else if (State == GameState.GameWonByTeamTwo)
            {
                return "game - team two";
            }

            if (TeamOneScore == PointState.Advantage)
            {
                return "advantage - team one";
            }
            else if (TeamTwoScore == PointState.Advantage)
            {
                return "advantage - team two";
            }

            return "deuce";
        }

        private void InitialGameSetup()
        {
            TeamOneScore = PointState.Love;
            TeamTwoScore = PointState.Love;
            State = GameState.PriorToDeuce;
        }

        private void RefineGameState(Team team)
        {
            if (StillPlaying())
            {
                if (HasEnteredDeuce())
                {
                    State = GameState.Deuce;

                    if (GetPointState(team) == PointState.Advantage)
                    {
                        State = DetermineWinner(team);
                        return;
                    }

                    if ((TeamOneScore == PointState.Deuce && TeamTwoScore == PointState.Deuce)
                        || (TeamOneScore == PointState.Fourty && TeamTwoScore == PointState.Fourty))
                    {
                        SetPointState(team, PointState.Advantage);
                        return;
                    }

                    TeamOneScore = PointState.Deuce;
                    TeamTwoScore = PointState.Deuce;
                    return;
                }

                UpdatePoints(team);

                if (((int)GetPointState(team)) > 4)
                {
                    State = DetermineWinner(team);
                }
            }
        }

        private void UpdatePoints(Team team)
        {
            var pointState = GetPointState(team);
            switch (pointState)
            {
                case PointState.None:
                    SetPointState(team, PointState.Love);
                    break;
                case PointState.Love:
                    SetPointState(team, PointState.Fifteen);
                    break;
                case PointState.Fifteen:
                    SetPointState(team, PointState.Thirty);
                    break;
                case PointState.Thirty:
                    SetPointState(team, PointState.Fourty);
                    break;
                case PointState.Fourty:
                    SetPointState(team, PointState.Deuce);
                    break;
            }
        }

        private void SetPointState(Team team, PointState state)
        {
            if (team == Team.One)
                TeamOneScore = state;
            else
                TeamTwoScore = state;
        }

        private GameState DetermineWinner(Team team)
        {
            return team == Team.One
                        ? GameState.GameWonByTeamOne
                        : GameState.GameWonByTeamTwo;
        }

        private PointState GetPointState(Team team)
        {
            return team == Team.One
                        ? TeamOneScore
                        : TeamTwoScore;
        }

        private bool HasEnteredDeuce()
        {
            return ((int)TeamOneScore) >= 3 && ((int)TeamTwoScore) >= 3 ? true : false;
        }

        private bool StillPlaying()
        {
            return (State == GameState.Deuce || State == GameState.PriorToDeuce);
        }
    }
}

