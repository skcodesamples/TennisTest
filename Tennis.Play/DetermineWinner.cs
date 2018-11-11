using System;
using Tennis.Play.Interface;
using Tennis.Service.Enum;

namespace Tennis.Play
{
    public class DetermineWinner : IDetermineWinner
    {
        private readonly Random random;
        private readonly ITeam _teamOne;
        private readonly ITeam _teamTwo;

        public DetermineWinner(ITeam teamOne, ITeam teamTwo)
        {
            random = new Random();

            _teamOne = teamOne;
            _teamTwo = teamTwo;
        }

        public Team ForPoint()
        {
            int randomNumber = random.Next(10000);
            return randomNumber % 2 == 1 ? Team.One : Team.Two;
        }
    }

}
