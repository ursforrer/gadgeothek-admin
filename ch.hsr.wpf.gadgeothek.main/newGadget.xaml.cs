using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.main.vms;
using ch.hsr.wpf.gadgeothek.service;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public GadgetNewViewModel gadgetNewViewModel { get; set; }

        public newGadget()
        {
            InitializeComponent();
            gadgetNewViewModel = new GadgetNewViewModel();
            DataContext = gadgetNewViewModel;
        }

    }
}
