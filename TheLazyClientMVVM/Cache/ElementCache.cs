using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLazyClientMVVM.Entities;

namespace TheLazyClientMVVM.Cache
{
    public class ElementCache
    {
        private List<ElementEntity> elements { get; set; }
        public ElementCache()
        {
            elements = new List<ElementEntity>();
        }
        public ElementEntity getElement(int id, bool direct = false)
        {
            Predicate<ElementEntity> match = new Predicate<ElementEntity>(e => id == e.id);
            ElementEntity cached = elements.FirstOrDefault(e => match(e) && e.isValid());
            if (cached != null && !direct) 
            {
                Com.main.cached_query_count++;
                return cached;
            }
            else
            {
                ElementEntity tocache = DbClient.DbElementClient.getElementInfo(id);
                elements.RemoveAll(match);
                elements.Add(tocache);
                return tocache;
            }
        }
    }
}
