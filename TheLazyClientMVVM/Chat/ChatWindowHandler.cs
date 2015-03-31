using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Chat
{
    public class ChatWindowHandler
        {
        


        public ChatRoom room;
        public void bindRoom()
        {
            room = Com.main.chatManager.rooms[0];
        }
        public void sendMessage()
        {

        }
    }
}
