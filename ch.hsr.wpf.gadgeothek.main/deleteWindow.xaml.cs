using ch.hsr.wpf.gadgeothek.domain;
using MahApps.Metro.Controls;
using System.Windows;

namespace ch.hsr.wpf.gadgeothek.main
{
    /// <summary>
    /// Interaktionslogik für deleteWindow.xaml
    /// </summary>
    public partial class deleteWindow : MetroWindow
    {
        public deleteWindow(Gadget gadget)
        {
            InitializeComponent();

            // Prefill the window with the data
            nr.Text = gadget.InventoryNumber;
            price.Text = gadget.Price.ToString();
            cond.Text = gadget.Condition.ToString();
            manu.Text = gadget.Manufacturer;
            name.Text = gadget.Name;
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            // Close if no is pressed in the window.
            this.Close();
        }
    }
}
