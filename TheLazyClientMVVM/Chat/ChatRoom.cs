using Meebey.SmartIrc4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.Chat
{
    public class ChatRoom
    {
        public event EventHandler Joined;
        public event EventHandler MessageRecieved;
        public event EventHandler UserListUpdated;

        public List<string> messages = new List<string>();
        public List<string> users = new List<string>();
        public string name { get; set; }
        public void prepareForChat()
        {
            joinChannel();
        }

        public string channelName()
        {
            return String.Format("#LZY_{0}", name);
        }
        public List<string> getUserList()
        {
            List<string> keys = getChannel().Users.Keys.Cast<string>().ToList();
            return keys;
        }
        public void joinChannel(){
            getIRCClient().RfcJoin(channelName());
        }
        private Channel getChannel()
        {
            return getIRCClient().GetChannel(channelName());
        }
        public void sendMessage(string msg)
        {
            messages.Add("Jo > " + msg);
            getIRCClient().SendMessage(SendType.Message, channelName(), msg);
            MessageRecieved(null, null);
        }
        public void OnChannelMessage(object sender, IrcEventArgs e)
        {
            messages.Add(e.Data.Nick + " > " + e.Data.Message);
            MessageRecieved(sender, e);
        }
        public void OnJoin(object sender, JoinEventArgs e)
        {
            string u = getIRCClient().Nickname;
            if (e.Who == u)
            {
                messages.Add("Connectat al xat!");
                Joined(sender, e);

            sendMessage("CONNECTAT -- hola a tothom!");
            }
           
        }
        public void OnAction(object sender, ActionEventArgs e)
        {
            System.Console.WriteLine("Hem entrat a " + e.ActionMessage);
        }
        public IrcClient getIRCClient()
        {
            return Com.main.chatManager.handler.irc;
        }
    }
}
