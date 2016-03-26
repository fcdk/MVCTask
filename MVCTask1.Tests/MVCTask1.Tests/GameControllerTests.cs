using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVCTask1.Controllers;
using MVCTask1Model.RepositoryInterfaces;
using MVCTask1Model.UnitOfWork;
using Newtonsoft.Json;
using MVCTask1EF;
using MVCTask1Model.Repositories;

namespace MVCTask1.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public void GetAllGames_must_return_JsonResult_all_games_with_name_prop()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            var games = new List<Game>()
            {
                new Game { Name = "NameTest1", Description = "DescriptionTest1" },
                new Game { Name = "NameTest2", Description = "DescriptionTest2" }
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
        public void CreateGame_must_call_once_unitOfWork_Games_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            Game game = new Game();
            mockGameRepository.Setup(x => x.Insert(It.IsNotNull<Game>()))
                .Callback<Game>(obj => game = obj);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.CreateGame("TestName", "TestDescription");

            mockGameRepository.Verify(x => x.Insert(It.IsAny<Game>()), Times.Once());
            Assert.AreEqual("TestName", game.Name);
            Assert.AreEqual("TestDescription", game.Description);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }


        ////[TestMethod]
        ////public void CreateGame_name_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        ////{
        ////    var mockUnitOfWork = new Mock<IUnitOfWork>();
        ////    var mockGameRepository = new Mock<IGameRepository>();
        ////    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        ////    mockGameRepository.Setup(x => x.Insert(It.IsNotNull<Game>()))
        ////        .Callback<Game>(game =>
        ////        {
        ////            if (game.Name == null)
        ////                throw new ArgumentException("name argument mustn`t be null and empty");
        ////        });

        ////    var controller = new GameController(mockUnitOfWork.Object);

        ////    controller.CreateGame(null, "TestDescription");

        ////    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        ////}

        ////[TestMethod]
        ////public void CreateGame_name_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        ////{
        ////    var mockUnitOfWork = new Mock<IUnitOfWork>();
        ////    var mockGameRepository = new Mock<IGameRepository>();
        ////    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        ////    mockGameRepository.Setup(x => x.Create(String.Empty, It.IsAny<string>()))
        ////      .Throws(new ArgumentException("name argument mustn`t be null and empty"));

        ////    var controller = new GameController(mockUnitOfWork.Object);

        ////    controller.CreateGame(string.Empty, "TestName");

        ////    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        ////}

        [TestMethod]
        public void UpdateGame_name_and_key_are_not_null_and_string_empty_should_call_once_unitOfWork_Games_Update_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.UpdateGame("TestKey", "TestName", "TestDescription");

            mockGameRepository.Verify(x => x.Update("TestKey", "TestName", "TestDescription"), Times.Once());
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }

        //[TestMethod]
        //public void UpdateGame_key_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.Update(null, It.IsNotNull<string>(), It.IsAny<string>()))
        //      .Throws(new ArgumentException("key and name arguments must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.UpdateGame(null, "TestName", "TestDescription");           

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void UpdateGame_key_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.Update(string.Empty, It.IsNotNull<string>(), It.IsAny<string>()))
        //      .Throws(new ArgumentException("key and name arguments must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.UpdateGame(string.Empty, "TestName", "TestDescription");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void UpdateGame_name_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.Update(It.IsNotNull<string>(), null, It.IsAny<string>()))
        //      .Throws(new ArgumentException("key and name arguments must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.UpdateGame("TestKey", null, "TestDescription");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void UpdateGame_name_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.Update(It.IsNotNull<string>(), String.Empty, It.IsAny<string>()))
        //      .Throws(new ArgumentException("key and name arguments must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.UpdateGame("TestKey", String.Empty, "TestDescription");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void GetGameByKey_key_is_not_string_empty_and_null_should_retun_JsonResult_with_same_key()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    var game = new Game { GameKey = "KeyTest", Name = "NameTest", Description = "DescriptionTest"};

        //    mockGameRepository.Setup(x => x.GetGameByKey(game.GameKey))
        //        .Returns(() => game);
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    string stringResult = (new JavaScriptSerializer()).Serialize(controller.GetGameByKey(game.GameKey).Data);
        //    dynamic gameJsonResult = JsonConvert.DeserializeObject(stringResult);

        //    Assert.AreEqual(game.GameKey, gameJsonResult.GameKey.ToString());

        //    mockGameRepository.Verify(x => x.GetGameByKey(game.GameKey), Times.Once());
        //}

        //[TestMethod]
        //public void GetGameByKey_key_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.GetGameByKey(null))
        //      .Throws(new ArgumentException("key argument must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.GetGameByKey(null);

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void GetGameByKey_key_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.GetGameByKey(string.Empty))
        //      .Throws(new ArgumentException("key argument must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.GetGameByKey(string.Empty);

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void DeleteGame_key_is_not_null_and_string_empty_should_call_once_unitOfWork_Games_Delete_and_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.DeleteGame("TestKey");

        //    mockGameRepository.Verify(x => x.Delete("TestKey"), Times.Once());
        //    mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        //}

        //[TestMethod]
        //public void DeleteGame_key_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.Delete(null))
        //      .Throws(new ArgumentException("key argument must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.DeleteGame(null);

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void DeleteGame_key_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.Delete(string.Empty))
        //      .Throws(new ArgumentException("key argument must not be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.DeleteGame(string.Empty);

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void AddCommentToGame_gameKey_name_and_body_are_not_null_and_string_empty_should_call_once_unitOfWork_Comments_Create_and_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame("TestKey", "TestName", "TestBody", "TestParentCommentKey");

        //    mockCommnentRepository.Verify(x => x.Create("TestKey", "TestName", "TestBody", "TestParentCommentKey"), Times.Once());
        //    mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        //}

        //[TestMethod]
        //public void AddCommentToGame_gameKey_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.Create(null, It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsAny<string>()))
        //      .Throws(new ArgumentException("gameKey, name and body arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame(null, "TestName", "TestBody", "TestParentCommentKey");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void AddCommentToGame_gameKey_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.Create(String.Empty, It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsAny<string>()))
        //      .Throws(new ArgumentException("gameKey, name and body arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame(String.Empty, "TestName", "TestBody", "TestParentCommentKey");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void AddCommentToGame_name_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.Create(It.IsNotNull<string>(), null, It.IsNotNull<string>(), It.IsAny<string>()))
        //      .Throws(new ArgumentException("gameKey, name and body arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame("TestKey", null, "TestBody", "TestParentCommentKey");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void AddCommentToGame_name_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.Create(It.IsNotNull<string>(), String.Empty, It.IsNotNull<string>(), It.IsAny<string>()))
        //      .Throws(new ArgumentException("gameKey, name and body arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame("TestKey", String.Empty, "TestBody", "TestParentCommentKey");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void AddCommentToGame_body_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.Create(It.IsNotNull<string>(), It.IsNotNull<string>(), null, It.IsAny<string>()))
        //      .Throws(new ArgumentException("gameKey, name and body arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame("TestKey", "TestName", null, "TestParentCommentKey");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void AddCommentToGame_body_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.Create(It.IsNotNull<string>(), It.IsNotNull<string>(), String.Empty, It.IsAny<string>()))
        //      .Throws(new ArgumentException("gameKey, name and body arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.AddCommentToGame("TestKey", "TestName", String.Empty, "TestParentCommentKey");

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void GetAllCommentsByGame_key_is_not_null_and_string_empty_must_return_JsonResult_all_comments_with_CommentKey_and_GameKey_props()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    var comments = new List<Comment>
        //    {
        //        new Comment { CommentKey = "CommentKeyTest1", GameKey = "GameKeyTest1" },
        //        new Comment { CommentKey = "CommentKeyTest2", GameKey = "GameKeyTest1" }
        //    };
        //    var game = new Game { GameKey = "GameKeyTest1" };

        //    mockCommnentRepository.Setup(x => x.GetCommentsByGame("GameKeyTest1"))
        //        .Returns(() => comments.AsEnumerable());
        //    mockGameRepository.Setup(x => x.GetGameByKey("GameKeyTest1"))
        //        .Returns(() => game);
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    string stringResult = (new JavaScriptSerializer()).Serialize(controller.GetAllCommentsByGame("GameKeyTest1").Data);
        //    dynamic commentsJsonResult = JsonConvert.DeserializeObject(stringResult);
        //    List<Comment> commentListResult = commentsJsonResult.ToObject<List<Comment>>();

        //    Assert.AreEqual(commentListResult.Count, 2);
        //    foreach (var comment in commentListResult)
        //    {
        //        Assert.IsTrue(comments.Any(x => x.CommentKey == comment.CommentKey));
        //        Assert.AreEqual("GameKeyTest1", comment.GameKey);
        //    }

        //    mockCommnentRepository.Verify(x => x.GetCommentsByGame("GameKeyTest1"), Times.Once());
        //}

        //[TestMethod]
        //public void GetAllCommentsByGame_key_is_null_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.GetCommentsByGame(null))
        //      .Throws(new ArgumentException("key arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.GetAllCommentsByGame(null);

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void GetAllCommentsByGame_key_is_empty_string_should_not_throw_ArgumentException_and_not_call_unitOfWork_Save()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockCommnentRepository = new Mock<ICommentRepository>();
        //    mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
        //    mockCommnentRepository.Setup(x => x.GetCommentsByGame(string.Empty))
        //        .Throws(new ArgumentException("key arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.GetAllCommentsByGame(string.Empty);

        //    mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        //}

        //[TestMethod]
        //public void DownloadGame_key_is_not_null_and_string_empty_must_return_FileResult()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.GetGameByKey("TestKey1")).Returns(new Game { GameKey = "TestKey1" });

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    Assert.IsInstanceOfType(controller.DownloadGame("TestKey1"), typeof(FileResult));
        //}

        //[TestMethod]
        //public void DownloadGame_key_is_null_should_not_throw_ArgumentException()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.GetGameByKey(null))
        //        .Throws(new ArgumentException("key arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.DownloadGame(null);
        //}

        //[TestMethod]
        //public void DownloadGame_key_is_empty_string_should_not_throw_ArgumentException()
        //{
        //    var mockUnitOfWork = new Mock<IUnitOfWork>();
        //    var mockGameRepository = new Mock<IGameRepository>();
        //    mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
        //    mockGameRepository.Setup(x => x.GetGameByKey(String.Empty))
        //        .Throws(new ArgumentException("key arguments mustn`t be null and empty"));

        //    var controller = new GameController(mockUnitOfWork.Object);

        //    controller.DownloadGame(String.Empty);
        //}

    }
}
