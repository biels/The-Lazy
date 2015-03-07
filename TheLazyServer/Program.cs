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
            Console.WriteLine("Inicialitzant el servidor...");
            using (ServiceHost host = new ServiceHost(typeof(TheLazyService)))
            {
                host.Open();
                Console.WriteLine("Servidor obert");
            }
        }
    }
}
