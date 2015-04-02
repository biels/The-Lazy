using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; //Utilizant xarxa
using System.Net.FtpClient; //Utilitzant FTP
using System.Text;
using System.Threading.Tasks;

namespace TheLazyClientMVVM.FileExplorer
{
    public class FileExploreHandler
    {
        //Paràmetres de connexió
        public string host { get; set; } 
        public string username { get; set; } 
        public string password { get; set; } 
        public string relative_path { get; set; }

        //EVENTS
        public event ProgressUpdatedEventHandler ProgressUpdatedEvent;
        public delegate void ProgressUpdatedEventHandler(long progress, long total);
        //UI
        public List<RemoteFileInfo> Files { get; set; } //A mostar a la UI

        //FTP
        FtpClient conn = new FtpClient();
        void init()
        {
            conn.Host = host;
            conn.Credentials = new NetworkCredential(username, password);
            conn.SetWorkingDirectory(relative_path);
        }
        //List
        void updateFileList() //Actualitzar List<RemoteFileInfo> Files
        {
            
        }
        //Upload
        void uploadFile() //Aniria bé asinrònica - StreamReader
        {

        }
        void uploadProgressUpdated() //Callback de uploadFile()
        {
            ProgressUpdatedEvent(3, 10);
        }
        //Download
        void downloadFile() //Aniria bé asinrònica - StreamWriter
        {

        }
        void downloadProgressUpdated() //Callback de downloadFile()
        {

        }
        //Delete
        void deleteFile() //Simple ordre FTP amb la lliberia, veure exemples
        {

        }
    }
}
