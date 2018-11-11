using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tennis.Service.Enum;
using Tennis.Service.Interface;

namespace Tennis.Service.Test
{
    [TestClass]
    public class GameServiceShould
    {
        private IGameService _target;


        [TestInitialize]
        public void Setup()
        {
            _target = new GameService();
        }

        [TestCleanup]
        public void TearDown()
        {
            _target = null;
        }

        [TestMethod]
        public void NewGameScoreShouldBeLoveToLove()
        {
            //Assert
            Assert.AreEqual(PointState.Love, _target.TeamOneScore);
            Assert.AreEqual(PointState.Love, _target.TeamTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, _target.State);
            Assert.AreEqual("Love - Love", _target.GetScore());
        }


        [TestMethod]
        public void TeamOneShouldWinWithPointScoreFifteenToLove()
        {
            //Act
            TeamOneWinsPoints(1);

            //Assert
            Assert.AreEqual(PointState.Fifteen, _target.TeamOneScore);
            Assert.AreEqual(PointState.Love, _target.TeamTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, _target.State);
            Assert.AreEqual("Fifteen - Love", _target.GetScore());
        }

        [TestMethod]
        public void TeamOneShouldWinGameWithPointScoreFourtyToLove()
        {
            //Act
            TeamOneWinsPoints(4);

            //Assert
            Assert.AreEqual(GameState.GameWonByTeamOne, _target.State);
            Assert.AreEqual("game - team one", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwoShouldWinGameWithPointScoreFourtyToLove()
        {
            //Act
            TeamTwoWinsPoints(4);

            //Assert
            Assert.AreEqual(GameState.GameWonByTeamTwo, _target.State);
            Assert.AreEqual("game - team two", _target.GetScore());
        }

        
        [TestMethod]
        public void TeamOneWithTwoPointWiningsShouHavePointScoreThirtyToFifteen()
        {
            //Act
            TeamOneWinsPoints(2);
            TeamTwoWinsPoints(1);


            //Assert
            Assert.AreEqual(PointState.Thirty, _target.TeamOneScore);
            Assert.AreEqual(PointState.Fifteen, _target.TeamTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, _target.State);
            Assert.AreEqual("Thirty - Fifteen", _target.GetScore());
        }

        [TestMethod]
        public void WithTwoWiningsofBothTeamsPointScoreShouldBeThirtyToThirty()
        {
            //Act
            TeamOneWinsPoints(2);
            TeamTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(PointState.Thirty, _target.TeamOneScore);
            Assert.AreEqual(PointState.Thirty, _target.TeamTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, _target.State);
            Assert.AreEqual("Thirty - Thirty", _target.GetScore());
        }

        [TestMethod]
        public void WithThreeToTwoPointScoreShouldBeFourtyToThirty()
        {
            //Act
            TeamOneWinsPoints(3);
            TeamTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(PointState.Fourty, _target.TeamOneScore);
            Assert.AreEqual(PointState.Thirty, _target.TeamTwoScore);
            Assert.AreEqual(GameState.PriorToDeuce, _target.State);
            Assert.AreEqual("Fourty - Thirty", _target.GetScore());
        }

        [TestMethod]
        public void WithThreeThreeWiningsPointScoreShouldBeDeuce()
        {
            //Act
            TeamOneWinsPoints(3);
            TeamTwoWinsPoints(3);


            //Assert
            Assert.AreEqual(PointState.Deuce, _target.TeamOneScore);
            Assert.AreEqual(PointState.Deuce, _target.TeamTwoScore);
            Assert.AreEqual(GameState.Deuce, _target.State);
            Assert.AreEqual("deuce", _target.GetScore());
        }

        [TestMethod]
        public void WithWiningPointScoreOverDueceTeamOneShoudHaveAdvantage()
        {
            //Act
            GetToDeuce();
            TeamOneWinsPoints(1);


            //Assert
            Assert.AreEqual(GameState.Deuce, _target.State);
            Assert.AreEqual("advantage - team one", _target.GetScore());
        }

        [TestMethod]
        public void WithWiningPointScoreTeamTwoPointScoreShouldBeDeuce()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(1);


            //Assert
            Assert.AreEqual(GameState.Deuce, _target.State);
            Assert.AreEqual("deuce", _target.GetScore());
        }

        [TestMethod]
        public void WithWiningPointScoreOverDueceTeamTwoShoudHaveAdvantageTwo()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(1);
            TeamTwoWinsPoints(1);


            //Assert
            Assert.AreEqual(GameState.Deuce, _target.State);
            Assert.AreEqual("advantage - team two", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwoShouldWinGame()
        {
            //Act
            GetToDeuce();
            TeamTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(GameState.GameWonByTeamTwo, _target.State);
            Assert.AreEqual("game - team two", _target.GetScore());
        }

        [TestMethod]
        public void TeamOneShouldWinGame()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(1);
            TeamOneWinsPoints(2);


            //Assert
            Assert.AreEqual(GameState.GameWonByTeamOne, _target.State);
            Assert.AreEqual("game - team one", _target.GetScore());
        }

        [TestMethod]
        public void WithLongGameTeamOneHasAdvantage()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(20);
            TeamOneWinsPoints(1);

            //Assert
            Assert.AreEqual(GameState.Deuce, _target.State);
            Assert.AreEqual("advantage - team one", _target.GetScore());
        }

        [TestMethod]
        public void WithLongGameTeamTwoHasAdvantage()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(45);
            TeamTwoWinsPoints(1);

            //Assert
            Assert.AreEqual(GameState.Deuce, _target.State);
            Assert.AreEqual("advantage - team two", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwoShouldWinLongGame()
        {
            //Act
            GetToDeuce();
            HoldAtDeuce(45);
            TeamTwoWinsPoints(2);


            //Assert
            Assert.AreEqual(GameState.GameWonByTeamTwo, _target.State);
            Assert.AreEqual("game - team two", _target.GetScore());
        }

        private void GetToDeuce()
        {
            TeamOneWinsPoints(3);
            TeamTwoWinsPoints(3);
        }

        private void HoldAtDeuce(int times)
        {
            for (int i = 0; i < times; i++)
            {
                TeamOneWinsPoints(1);
                TeamTwoWinsPoints(1);
            }
        }

        private void TeamOneWinsPoints(int points)
        {
            for (int i = 0; i < points; i++)
            {
                _target.Win(s => Team.One);
            }
        }

        private void TeamTwoWinsPoints(int points)
        {
            for (int i = 0; i < points; i++)
            {
                _target.Win(s => Team.Two);
            }
        }
    }
}
