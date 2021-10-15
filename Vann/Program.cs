using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Vann
{
    class Program
    {

       

        static void Main(string[] args)
        {
            InitConsole("Vann",ConsoleColor.White);

            Console.Write("File destination : ");
            string file = Console.ReadLine();


            if(File.Exists(file))
            {
                var line = "";
                StreamReader reader =
                    new StreamReader(file);
                while ((line = reader.ReadLine()) != null)
                {
                   
                    Engine.TokenizeString(line);

                    Engine.RunLastLineTokens();
                }

                reader.Close();
                Console.ReadLine();
            }
            else
            {
                Engine.SetError($"Is this file exitst '{file}'");
                Engine.DisplayCurrentError();
                

            }


            Console.ReadLine();


        }




        static void InitConsole(string title,ConsoleColor fgc)
        {
            Console.ForegroundColor = fgc;

            Console.Title = title;

            Console.WindowHeight = 30;
            Console.WindowWidth = 120;
        }
    }
}
