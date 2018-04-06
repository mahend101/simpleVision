using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemWatcherTest
{
    class FilesWatcher
    {
        /// <summary>
        /// Watcher.
        /// </summary>
        private FileSystemWatcher _watcher;

        private readonly OrderedDictionary _orderedDict = new OrderedDictionary();

        public event dictionaryChanged;

        private string _path;

        public OrderedDictionary Dict
        {
            get
            {
                return _orderedDict;
            }
        }

        public FilesWatcher(string path, Func<string, bool> predicateForEnumeration)
        {
            _path = path;

            _watcher = new FileSystemWatcher(path);

            // get the list of files now
            IEnumerable<string> files = System.IO.Directory.EnumerateFiles(path).Where(predicateForEnumeration);

            int i = 0;
            foreach (string f in files)
            {
                _orderedDict.Add(i, f);
                i++;
            }

            _watcher.Created += _watcher_Created;
            _watcher.Deleted += _watcher_Deleted;
            _watcher.Renamed += _watcher_Renamed;
        }

        private void _watcher_Renamed(object sender, RenamedEventArgs e)
        {
            _orderedDict[e.OldFullPath] = e.FullPath;
        }

        private void _watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _orderedDict.Remove(e.FullPath);
        }

        private void _watcher_Created(object sender, FileSystemEventArgs e)
        {
            _orderedDict.Add(_orderedDict.Count, e.FullPath);
        }
    }
}
