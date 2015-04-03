using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Entities
{
    public class Entity
    {
        
        
        //INFO Memòria cau
        protected DateTime timestamp { get; set; }
        public Entity()
        {
            timestamp = DateTime.Now;
        }
        protected TimeSpan getMaximumAge() //Temps de validesa a la memòria cau
        {
            return new TimeSpan(0, 10, 0); //[10 minuts]
        }
        public bool isValid()
        {
            if (!Com.main.config.caching_enabled) return false;
            return (DateTime.Now - timestamp) <= getMaximumAge();
        }
    }
}
