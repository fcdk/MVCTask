//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCTask1EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class GenreInGame
    {
        public string GenreInGameKey { get; set; }
        public string GenreKey { get; set; }
        public string GameKey { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
