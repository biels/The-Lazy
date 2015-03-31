using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Chat
{
    public class ChatManager
    {
        public List<ChatWindowHandler> windows = new List<ChatWindowHandler>();
        public List<ChatRoom> rooms = new List<ChatRoom>();
        public IrcHandler handler = new IrcHandler();
        public void init()
        {
            handler.startThread();
            rooms.Add(new ChatRoom() { name = "main" });
            rooms[0].prepareForChat();
            windows.Add(new ChatWindowHandler() { room = rooms[0] });
        }
    }
}
