using System.Web.Mvc;
using System.Web.Routing;

namespace MVCTask1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AllGames",
                url: "games",
                defaults: new { controller = "Game", action = "GetAllGames" }
            );

            routes.MapRoute(
                name: "CreateGame",
                url: "games/new",
                defaults: new { controller = "Game", action = "CreateGame" }
            );

            routes.MapRoute(
                name: "UpdateGame",
                url: "games/update",
                defaults: new { controller = "Game", action = "UpdateGame" }
            );

            routes.MapRoute(
                name: "GetGameByKey",
                url: "game/{key}",
                defaults: new { controller = "Game", action = "GetGameByKey", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteGame",
                url: "games/remove",
                defaults: new { controller = "Game", action = "DeleteGame" }
            );

            routes.MapRoute(
                name: "AddCommentToGame",
                url: "game/{key}/newcomment",
                defaults: new { controller = "Game", action = "AddCommentToGame", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetAllCommentsByGame",
                url: "game/{key}/comments",
                defaults: new { controller = "Game", action = "GetAllCommentsByGame", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DownloadGame",
                url: "game/{key}/download",
                defaults: new { controller = "Game", action = "DownloadGame", key = UrlParameter.Optional }
            );

        }
    }
}
