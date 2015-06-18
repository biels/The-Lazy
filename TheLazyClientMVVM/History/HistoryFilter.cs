using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.History
{
    class HistoryFilter //Look element filter
    {
        //Filter criteria        
        public string search_text { get; set; }
        public string username { get; set; } //Ignorat si == null


        //Results
        //<lists> <--
        bool busy = false;


        //Criteria enums
    }
}
