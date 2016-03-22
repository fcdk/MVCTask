using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVCTask1.Controllers;
using MVCTask1Model.RepositoryInterfaces;
using MVCTask1Model.UnitOfWork;
using Newtonsoft.Json;
using MVCTask1EF;

namespace MVCTask1.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public void GetAllGames_must_return_jsonresult_all_games_with_name_prop()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            var games = new List<Game>()
            {
                new Game { Name = "NameTest1", Description = "DescriptionTest1" },
                new Game { Name = "NameTest2", Description = "DescriptionTest2" },
            };

            mockGameRepository.Setup(x => x.GetAllGames())
                .Returns(() => games.AsEnumerable());
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            string stringResult = (new JavaScriptSerializer()).Serialize(controller.GetAllGames().Data);
            dynamic gamesJsonResult = JsonConvert.DeserializeObject(stringResult);
            List<Game> gameListResult = gamesJsonResult.ToObject<List<Game>>();

            Assert.AreEqual(gameListResult.Count, 2);
            foreach (var game in gameListResult)
            {
                Assert.IsTrue(games.Any(x => x.Name == game.Name));
            }

            mockGameRepository.Verify(x => x.GetAllGames(), Times.Once());
        }

        [TestMethod]
        public void CreateGame_create_is_successful_should_call_once_unitOfWork_Games_Create_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.CreateGame("TestName", "TestDescription");

            mockGameRepository.Verify(x => x.Create("TestName", "TestDescription"), Times.Once());
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void CreateGame_name_is_null_should_not_throw_exception_and_not_call_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            mockGameRepository.Setup(x => x.Create(null, It.IsAny<string>()))
              .Throws(new ArgumentException("name argument mustn`t be null and empty"));

            var controller = new GameController(mockUnitOfWork.Object);            

            try
            {
                controller.CreateGame(null, null);
            }
            catch (ArgumentException ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

    }
}
