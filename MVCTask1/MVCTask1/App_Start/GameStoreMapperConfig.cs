using System;
using AutoMapper;
using MVCTask1.Models.Game;
using MVCTask1EF;

namespace MVCTask1.App_Start
{
    class GameStoreMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GameViewModel, Game>()
                    .ConstructUsing(x => new Game { GameKey = Guid.NewGuid().ToString(), Name = x.Name, Description = x.Description });
            });

            return config.CreateMapper();
        }
    }
}
