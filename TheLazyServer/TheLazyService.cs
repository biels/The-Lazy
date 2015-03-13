using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TheLazyInterfaces;
using TheLazyInterfaces.Containers;

namespace TheLazyServer
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "TheLazyService" en el código y en el archivo de configuración a la vez.
    public class TheLazyService : ITheLazyService
    {
        public void DoWork()
        {
            Console.WriteLine("Work done!");
        }
        public List<PostInfo> getPosts()
        {
            List<PostInfo> result = new List<PostInfo>();
            PostInfo p = new PostInfo();
            p.text = "Server_text";
            p.subject = "Random_things";
            result.Add(p);
            return result;
        }
        public int openSession(int user_id)
        {
            //try
            //{
            //    using (the_lazyEntities1 db = new the_lazyEntities1())
            //    {
                    
            //    }
            //}
            return 0;
        }
        public int login(string username, string password)
        {
            try
            {
                
            }
            catch
            {
                
                throw;
            }
            return -5;
        }
    }
}
