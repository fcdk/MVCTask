using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using Moq;

namespace MVCTask.Tests
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void RegisterRoutes_games_should_map_GameController_GetAllGamesAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/games");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("GetAllGames", routeData.Values["action"]);
        }

        [TestMethod]
        public void RegisterRoutes_games_new_should_map_GameController_CreateGameAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/new");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("CreateGame", routeData.Values["action"]);
        }

        [TestMethod]
        public void RegisterRoutes_games_update_should_map_GameController_UpdateGameAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/update");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("UpdateGame", routeData.Values["action"]);
        }

        [TestMethod]
        public void RegisterRoutes_game_key_should_map_GameController_GetGameByKeyAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/1");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("GetGameByKey", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["key"]);
        }

        [TestMethod]
        public void RegisterRoutes_games_remove_should_map_GameController_DeleteGameAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/games/remove");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("DeleteGame", routeData.Values["action"]);
        }

        [TestMethod]
        public void RegisterRoutes_game_key_newcomment_should_map_GameController_AddCommentToGameAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/1/newcomment");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("AddCommentToGame", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["key"]);
        }

        [TestMethod]
        public void RegisterRoutes_game_key_comments_should_map_GameController_GetAllCommentsByGameAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/1/comments");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("GetAllCommentsByGame", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["key"]);
        }

        [TestMethod]
        public void RegisterRoutes_game_key_download_should_map_GameController_DownloadGameAction()
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var httpContextMock = new Mock<HttpContextBase>();

            httpContextMock.Setup(x => x.Request
                .AppRelativeCurrentExecutionFilePath)
                .Returns("~/game/1/download");

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("DownloadGame", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["key"]);
        }

    }
}