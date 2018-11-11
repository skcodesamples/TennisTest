using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tennis.Service.Enum;
using Tennis.Service.Interface;

namespace Tennis.Service.Test
{
    [TestClass]
    public class MatchTest
    {
        IMatchService _target;

        [TestInitialize]
        public void TestSetup()
        {
            _target = new MatchService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _target = null;
        }

        [TestMethod]
        public void NewMatch_Score_Is_0_0()
        {
            //Assert
            Assert.AreEqual(0, _target.TeamOneScore);
            Assert.AreEqual(0, _target.TeamTwoScore);
            Assert.AreEqual(MatchState.Playing, _target.State);
            Assert.AreEqual("0 - 0", _target.GetScore());
        }

        [TestMethod]
        public void TeamOne_Wins_Set_Score_Is_1_0()
        {
            //Act
            TeamOneWinsSets(1);

            //Assert
            Assert.AreEqual(1, _target.TeamOneScore);
            Assert.AreEqual(0, _target.TeamTwoScore);
            Assert.AreEqual(MatchState.Playing, _target.State);
            Assert.AreEqual("1 - 0", _target.GetScore());
        }

        [TestMethod]
        public void TeamOne_Wins_Set_Score_Is_2_0_Match_Won()
        {
            //Act
            TeamOneWinsSets(2);

            //Assert
            Assert.AreEqual(2, _target.TeamOneScore);
            Assert.AreEqual(0, _target.TeamTwoScore);
            Assert.AreEqual(MatchState.MatchWonByTeamOne, _target.State);
            Assert.AreEqual("2 - 0", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwo_Wins_Set_Score_Is_0_1()
        {
            //Act
            TeamTwoWinsSets(1);

            //Assert
            Assert.AreEqual(0, _target.TeamOneScore);
            Assert.AreEqual(1, _target.TeamTwoScore);
            Assert.AreEqual(MatchState.Playing, _target.State);
            Assert.AreEqual("0 - 1", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwo_Wins_Set_Score_Is_0_2_Match_Won()
        {
            //Act
            TeamTwoWinsSets(2);

            //Assert
            Assert.AreEqual(0, _target.TeamOneScore);
            Assert.AreEqual(2, _target.TeamTwoScore);
            Assert.AreEqual(MatchState.MatchWonByTeamTwo, _target.State);
            Assert.AreEqual("0 - 2", _target.GetScore());
        }

        [TestMethod]
        public void TeamTwo_Wins_Set_Score_Is_1_2_Match_Won()
        {
            //Act
            TeamTwoWinsSets(1);
            TeamOneWinsSets(1);
            TeamTwoWinsSets(1);

            //Assert
            Assert.AreEqual(1, _target.TeamOneScore);
            Assert.AreEqual(2, _target.TeamTwoScore);
            Assert.AreEqual(MatchState.MatchWonByTeamTwo, _target.State);
            Assert.AreEqual("1 - 2", _target.GetScore());
        }

        private void TeamOneWinsSets(int points)
        {
            for (int i = 0; i < points; i++)
            {
                _target.Win(s => Team.One);
            }
        }

        private void TeamTwoWinsSets(int points)
        {
            for (int i = 0; i < points; i++)
            {
                _target.Win(s => Team.Two);
            }
        }
    }
}

