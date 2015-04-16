using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.FileExplorer
{
    public class FileExploreHandlerManager
    {
        private List<FileExploreHandler> handlers;
        public FileExploreHandlerManager()
        {
            handlers = new List<FileExploreHandler>();
        }
        public FileExploreHandler getHandler(int element_id)
        {
            Predicate<FileExploreHandler> match = new Predicate<FileExploreHandler>(e => element_id == e.element_id);
            FileExploreHandler existing = handlers.FirstOrDefault(e => match(e));
            if (existing != null)
            {
                return existing;
            }
            else
            {
                FileExploreHandler newHandler = new FileExploreHandler();
                newHandler.init();
                handlers.Add(newHandler);
                return newHandler;
            }
        }
        public void removeHandler(int element_id)
        {
            Predicate<FileExploreHandler> match = new Predicate<FileExploreHandler>(e => element_id == e.element_id);
            FileExploreHandler existing = handlers.FirstOrDefault(e => match(e));
            if (existing != null)
            {
                //TODO: Close here if needed
                handlers.Remove(existing);
            }
        }
    }
}
