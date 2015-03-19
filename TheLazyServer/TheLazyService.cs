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
        public int loginUser(string username, string password)
        {
            Log.I(String.Format("Intent d'inici de sessió {U: {0}, C: {1}}", username, password));
            int session_id = DbClients.DbLogin.loginUser(username, password);
            if (session_id > 0)
            {
                Log.I(String.Format("Sessió iniciada per {0} correctament!", username));
            }
            else
            {
                Log.W(String.Format("Ha fallat l'inici de sessió {U: {0}, C: {1}}!", username, password));
            }
            return session_id;
        }


        public bool register(string username, string password, string email)
        {
            Log.I(String.Format("Intent de registre {U: {0}, C: {1}, E: {2}}", username, password, email));
            bool registered = DbClients.DbLogin.register(username, password, email);
            if (registered){
                Log.I(String.Format("Usuari {0} registrat correctament!", username));
            }
            else
            {
                Log.I(String.Format("Ha fallat el registre {U: {0}, C: {1}, E: {2}}", username, password, email));
            }
            return registered;
        }

        public void keepAlive(int sessionId)
        {
            DbClients.DbLogin.keepAlive(sessionId);
            Log.I("keepAlive per a la sessió " + sessionId);
        }
    }
}
