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
                cfg.CreateMap<GameViewModel, Game>()
                    .ConstructUsing(x => new Game { GameKey = Guid.NewGuid().ToString(), Name = x.Name, Description = x.Description });
            });

            return config.CreateMapper();
        }
    }
}
