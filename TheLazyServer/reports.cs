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
    
    public partial class reports
    {
        public int report_id { get; set; }
        public int reporter_id { get; set; }
        public int reported_id { get; set; }
        public Nullable<int> related_element_id { get; set; }
        public Nullable<int> related_post_id { get; set; }
        public string reason { get; set; }
        public Nullable<int> level { get; set; }
    
        public virtual elements elements { get; set; }
        public virtual posts posts { get; set; }
        public virtual users users { get; set; }
        public virtual users users1 { get; set; }
    }
}
