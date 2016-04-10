using System;
using AutoMapper;
using MVCTask.Models.Game;
using MVCTask.Models.Order;
using MVCTask.Models.Publisher;
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
                        GameKey = Guid.NewGuid().ToString(),
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        UnitsInStock = x.UnitsInStock,
                        Discontinued = x.Discontinued
                    });
                cfg.CreateMap<Game, CreateGameViewModel>();

                cfg.CreateMap<Game, GameDetailsViewModel>()
                    .ForMember(
                        x => x.Key,
                        x => x.ResolveUsing(y => y.GameKey)
                    )
                    .ForMember(
                        x => x.PublisherName,
                        x => x.ResolveUsing(y => y.Publisher?.CompanyName)
                    );

                cfg.CreateMap<CommentsViewModel, Comment>()
                    .ConstructUsing(x => new Comment
                    {
                        CommentKey = Guid.NewGuid().ToString(),
                        ParentCommentKey = x.ParentCommentKey,
                        Name = x.Name,
                        Body = x.Body,
                        GameKey = x.GameKey });
                cfg.CreateMap<Game, CreateGameViewModel>();

                cfg.CreateMap<Publisher, PublisherViewModel>();
                cfg.CreateMap<PublisherViewModel, Publisher>()
                    .ConstructUsing(x => new Publisher
                    {
                        PublisherKey = Guid.NewGuid().ToString(),
                        CompanyName = x.CompanyName,
                        Description = x.Description,
                        HomePage = x.HomePage
                    });

                cfg.CreateMap<OrderDetailsViewModel, OrderDetail>()
                    .ConstructUsing(x => new OrderDetail
                    {
                        OrderDetailsKey = Guid.NewGuid().ToString(),
                        GameKey = x.GameKey,                        
                        Quantity = x.Quantity,
                        Discount = x.Discount
                    });

            });

            return config.CreateMapper();
        }
    }
}
