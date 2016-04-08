using System.Web.Mvc;
using System.Web.Routing;

namespace MVCTask
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AllGames",
                url: "games",
                defaults: new { controller = "Game", action = "All" }
            );

            routes.MapRoute(
                name: "CreateGame",
                url: "games/new",
                defaults: new { controller = "Game", action = "Create" }
            );

            routes.MapRoute(
                name: "UpdateGame",
                url: "games/update",
                defaults: new { controller = "Game", action = "Update" }
            );

            routes.MapRoute(
                name: "GameDetails",
                url: "game/{key}",
                defaults: new { controller = "Game", action = "Details", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteGame",
                url: "games/remove",
                defaults: new { controller = "Game", action = "Delete" }
            );

            routes.MapRoute(
                name: "AddCommentToGame",
                url: "game/{key}/newcomment",
                defaults: new { controller = "Game", action = "AddComment", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetAllCommentsByGame",
                url: "game/{key}/comments",
                defaults: new { controller = "Game", action = "Comments", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DownloadGame",
                url: "game/{key}/download",
                defaults: new { controller = "Game", action = "Download", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NumberOfGames",
                url: "game/total",
                defaults: new { controller = "Game", action = "Total" }
            );

        }
    }
}
