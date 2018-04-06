using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Data;

namespace ConsoleApplication12
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> obj, T value)
        {
            return obj.IndexOf(value, null);
        }

        public static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            var found = obj
                .Select((a, i) => new { a, i })
                .FirstOrDefault(x => comparer.Equals(x.a, value));
            return found == null ? -1 : found.i;
        }
    }

    class Program
    {
        private static Stopwatch sw;

        static void Main(string[] args)
        {
            sw = new Stopwatch();

            string path = @"C:\TEST\Images";

            Console.WriteLine($"PATH : {path}");

            int countBMP = getCountForFileType(path, "bmp");
            //int countSVG = getCountForFileType(path, "svg");

            Console.Read();
        }

        private static int getCountForFileType(string path, string type)
        {
            bool showTicks = true;
            Console.WriteLine($"===== for type {type} ==== ");

            // get files
            sw.Restart();
            var allFilesWithExtension = System.IO.Directory.EnumerateFiles(path).AsParallel().Where<string>(f => f.EndsWith(type));
            sw.Stop();

            //Console.WriteLine("-- get files ");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time to get enumeration {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time to get enumeration {sw.ElapsedMilliseconds} ms \n");

            // get count
            sw.Restart();
            int numberOfFiles = allFilesWithExtension.Count();

            sw.Stop();
            Console.WriteLine("-- get count ");
            Console.WriteLine($"number of files: {numberOfFiles}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting count {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting count {sw.ElapsedMilliseconds} ms \n");

            // get count as parallel
            sw.Restart();
            int numberOfFilesParallel = allFilesWithExtension.AsParallel().Count();

            sw.Stop();
            Console.WriteLine("-- get count as parallel");
            Console.WriteLine($"number of files: {numberOfFilesParallel}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting count parallel {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting count as parallel {sw.ElapsedMilliseconds} ms \n");

            // get array
            sw.Restart();
            var array = allFilesWithExtension.ToArray();
            sw.Stop();
            Console.WriteLine("-- get array");
            Console.WriteLine($"array count: {array.Count()}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting array {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting array {sw.ElapsedMilliseconds} ms \n");

            // get element from array
            sw.Restart();
            string fileAt = array[numberOfFiles / 2];
            sw.Stop();
            Console.WriteLine($"-- get filename at using Array {numberOfFiles / 2}");
            Console.WriteLine($"file at nth place: {fileAt}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedMilliseconds} ms \n");

            // get element from enumerator 
            sw.Restart();
            string fileAtenumerator = allFilesWithExtension.ElementAt(numberOfFiles / 2);
            sw.Stop();
            Console.WriteLine($"-- get filename at using enumerator {numberOfFiles / 2}");
            Console.WriteLine($"file at nth place: {fileAtenumerator}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedMilliseconds} ms \n");

            // get last
            sw.Restart();
            var last = allFilesWithExtension.Last();

            sw.Stop();

            Console.WriteLine("-- get last ");
            Console.WriteLine($"\n last: {last}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting last {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting last {sw.ElapsedMilliseconds} ms \n");

            //get indexOf
            sw.Restart();
            var indexOfValue = allFilesWithExtension.IndexOf(last);

            sw.Stop();

            Console.WriteLine("-- get indexof ");
            Console.WriteLine($"\n last: {indexOfValue}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting indexOf {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting indexOf {sw.ElapsedMilliseconds} ms \n");


            // get first
            sw.Restart();
            var first = allFilesWithExtension.First();

            sw.Stop();
            Console.WriteLine("-- get first ");
            Console.WriteLine($"\n first: {first}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting first {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting first {sw.ElapsedMilliseconds} ms \n");

            // get next
            sw.Restart();
            int Counter = 0;
            string CurrentLoadedImage = last;
            foreach (String file in allFilesWithExtension)
            {
                if (CurrentLoadedImage == file)
                {
                    break;
                }
                else
                {
                    Counter++;
                }
            }

            string currentFilePath = "";
            try
            {
                //currentFilePath = allFilesWithExtension.Skip(Counter ).First();
                currentFilePath = allFilesWithExtension.ElementAt(Counter);
                //currentFilePath = array[Counter];
                CurrentLoadedImage = currentFilePath;
                //LoadNewFile(filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            sw.Stop();
            Console.WriteLine("-- get next ");
            Console.WriteLine($"\n next counter : {Counter}");
            Console.WriteLine($"\n current filepath : {currentFilePath}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting next {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting next  {sw.ElapsedMilliseconds} ms \n");


            Console.WriteLine($"================== \n");

            return numberOfFiles;
        }
    }
}
