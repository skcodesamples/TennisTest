using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tennis.Service.Enum;
using Tennis.Service.Interface;

namespace Tennis.Service.Test
{
    [TestClass]
    public class SetTest
    {
        ISetService _target;

        [TestInitialize]
        public void TestSetup()
        {
            _target = new SetService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _target = null;
        }

        [TestMethod]
        public void TeamOne_Wins_Game_Score_Is_1_0()
        {
            //Act
            TeamOneWinsGames(1);

            //Assert
            Assert.AreEqual(1, _target.TeamOneScore);
            Assert.AreEqual(0, _target.TeamTwoScore);
            Assert.AreEqual(SetState.Playing, _target.State);
            Assert.AreEqual("1 - 0", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwo_Wins_Game_Score_Is_1_1()
        {
            //Act
            TeamOneWinsGames(1);
            TeamTwoWinsGames(1);

            //Assert
            Assert.AreEqual(1, _target.TeamOneScore);
            Assert.AreEqual(1, _target.TeamTwoScore);
            Assert.AreEqual(SetState.Playing, _target.State);
            Assert.AreEqual("1 - 1", _target.GetScore());
        }

        [TestMethod]
        public void TeamOne_Wins_Set_Score_Is_6_0()
        {
            //Act
            TeamOneWinsGames(6);

            //Assert
            Assert.AreEqual(6, _target.TeamOneScore);
            Assert.AreEqual(0, _target.TeamTwoScore);
            Assert.AreEqual(SetState.SetWonByTeamOne, _target.State);
            Assert.AreEqual("6 - 0", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwo_Wins_Set_Score_Is_0_6()
        {
            //Act
            TeamTwoWinsGames(6);

            //Assert
            Assert.AreEqual(0, _target.TeamOneScore);
            Assert.AreEqual(6, _target.TeamTwoScore);
            Assert.AreEqual(SetState.SetWonByTeamTwo, _target.State);
            Assert.AreEqual("0 - 6", _target.GetScore());
        }

        [TestMethod]
        public void TeamOne_Wins_Game_Score_Is_6_5()
        {
            //Act
            MoveTo5All();
            TeamOneWinsGames(1);

            //Assert
            Assert.AreEqual(6, _target.TeamOneScore);
            Assert.AreEqual(5, _target.TeamTwoScore);
            Assert.AreEqual(SetState.Playing, _target.State);
            Assert.AreEqual("6 - 5", _target.GetScore());
        }

        [TestMethod]
        public void TeamOne_Wins_Set_Score_Is_7_5()
        {
            //Act
            MoveTo5All();
            TeamOneWinsGames(2);

            //Assert
            Assert.AreEqual(7, _target.TeamOneScore);
            Assert.AreEqual(5, _target.TeamTwoScore);
            Assert.AreEqual(SetState.SetWonByTeamOne, _target.State);
            Assert.AreEqual("7 - 5", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwo_Wins_Set_Score_Is_6_8()
        {
            //Act
            MoveTo5All();
            TeamOneWinsGames(1);
            TeamTwoWinsGames(3);

            //Assert
            Assert.AreEqual(6, _target.TeamOneScore);
            Assert.AreEqual(8, _target.TeamTwoScore);
            Assert.AreEqual(SetState.SetWonByTeamTwo, _target.State);
            Assert.AreEqual("6 - 8", _target.GetScore());
        }

        [TestMethod]
        public void NewSet_Game_Score_0_0()
        {
            //Assert
            Assert.AreEqual(0, _target.TeamOneScore);
            Assert.AreEqual(0, _target.TeamTwoScore);
            Assert.AreEqual(SetState.Playing, _target.State);
            Assert.AreEqual("0 - 0", _target.GetScore());
        }

        private void MoveTo5All()
        {
            TeamOneWinsGames(5);
            TeamTwoWinsGames(5);
        }

        private void TeamOneWinsGames(int points)
        {
            for (int i = 0; i < points; i++)
            {
                _target.Win(s => Team.One);
            }
        }

        private void TeamTwoWinsGames(int points)
        {
            for (int i = 0; i < points; i++)
            {
                _target.Win(s => Team.Two);
            }
        }
    }
}

