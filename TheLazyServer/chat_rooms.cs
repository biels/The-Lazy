//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheLazyServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class chat_rooms
    {
        public chat_rooms()
        {
            this.chat_messages = new HashSet<chat_messages>();
        }
    
        public int room_id { get; set; }
        public int creator_user_id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<chat_messages> chat_messages { get; set; }
        public virtual users users { get; set; }
    }
}
