using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; //Utilizant xarxa
using System.Net.FtpClient; //Utilitzant FTP
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.WebSockets;

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
        public event EventHandler UploadCompleted;
        //UI
        public List<RemoteFileInfo> files { get; set; } //A mostar a la UI

        //FTP
        FtpClient conn = new FtpClient();
        void init()
        {
            conn.Host = host;
            conn.Credentials = new NetworkCredential(username, password);
            conn.SetWorkingDirectory(relative_path);//lloc relatiu
        }
        //List
        void updateFileList() //Actualitzar List<RemoteFileInfo> Files
        {
            conn.Connect();
            foreach (FtpListItem item in conn.GetListing(conn.GetWorkingDirectory(),
                FtpListOption.Modify | FtpListOption.Size))
            {
                if (FtpFileSystemObjectType.File == item.Type)
                {
                    RemoteFileInfo ent = new RemoteFileInfo();
                    ent.filename = item.Name;
                    ent.size = item.Size;
                    ent.modified_time = item.Modified;
                    files.Add(ent);
                }
            }
            conn.Disconnect();
        }
        //Upload

        public async void uploadFile(string file_name, string start_path) //Aniria bé asinrònica - StreamReader
        {
            conn.Connect();
            if (conn == null) { return; }
            string relativeFile_path;
            relativeFile_path = relative_path + "/" + file_name;

            Stream destinationStream = conn.OpenWrite(relativeFile_path);
            Stream sourceStream = File.OpenRead(start_path);

            //uploadProgressUpdated(sourceStream.Length, sourceStream);
            await sourceStream.CopyToAsync(destinationStream);
            UploadCompleted(sourceStream, new EventArgs());

            destinationStream.Close();
            sourceStream.Close();
            conn.Disconnect();

        }

        //void uploadFile(string file_name, string start_path)
        //{
        //    conn.Connect();
        //    if (conn == null) { return; }
        //    string relativeFile_path;
        //    relativeFile_path = relative_path + "/" + file_name;

        //    byte[] buf = new byte[8192];
        //    int read = 0;
        //    Stream destinationStream = conn.OpenWrite(relativeFile_path);
        //    Stream sourceStream = File.OpenRead(start_path);
        //    long size = sourceStream.Length / 1024;

        //    while ((read = sourceStream.Read(buf, 0 , buf.Length)) > 0){
        //        destinationStream.Write(buf , 0 , read);
        //        uploadProgressUpdated(size, destinationStream.Length);
        //    }
        //    destinationStream.Close();
        //    sourceStream.Close();
        //    conn.Disconnect();
        //}

        void uploadProgressUpdated(long total_size, long procces) //Callback de uploadFile()
        {
            long procces_kb;
            procces_kb = procces / 1024;
            ProgressUpdatedEvent(procces_kb, total_size);
        }

        //download
        public async void downloadFile(string file_name, string local_path) //Aniria bé asinrònica - StreamWriter
        {
            conn.Connect();
            if (conn == null){ return; }
            string relativeFile_path;
            relativeFile_path = relative_path + "/" + file_name;

            Stream sourceStream = conn.OpenRead(relativeFile_path);
            Stream destinationStream = File.Create(local_path);

            await sourceStream.CopyToAsync(destinationStream);
            UploadCompleted(sourceStream, new EventArgs());

            sourceStream.Close();
            destinationStream.Close();
            conn.Disconnect();
        }

        //void downloadFile(string file_name, string end_path)
        //{
        //    conn.Connect();
        //    if (conn == null){ return; }
        //    string relativeFile_path;
        //    relativeFile_path = relative_path + "/" + file_name;
            
        //    byte[] buf = new byte[8192];
        //    int read = 0;
        //    Stream sourceStream = conn.OpenRead(relativeFile_path);
        //    Stream destinationStream = File.Create(end_path);
        //    long size = sourceStream.Length / 1024;

        //    while ((read = sourceStream.Read(buf, 0, buf.Length)) > 0)
        //    {
        //        destinationStream.Write(buf, 0, read);
        //        downloadProgressUpdated(size, destinationStream.Length);
        //    }
        //    destinationStream.Close();
        //    sourceStream.Close();
        //    conn.Disconnect();
        //}
        void downloadProgressUpdated(long total_size, long procces) //Callback de downloadFile()
        {
            long procces_kb;
            procces_kb = procces / 1024;
            ProgressUpdatedEvent(procces_kb, total_size);
        }
        //Delete
        void deleteFile(string file_name) //Simple ordre FTP amb la lliberia, veure exemples
        {
            conn.Connect();
            if(conn == null) { return; }
            string relativeFile_path;
            relativeFile_path = relative_path + "/" + file_name;

            conn.DeleteFile(relativeFile_path);
            conn.Disconnect();
        }
    }
}
