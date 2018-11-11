using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tennis.Play.Interface;
using Tennis.Service.Enum;

namespace Tennis.Play.Test
{
    [TestClass]
    public class PlayGameTest
    {
        private Mock<IDetermineWinner> _determineWinner;
        private PlayGame _target;

        [TestInitialize]
        public void SetupTests()
        {
            _determineWinner = new Mock<IDetermineWinner>();
            _target = new PlayGame(_determineWinner.Object);
        }

        [TestCleanup]
        public void CleanuupTests()
        {
            _determineWinner.Verify();
            _target = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GamePlay_Constructor_Null_IDetermineWinner_Contract_Checked()
        {
            //Act
            _target = new PlayGame(null);
        }

        [TestMethod]
        public void GamePlay_Team_One_Wins()
        {
            //Arrange
            _determineWinner.Setup(w => w.ForPoint())
                .Returns(Team.One);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(Team.One, result);
            Assert.AreEqual(5, _target.GetPointScores().Count());
        }

        [TestMethod]
        public void GamePlay_Team_Two_Wins()
        {
            //Arrange
            _determineWinner.Setup(w => w.ForPoint())
                .Returns(Team.Two);

            //Act
            var result = _target.Play();

            //Assert
            Assert.AreEqual(result, Team.Two);
            Assert.AreEqual(5, _target.GetPointScores().Count());
        }
    }
}
