using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComboBoxCommandBinding.FrameWork;
using System.Windows.Input;

namespace ComboBoxCommandBinding.ViewModels
{
    public class MainWindowViewModel : ModelBase
    {
        public ObservableCollection<string> FileNames { get; set; }

        private string _fileName;
        /// <summary>
        /// Filename selected to be as source
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if (value != null)
                {
                    _fileName = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            var coll = new ObservableCollection<string>();
            coll.Add("one");
            coll.Add("two");

            this.FileNames = coll;
        }

        public int setMessage()
        {
            int x=0;
            x++;
            return x;
        }



    }
}
