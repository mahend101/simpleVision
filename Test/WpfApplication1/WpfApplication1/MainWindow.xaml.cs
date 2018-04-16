using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool dragStarted = false;

        private void Slider_DragStarted(object sender, RoutedEventArgs e)
        {
            this.dragStarted = true;
        }

        private void Slider_DragCompleted(object sender, RoutedEventArgs e)
        {
            this.dragStarted = false;
            myFunction();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!dragStarted)
                myFunction();
        }

        private void myFunction()
        {
            val.Text = slider.Value.ToString();
        }
    }
}
