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
using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;

namespace ch.hsr.wpf.gadgeothek.main
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<Gadget> Gadgets { get; set; }
        public ObservableCollection<Loan> Loans { get; set; }
        private LibraryAdminService service;
        public MainWindow()
        {
            InitializeComponent();
            var serverUrl = "http://localhost:8080";
            service = new LibraryAdminService(serverUrl);

            Gadgets = new ObservableCollection<Gadget>(service.GetAllGadgets());
            Loans = new ObservableCollection<Loan>(service.GetAllLoans());


            DataContext = this;
        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            newGadget win2 = new newGadget();
            win2.ShowDialog();
        }
    }
}
