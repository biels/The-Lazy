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
    
    public partial class notifications
    {
        public int notification_id { get; set; }
        public int user_id { get; set; }
        public string type { get; set; }
        public bool seen { get; set; }
    
        public virtual users users { get; set; }
    }
}
