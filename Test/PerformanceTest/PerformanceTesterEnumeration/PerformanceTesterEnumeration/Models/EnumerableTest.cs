using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTester
{
    class EnumerableTest : ModelBase
    {
        #region fields 
        private DataTable _table;
        private readonly Stopwatch _sw = new Stopwatch();
        private string _filesPath;

        #endregion  //fields 

        #region properties

        public string FilesPath
        {
            get
            {
                return _filesPath;
            }
            set
            {
                _filesPath = value;
                OnPropertyChanged();
            }
        }

        public DataTable Table
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value;
                OnPropertyChanged();
            }
        }

        #endregion //properties

        #region constructor
        public EnumerableTest()
        {

        }
        #endregion // constructor


        private int getCountForFileType(string path, string type)
        {
            bool showTicks = true;
            Console.WriteLine($"===== for type {type} ==== ");

            // get files
            _sw.Restart();
            var allFilesWithExtension = System.IO.Directory.EnumerateFiles(path).AsParallel().Where<string>(f => f.EndsWith(type));
            _sw.Stop();

            //Console.WriteLine("-- get files ");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time to get enumeration {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time to get enumeration {sw.ElapsedMilliseconds} ms \n");

            // get count
            _sw.Restart();
            int numberOfFiles = allFilesWithExtension.Count();

            _sw.Stop();
            Console.WriteLine("-- get count ");
            Console.WriteLine($"number of files: {numberOfFiles}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting count {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting count {sw.ElapsedMilliseconds} ms \n");

            // get count as parallel
            _sw.Restart();
            int numberOfFilesParallel = allFilesWithExtension.AsParallel().Count();

            _sw.Stop();
            Console.WriteLine("-- get count as parallel");
            Console.WriteLine($"number of files: {numberOfFilesParallel}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting count parallel {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting count as parallel {sw.ElapsedMilliseconds} ms \n");

            // get array
            _sw.Restart();
            var array = allFilesWithExtension.ToArray();
            _sw.Stop();
            Console.WriteLine("-- get array");
            Console.WriteLine($"array count: {array.Count()}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting array {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting array {sw.ElapsedMilliseconds} ms \n");

            // get element from array
            _sw.Restart();
            string fileAt = array[numberOfFiles / 2];
            _sw.Stop();
            Console.WriteLine($"-- get filename at using Array {numberOfFiles / 2}");
            Console.WriteLine($"file at nth place: {fileAt}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedMilliseconds} ms \n");

            // get element from enumerator 
            _sw.Restart();
            string fileAtenumerator = allFilesWithExtension.ElementAt(numberOfFiles / 2);
            _sw.Stop();
            Console.WriteLine($"-- get filename at using enumerator {numberOfFiles / 2}");
            Console.WriteLine($"file at nth place: {fileAtenumerator}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting file at nth place {sw.ElapsedMilliseconds} ms \n");

            // get last
            _sw.Restart();
            var last = allFilesWithExtension.Last();

            _sw.Stop();

            Console.WriteLine("-- get last ");
            Console.WriteLine($"\n last: {last}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting last {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting last {sw.ElapsedMilliseconds} ms \n");

            //get indexOf
            _sw.Restart();
            var indexOfValue = allFilesWithExtension.IndexOf(last);

            _sw.Stop();

            Console.WriteLine("-- get indexof ");
            Console.WriteLine($"\n last: {indexOfValue}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting indexOf {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting indexOf {sw.ElapsedMilliseconds} ms \n");


            // get first
            _sw.Restart();
            var first = allFilesWithExtension.First();

            _sw.Stop();
            Console.WriteLine("-- get first ");
            Console.WriteLine($"\n first: {first}");
            if (showTicks)
            {
                Console.WriteLine($"elapsed time getting first {sw.ElapsedTicks} ticks ");
            }
            Console.WriteLine($"elapsed time getting first {sw.ElapsedMilliseconds} ms \n");

            // get next
            _sw.Restart();
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

            _sw.Stop();
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
