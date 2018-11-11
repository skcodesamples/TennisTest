using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tennis.Play.Interface;
using Tennis.Service.Enum;

namespace Tennis.Play.Test
{
    [TestClass]
    public class PlayMatchTest
    {
        IPlayMatch _target;
        Mock<IPlaySet> _playSet;
        Mock<ITeam> _teamOne;
        Mock<ITeam> _teamTwo;

        [TestInitialize]
        public void TestInitialize()
        {
            _playSet = new Mock<IPlaySet>();
            _teamOne = new Mock<ITeam>();
            _teamTwo = new Mock<ITeam>();
            _target = new PlayMatch(_playSet.Object, _teamOne.Object, _teamTwo.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _playSet.Verify();
            _target = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_IPlaySet_Checked()
        {
            //Act
            _target = new PlayMatch(null, _teamOne.Object, _teamTwo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_TeamOne_Checked()
        {
            //Act
            _target = new PlayMatch(_playSet.Object, null, _teamTwo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_TeamTwo_Checked()
        {
            //Act
            _target = new PlayMatch(_playSet.Object, _teamOne.Object, null);
        }

        [TestMethod]
        public void Team_One_Wins()
        {
            //Arrage
            _playSet.Setup(s => s.Play()).Returns(Team.One);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(_teamOne.Object, result);
            Assert.AreEqual(3, _target.GetSetScores().Count());
        }

        [TestMethod]
        public void Team_Two_Wins()
        {
            //Arrage
            _playSet.Setup(s => s.Play()).Returns(Team.Two);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(_teamTwo.Object, result);
            Assert.AreEqual(3, _target.GetSetScores().Count());
        }
        [TestMethod]
        public void MatchShouldHaveUptoThreeSets()
        {
            //Arrage
            _playSet.Setup(s => s.Play()).Returns(Team.Two);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(3, _target.GetSetScores().Count());
        }

    }
}
