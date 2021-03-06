﻿using System;
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
        //public string relative_path { get; set; }

        //Element-specific
        public int element_id { get; set; }

        //EVENTS
        public event ProgressUpdatedEventHandler ProgressUpdatedEvent;
        public delegate void ProgressUpdatedEventHandler(long progress, long total);
        public event EventHandler UploadCompletedEvent;
        public event Action DownloadCompletedEvent;
        public event EventHandler FileListRecieved;
        //UI
        public List<RemoteFileInfo> files { get; set; } //A mostar a la UI

        //FTP
        FtpClient ftp = new FtpClient();
        public FileExploreHandler()
        {
            files = new List<RemoteFileInfo>();
        }
        public async void init()
        {
            fillConnectionInfo();
            //Essential
            ftp.Host = host;
            ftp.Credentials = new NetworkCredential(username, password);

            //After set-up
            await Task.Run(() => createMainDirectoryIfNeededAsync());
            //ftp.SetWorkingDirectory(getRelativePath());//lloc relatiu
        }
        public void fillConnectionInfo() //Unsafe
        {
            SelectorConnexions.ConnectionInfo cinfo = new SelectorConnexions.ConnectionInfo();
            host = cinfo.Address;
            username = "thelazy";
            password = "1234";
        }
        //Deterministic functions
        private string getElementDirectory()
        {
            return "elements";
        }
        private string getRelativePath()
        {
            return string.Format("{0}/{1}", getElementDirectory(), element_id);
        }
        //List
        public async void requestFileListAsync()
        {
            await Task.Run(() => requestFileList());
            FileListRecieved(this, new EventArgs());
        }
        private void requestFileList() //Actualitzar List<RemoteFileInfo> Files
        {
            ftp.Connect();
            files.Clear();
            foreach (FtpListItem item in ftp.GetListing(getRelativePath(),
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
            ftp.Disconnect();
        }
        //Upload
        public async void UploadFileAsync(string file_name, string local_path) //Aniria bé asinrònica - StreamReader
        {
            await Task.Run(() => ftp.Connect());
            if (ftp == null) { return; }
            string relativeFile_path;
            relativeFile_path = getRelativePath() + "/" + file_name;

            Stream destinationStream = ftp.OpenWrite(relativeFile_path);
            Stream sourceStream = File.OpenRead(local_path);

            //uploadProgressUpdated(sourceStream.Length, sourceStream);
            await sourceStream.CopyToAsync(destinationStream);
            UploadCompletedEvent(sourceStream, new EventArgs());

            destinationStream.Close();
            sourceStream.Close();
            await Task.Run(() => ftp.Disconnect());
            //Finished event
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
        public async void downloadFileAsync(string file_name, string local_path) //Aniria bé asinrònica - StreamWriter
        {
            await Task.Run(() => ftp.Connect());
            if (ftp == null){ return; }
            string relativeFile_path;
            relativeFile_path = getRelativePath() + "/" + file_name;

            Stream sourceStream = ftp.OpenRead(relativeFile_path);
            Stream destinationStream = File.Create(local_path);

            await sourceStream.CopyToAsync(destinationStream);
            if (DownloadCompletedEvent != null) DownloadCompletedEvent();
            sourceStream.Close();
            destinationStream.Close();
            await Task.Run(() => ftp.Disconnect());
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
        public void deleteFile(string file_name) //Simple ordre FTP amb la lliberia, veure exemples
        {
            ftp.Connect();
            if (ftp == null) { return; }
            //string relativeFile_path;
            //relativeFile_path = relative_path + "/" + file_name;

            ftp.DeleteFile(file_name);
            ftp.Disconnect();
        }
       //Delete
       public async void createDirectoryAsync(string dir_name) //Simple ordre FTP amb la lliberia, veure exemples
       {           
           if (ftp == null) { return; }
           await Task.Run(() => ftp.Connect());
           await Task.Run(() => ftp.CreateDirectory(dir_name));
           ftp.Disconnect();
       }
       public async void createMainDirectoryIfNeededAsync() //Simple ordre FTP amb la lliberia, veure exemples
       {
           if (ftp == null) { return; }
           await Task.Run(() => ftp.Connect());
           //ftp.SetWorkingDirectory("");
           //ftp.SetWorkingDirectory(getElementDirectory());
           if (!ftp.DirectoryExists(getRelativePath()))
           {
               await Task.Run(() => ftp.CreateDirectory(getRelativePath()));
           }          
           ftp.Disconnect();
       }
    }
}
