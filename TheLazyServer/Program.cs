using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.White;
            //testConsole();
            
            if (DbClient.TestConnection())
            {
                Log.I("Connexió amb la base de dades realitzada correctament.");
            }
            else
            {
                Log.E("Error al connectar amb la base de dades");
                Console.ReadLine();
                close();
                return;
            }
            Log.I("Inicialitzant el servidor WCF...");
            using (ServiceHost host = new ServiceHost(typeof(TheLazyService)))
            {
                host.Open();
                Log.I("Servidor obert, [enter] per tnacar");
                Console.ReadLine();
            }
        }
        static void close()
        {
            Log.I("Aturant el servidor...");
        }
        static void testConsole()
        {
            Log.I("Test info.");
            Log.W("Test warning.");      
            Log.E("Test error.");
            Log.F("Test fatal <click to continue>.");
            Log.D("Test debug.");
        }
    }
}
