using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.main.vms;
using MahApps.Metro.Controls;
using System.Windows;

namespace ch.hsr.wpf.gadgeothek.main
{
    /// <summary>
    /// Interaktionslogik für deleteWindow.xaml
    /// </summary>
    public partial class deleteWindow : MetroWindow
    {
        public GadgetDeleteViewModel gadgetDeleteViewModel { get; set; }
        public deleteWindow(Gadget gadget)
        {
            InitializeComponent();

            gadgetDeleteViewModel = new GadgetDeleteViewModel(gadget);
            DataContext = gadgetDeleteViewModel;
        }
    }
}
