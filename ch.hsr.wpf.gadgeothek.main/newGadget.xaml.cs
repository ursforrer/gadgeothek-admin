using ch.hsr.wpf.gadgeothek.domain;
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
        private LibraryAdminService service;

        public newGadget()
        {
            InitializeComponent();
            service = new LibraryAdminService(ConfigurationManager.AppSettings["serverGadgeothek"]);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing, when cancled is pressed
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            domain.Condition cond = mapToEnum(input_add_condition.SelectedIndex);
            var gadget = new Gadget(input_add_description.Text) { Manufacturer = input_add_manufacturer.Text, Price = double.Parse(input_add_price.Text), Condition = cond };
            if (!service.AddGadget(gadget))
            {
                // Error Output
                MessageBox.Show($"{gadget} konnte nicht hinzugefügt werden...");
                Console.WriteLine($"{gadget} konnte nicht hinzugefügt werden...");
            }
            else
            {
                this.Close();
            }
        }

        private domain.Condition mapToEnum(int index)
        {
            // Own method, because Enum.Parse is not working
            switch(index)
            {
                case 0:
                    return domain.Condition.New;
                case 1:
                    return domain.Condition.Good;
                case 2:
                    return domain.Condition.Damaged;
                case 3:
                    return domain.Condition.Waste;
                case 4:
                    return domain.Condition.Lost;
                default:
                    return domain.Condition.New;

            }
        }
    }
}
