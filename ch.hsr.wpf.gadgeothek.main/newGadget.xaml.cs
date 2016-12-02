using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace ch.hsr.wpf.gadgeothek.main
{
    /// <summary>
    /// Interaktionslogik für newGadget.xaml
    /// </summary>
    public partial class newGadget : MetroWindow
    {
        public newGadget()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing, when cancled is pressed
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
