using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVCTask1.Controllers;
using MVCTask1EF;
using MVCTask1Model.RepositoryInterfaces;
using MVCTask1Model.UnitOfWork;
using Newtonsoft.Json;

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
            mockGameRepository.VerifyAll();
        }
    }
}
