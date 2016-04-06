using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MVCTask.Controllers;
using MVCTaskModel.RepositoryInterfaces;
using MVCTaskModel.UnitOfWork;
using Newtonsoft.Json;
using MVCTaskEF;

namespace MVCTask.Tests
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
        public void CreateGame_name_is_not_null_and_empty_must_call_once_unitOfWork_Games_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            Game game = new Game();
            mockGameRepository.Setup(x => x.Insert(It.IsNotNull<Game>()))
                .Callback<Game>(obj => game = obj);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.CreateGame("TestName", "TestDescription");

            mockGameRepository.Verify(x => x.Insert(It.IsNotNull<Game>()), Times.Once());
            Assert.AreEqual("TestName", game.Name);
            Assert.AreEqual("TestDescription", game.Description);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateGame_name_is_null_must_throw_ArgumentException_and_not_call_unitOfWork_Games_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.CreateGame(null, "TestDescription");

            mockGameRepository.Verify(x => x.Insert(It.IsAny<Game>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateGame_name_is_empty_must_throw_ArgumentException_and_not_call_unitOfWork_Games_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.CreateGame(string.Empty, "TestDescription");

            mockGameRepository.Verify(x => x.Insert(It.IsAny<Game>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        public void UpdateGame_name_and_key_are_not_null_and_empty_must_call_once_unitOfWork_Games_Update_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            Game game = new Game();
            mockGameRepository.Setup(x => x.Update(It.IsNotNull<Game>()))
                .Callback<Game>(obj => game = obj);
            mockGameRepository.Setup(x => x.GetByKey("TestKey")).Returns(new Game {GameKey = "TestKey"});

            var controller = new GameController(mockUnitOfWork.Object);

            controller.UpdateGame("TestKey", "TestName", "TestDescription");

            mockGameRepository.Verify(x => x.Update(It.IsNotNull<Game>()), Times.Once());
            Assert.AreEqual("TestKey", game.GameKey);
            Assert.AreEqual("TestName", game.Name);
            Assert.AreEqual("TestDescription", game.Description);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateGame_name_is_null_must_throw_ArgumentException_and_not_call_unitOfWork_Games_Update_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.UpdateGame("TestKey", null, "TestDescription");

            mockGameRepository.Verify(x => x.Update(It.IsAny<Game>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateGame_name_is_empty_must_throw_ArgumentException_and_not_call_unitOfWork_Games_Update_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.UpdateGame("TestKey", string.Empty, "TestDescription");

            mockGameRepository.Verify(x => x.Update(It.IsAny<Game>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        public void GetGameByKey_key_is_not_empty_and_null_must_return_JsonResult_with_same_key()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            var game = new Game { GameKey = "KeyTest", Name = "NameTest", Description = "DescriptionTest" };

            mockGameRepository.Setup(x => x.GetByKey(game.GameKey)).Returns(() => game);
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            string stringResult = (new JavaScriptSerializer()).Serialize(controller.GetGameByKey(game.GameKey).Data);
            dynamic gameJsonResult = JsonConvert.DeserializeObject(stringResult);

            Assert.AreEqual(game.GameKey, gameJsonResult.GameKey.ToString());

            mockGameRepository.Verify(x => x.GetByKey(game.GameKey), Times.Once());
        }        

        [TestMethod]
        public void DeleteGame_key_is_not_null_and_empty_must_call_once_unitOfWork_Games_Delete_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.DeleteGame("TestKey");

            mockGameRepository.Verify(x => x.Delete("TestKey"), Times.Once());
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }        

        [TestMethod]
        public void AddCommentToGame_gameKey_name_body_and_parentCommentKey_are_not_null_and_empty_must_call_once_unitOfWork_Comments_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommnentRepository = new Mock<ICommentRepository>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            Comment comment = new Comment();
            mockCommnentRepository.Setup(x => x.Insert(It.IsNotNull<Comment>()))
                .Callback<Comment>(obj => comment = obj);
            mockGameRepository.Setup(x => x.GetByKey("TestKey")).Returns(new Game {GameKey = "TestKey"});
            mockCommnentRepository.Setup(x => x.GetByKey("TestParentCommentKey")).Returns(new Comment { CommentKey = "TestKey", Name = "TestName1"});

            var controller = new GameController(mockUnitOfWork.Object);

            controller.AddCommentToGame("TestKey", "TestName", "TestBody", "TestParentCommentKey");

            mockCommnentRepository.Verify(x => x.Insert(It.IsNotNull<Comment>()), Times.Once());
            Assert.AreEqual("TestKey", comment.GameKey);
            Assert.AreEqual("TestName", comment.Name);
            Assert.AreEqual("[TestName1] TestBody", comment.Body);
            Assert.AreEqual("TestParentCommentKey", comment.ParentCommentKey);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }

        [TestMethod]
        public void AddCommentToGame_gameKey_name_and_body_are_not_null_and_empty_parentCommentKey_is_null_must_call_once_unitOfWork_Comments_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommnentRepository = new Mock<ICommentRepository>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            Comment comment = new Comment();
            mockCommnentRepository.Setup(x => x.Insert(It.IsNotNull<Comment>()))
                .Callback<Comment>(obj => comment = obj);
            mockGameRepository.Setup(x => x.GetByKey("TestKey")).Returns(new Game { GameKey = "TestKey" });

            var controller = new GameController(mockUnitOfWork.Object);

            controller.AddCommentToGame("TestKey", "TestName", "TestBody");

            mockCommnentRepository.Verify(x => x.Insert(It.IsNotNull<Comment>()), Times.Once());
            Assert.AreEqual("TestKey", comment.GameKey);
            Assert.AreEqual("TestName", comment.Name);
            Assert.AreEqual("TestBody", comment.Body);
            Assert.AreEqual(null, comment.ParentCommentKey);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCommentToGame_name_is_null_must_throw_ArgumentException_and_not_call_unitOfWork_Comments_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommentRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.AddCommentToGame("TestKey", null, "TestBody", "TestParentCommentKey");

            mockCommentRepository.Verify(x => x.Insert(It.IsAny<Comment>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCommentToGame_name_is_empty_must_throw_ArgumentException_and_not_call_unitOfWork_Comments_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommentRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.AddCommentToGame("TestKey", string.Empty, "TestBody", "TestParentCommentKey");

            mockCommentRepository.Verify(x => x.Insert(It.IsAny<Comment>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCommentToGame_body_is_null_must_throw_ArgumentException_and_not_call_unitOfWork_Comments_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommentRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.AddCommentToGame("TestKey", "TestName", null, "TestParentCommentKey");

            mockCommentRepository.Verify(x => x.Insert(It.IsAny<Comment>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCommentToGame_body_is_empty_must_throw_ArgumentException_and_not_call_unitOfWork_Comments_Insert_and_unitOfWork_Save()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommentRepository = new Mock<ICommentRepository>();
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommentRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            controller.AddCommentToGame("TestKey", "TestName", string.Empty, "TestParentCommentKey");

            mockCommentRepository.Verify(x => x.Insert(It.IsAny<Comment>()), Times.Never());
            mockUnitOfWork.Verify(x => x.Save(), Times.Never());
        }

        [TestMethod]
        public void GetAllCommentsByGame_key_is_not_null_and_empty_must_return_JsonResult_all_comments_with_CommentKey_and_GameKey_props()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockCommnentRepository = new Mock<ICommentRepository>();
            var mockGameRepository = new Mock<IGameRepository>();
            var comments = new List<Comment>
            {
                new Comment { CommentKey = "CommentKeyTest1", GameKey = "GameKeyTest1" },
                new Comment { CommentKey = "CommentKeyTest2", GameKey = "GameKeyTest1" }
            };
            var game = new Game { GameKey = "GameKeyTest1" };

            mockCommnentRepository.Setup(x => x.GetCommentsByGame("GameKeyTest1"))
                .Returns(() => comments.AsEnumerable());
            mockGameRepository.Setup(x => x.GetByKey("GameKeyTest1"))
                .Returns(() => game);
            mockUnitOfWork.Setup(x => x.Comments).Returns(mockCommnentRepository.Object);
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);

            var controller = new GameController(mockUnitOfWork.Object);

            string stringResult = (new JavaScriptSerializer()).Serialize(controller.GetAllCommentsByGame("GameKeyTest1").Data);
            dynamic commentsJsonResult = JsonConvert.DeserializeObject(stringResult);
            List<Comment> commentListResult = commentsJsonResult.ToObject<List<Comment>>();

            Assert.AreEqual(commentListResult.Count, 2);
            foreach (var comment in commentListResult)
            {
                Assert.IsTrue(comments.Any(x => x.CommentKey == comment.CommentKey));
                Assert.AreEqual("GameKeyTest1", comment.GameKey);
            }

            mockCommnentRepository.Verify(x => x.GetCommentsByGame("GameKeyTest1"), Times.Once());
        }

        [TestMethod]
        public void DownloadGame_key_is_not_null_and_string_empty_must_return_FileResult()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockGameRepository = new Mock<IGameRepository>();
            mockUnitOfWork.Setup(x => x.Games).Returns(mockGameRepository.Object);
            mockGameRepository.Setup(x => x.GetByKey("TestKey")).Returns(new Game { GameKey = "TestKey" });

            var controller = new GameController(mockUnitOfWork.Object);

            Assert.IsInstanceOfType(controller.DownloadGame("TestKey"), typeof(FileResult));
        }
        
    }
}
