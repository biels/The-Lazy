using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLazyServer
{
    public class Log
    {
        public static void Ll(string text, LogLevel level)
        {
            //[16:33:46 INFO]: Done (3,709s)!
            Console.WriteLine(String.Format("[{0} {1}]: {2}", DateTime.Now.ToShortTimeString(), Enum.GetName(typeof(LogLevel), level).ToUpper(), text));//DateTime.Now.ToShortTimeString(), Enum.GetName(typeof(LogLevel), level).ToUpper()), text);
            
        }
        public static void I(string text)
        {
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Ll(text, LogLevel.Info);
           // Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void E(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Ll(text, LogLevel.Error);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void F(string text)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Ll(text, LogLevel.Fatal);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void D(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Ll(text, LogLevel.Debug);
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void W(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Ll(text, LogLevel.Warning);
            Console.BackgroundColor = ConsoleColor.Black;
        }
       
        public enum LogLevel {Info, Warning, Error, Fatal, Debug }
    }
}
