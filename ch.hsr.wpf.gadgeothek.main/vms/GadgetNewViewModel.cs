using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.websocket;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ch.hsr.wpf.gadgeothek.main.vms
{
    public class GadgetNewViewModel : Base
    {
        private LibraryAdminService service;

        public Gadget newGadget { get; set; }

        public ICommand addGadgetCom { get; set; }
        public ICommand cancelGadgetCom { get; set; }

        public GadgetNewViewModel()
        {
            service = new LibraryAdminService(ConfigurationManager.AppSettings["serverGadgeothek"]);

            newGadget = new Gadget();

            addGadgetCom = new RelayCommand<Window>((x) => funcAddGadget(x), (x) => true);
            cancelGadgetCom = new RelayCommand<Window>((x) => funcCancel(x), (x) => true);
        }

        private void funcCancel(Window window)
        {
            // Do nothing, when cancled is pressed
            window.Close();
        }

        private void funcAddGadget(Window window)
        {
            if (!string.IsNullOrWhiteSpace(newGadget.Name) && !string.IsNullOrWhiteSpace(newGadget.Manufacturer) && newGadget.Price > 0)
            {
                var gadget = new Gadget(newGadget.Name) { Manufacturer = newGadget.Manufacturer, Price = newGadget.Price, Condition = newGadget.Condition };
                if (!service.AddGadget(gadget))
                {
                    // Error Output
                    MessageBox.Show($"{gadget} konnte nicht hinzugefügt werden...");
                    Console.WriteLine($"{gadget} konnte nicht hinzugefügt werden...");
                }
                else
                {
                    window.Close();
                }
            }
            else
            {
                // Do nothing
                MessageBox.Show("Bitte alle Felder ausfüllen.");
                Console.WriteLine("Bitte alle Felder ausfüllen.");
            }

        }
    }
}
