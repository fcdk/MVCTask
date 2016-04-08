using System;
using AutoMapper;
using MVCTask.Models.Game;
using MVCTaskEF;

namespace MVCTask.App_Start
{
    class GameStoreMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateGameViewModel, Game>()
                    .ConstructUsing(x => new Game
                    {
                        GameKey = Guid.NewGuid().ToString(), Name = x.Name, Description = x.Description, Price = x.Price, UnitsInStock = x.UnitsInStock,
                        Discontinued = x.Discontinued
                    });
                cfg.CreateMap<Game, CreateGameViewModel>();
                cfg.CreateMap<Game, GameDetailsViewModel>()
                    .ForMember(
                        x => x.Key,
                        x => x.ResolveUsing(y => y.GameKey)
                    );
                cfg.CreateMap<CommentsViewModel, Comment>()
                    .ConstructUsing(x => new Comment { CommentKey = Guid.NewGuid().ToString(), ParentCommentKey = x.ParentCommentKey, Name = x.Name,
                        Body = x.Body, GameKey = x.GameKey });
            });

            return config.CreateMapper();
        }
    }
}
