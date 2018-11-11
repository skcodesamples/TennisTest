using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tennis.Play.Interface;
using Tennis.Service.Enum;

namespace Tennis.Play.Test
{
    [TestClass]
    public class PlaySetTest
    {
        IPlaySet _target;
        Mock<IPlayGame> _playGame;

        [TestInitialize]
        public void SetupTestMethod()
        {
            _playGame = new Mock<IPlayGame>();
            _target = new PlaySet(_playGame.Object);
        }

        [TestCleanup]
        public void CleanupTestMethod()
        {
            _playGame.Verify();
            _target = null;
        }

       
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_PlayGame_Contract_Checked()
        {
            //Act
            _target = new PlaySet(null);
        }

        [TestMethod]
        public void Team_One_Wins()
        {
            //Assert
            _playGame.Setup(g => g.Play()).Returns(Team.One);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(Team.One, result);
            Assert.AreEqual(7, _target.GetGameScores().Count());
        }

        [TestMethod]
        public void Team_Two_Wins()
        {
            //Assert
            _playGame.Setup(g => g.Play()).Returns(Team.Two);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(Team.Two, result);
            Assert.AreEqual(7, _target.GetGameScores().Count());
        }
    }
}
