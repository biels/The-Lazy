using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using Meebey.SmartIrc4net;

namespace TheLazyClientMVVM.Chat
{
    public class IrcHandler
    {
        // Fem una instància de la API d'alt nivell
        public IrcClient irc = new IrcClient();
        public Thread thread;
        public void startThread()
        {
            thread = new Thread(new ThreadStart(threadMethod));
            thread.Start();
            //Listen();
        }
        public void threadMethod()
        {
            init();
            Connect();
            Login();
            //Join();
            Listen();
        }
        public void init()
        {
            Thread.CurrentThread.Name = "Xat";

            // UTF-8 test
            irc.Encoding = System.Text.Encoding.UTF8;

            // wait time between messages, we can set this lower on own irc servers
            irc.SendDelay = 200;

            // we use channel sync, means we can use irc.GetChannel() and so on
            irc.ActiveChannelSyncing = true;

            // here we connect the events of the API to our written methods
            // most have own event handler types, because they ship different data
            irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);
            irc.OnError += new ErrorEventHandler(OnError);
            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
            irc.OnConnected += new EventHandler(OnConnected);
            irc.OnJoin += new JoinEventHandler(OnJoin);
            irc.OnChannelAction += new ActionEventHandler(OnAction);
            //irc.OnChannelNotice += new ActionEventHandler(OnNotice);

        }
        public void Connect()
        {
            string[] serverlist;
            // the server we want to connect to, could be also a simple string
            serverlist = new string[] { "irc.freenode.org" };
            int port = 6667;            
            try
            {
                // here we try to connect to the server and exceptions get handled
                irc.Connect(serverlist, port);
            }
            catch (ConnectionException e)
            {
                // something went wrong, the reason will be shown
                System.Console.WriteLine("couldn't connect! Reason: " + e.Message);
                Exit();
            }

           
        }
        public void Login()
        {
            // here we logon and register our nickname and so on 
            irc.Login("LZ_" + Com.main.localUser.username, Com.main.localUser.real_name);
        }
        public void Join()
        {
            string channel = "#biel";
            try
            {
                if (!irc.IsConnected) { return; }
                // join the channel
                irc.RfcJoin(channel);

                irc.SendMessage(SendType.Message, channel, "ONLINE");
            }
            catch (ConnectionException)
            {
                // this exception is handled because Disconnect() can throw a not
                // connected exception
                Exit();
            }
            catch (Exception e)
            {
                // this should not happen by just in case we handle it nicely
                System.Console.WriteLine("Error occurred! Message: " + e.Message);
                System.Console.WriteLine("Exception: " + e.StackTrace);
                Exit();
            }
        }
        public void Listen()
        {
            try
            {
                irc.Listen();
                irc.Disconnect();
            }
            catch (ConnectionException)
            {
                // this exception is handled because Disconnect() can throw a not
                // connected exception
                Exit();
            }
            catch (Exception e)
            {
                // this should not happen by just in case we handle it nicely
                System.Console.WriteLine("Error occurred! Message: " + e.Message);
                System.Console.WriteLine("Exception: " + e.StackTrace);
                Exit();
            }
        }
        // this method handles when we receive "ERROR" from the IRC server
        public void OnError(object sender, ErrorEventArgs e)
        {
            System.Console.WriteLine("Error: " + e.ErrorMessage);
            Exit();
        }

        // this method will get all IRC messages
        public void OnRawMessage(object sender, IrcEventArgs e)
        {
            // System.Console.WriteLine("Received: " + e.Data.RawMessage);
        }
        public void OnChannelMessage(object sender, IrcEventArgs e)
        {
            foreach (ChatRoom r in Com.main.chatManager.rooms)
            {
                if (r.channelName() == e.Data.Channel)
                {
                    r.OnChannelMessage(sender, e);
                }
            }
        }
        public void OnConnected(object sender, EventArgs e)
        {
            System.Console.WriteLine("CONNECTAT!");
        }
        public void OnJoin(object sender, JoinEventArgs e)
        {
            foreach (ChatRoom r in Com.main.chatManager.rooms)
            {
                if (r.channelName() == e.Data.Channel)
                {
                    r.OnJoin(sender, e);
                }
            }
        }
        public void OnAction(object sender, ActionEventArgs e)
        {
            irc.SendMessage(SendType.Message, e.Data.Nick, "ACTION: " + e.ActionMessage);
        }
        public void OnNotice(object sender, ActionEventArgs e)
        {
            System.Console.WriteLine("Hem entrat a " + e.ActionMessage);
        }

        public void OnQueryMessage(object sender, IrcEventArgs e)
        {
            switch (e.Data.MessageArray[0])
            {
                // debug stuff
                case "dump_channel":
                    string requested_channel = e.Data.MessageArray[1];
                    // getting the channel (via channel sync feature)
                    Channel channel = irc.GetChannel(requested_channel);

                    // here we send messages
                    irc.SendMessage(SendType.Message, e.Data.Nick, "<channel '" + requested_channel + "'>");

                    irc.SendMessage(SendType.Message, e.Data.Nick, "Name: '" + channel.Name + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Topic: '" + channel.Topic + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Mode: '" + channel.Mode + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "Key: '" + channel.Key + "'");
                    irc.SendMessage(SendType.Message, e.Data.Nick, "UserLimit: '" + channel.UserLimit + "'");

                    // here we go through all users of the channel and show their
                    // hashtable key and nickname 
                    string nickname_list = "";
                    nickname_list += "Users: ";
                    foreach (DictionaryEntry de in channel.Users)
                    {
                        string key = (string)de.Key;
                        ChannelUser channeluser = (ChannelUser)de.Value;
                        nickname_list += "(";
                        if (channeluser.IsOp)
                        {
                            nickname_list += "@";
                        }
                        if (channeluser.IsVoice)
                        {
                            nickname_list += "+";
                        }
                        nickname_list += ")" + key + " => " + channeluser.Nick + ", ";
                    }
                    irc.SendMessage(SendType.Message, e.Data.Nick, nickname_list);

                    irc.SendMessage(SendType.Message, e.Data.Nick, "</channel>");
                    break;
                case "gc":
                    GC.Collect();
                    break;
                // typical commands
                case "join":
                    irc.RfcJoin(e.Data.MessageArray[1]);
                    break;
                case "part":
                    irc.RfcPart(e.Data.MessageArray[1]);
                    break;
                case "msg":
                    irc.SendMessage(SendType.Message, "#biel", "Hello madafaka!!");
                    break;
                case "die":
                    Exit();
                    break;
            }
        }
        public void ReadCommands()
        {
            // here we read the commands from the stdin and send it to the IRC API
            // WARNING, it uses WriteLine() means you need to enter RFC commands
            // like "JOIN #test" and then "PRIVMSG #test :hello to you"
            while (true)
            {
                string cmd = System.Console.ReadLine();
                if (cmd.StartsWith("/list"))
                {
                    int pos = cmd.IndexOf(" ");
                    string channel = null;
                    if (pos != -1)
                    {
                        channel = cmd.Substring(pos + 1);
                    }

                    IList<ChannelInfo> channelInfos = irc.GetChannelList(channel);
                    Console.WriteLine("channel count: {0}", channelInfos.Count);
                    foreach (ChannelInfo channelInfo in channelInfos)
                    {
                        Console.WriteLine("channel: {0} user count: {1} topic: {2}",
                                          channelInfo.Channel,
                                          channelInfo.UserCount,
                                          channelInfo.Topic);
                    }
                }
                else
                {
                    irc.WriteLine(cmd);
                }
            }
        }
        public void Exit()
        {
            // we are done, lets exit...
            System.Console.WriteLine("Exiting...");
            //System.Console.ReadLine();
            //System.Environment.Exit(0);
        }
    }
}
