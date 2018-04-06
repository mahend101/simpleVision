using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWatcherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MAIN");

            var filesWatcher = new FilesWatcher(@"C:\TEST\Images", isImageType);
            // Run an infinite loop.
            while (true)
            {
                Console.WriteLine("TYPE SOMETHING");
                string line = Console.ReadLine();
                Console.WriteLine("TYPED: " + line);



            }
        }

        private static bool isImageType(string fileName)
        {
            if(fileName.EndsWith("bmp"))
            {
                return true;
            }else
            {
                return false;
            }
        }

    }
}
