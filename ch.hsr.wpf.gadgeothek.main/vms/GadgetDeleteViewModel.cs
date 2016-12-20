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
    public class GadgetDeleteViewModel : Base
    {
        private LibraryAdminService service;

        public Gadget delGadget { get; set; }

        public ICommand sureGadgetCom { get; set; }
        public ICommand cancelGadgetCom { get; set; }

        public GadgetDeleteViewModel(Gadget gadget)
        {
            service = new LibraryAdminService(ConfigurationManager.AppSettings["serverGadgeothek"]);

            delGadget = gadget;

            sureGadgetCom = new RelayCommand<Window>((x) => funcsureGadget(x), (x) => true);
            cancelGadgetCom = new RelayCommand<Window>((x) => funcCancel(x), (x) => true);
        }

        private void funcCancel(Window window)
        {
            // Do nothing, when cancled is pressed
            window.Close();
        }

        private void funcsureGadget(Window window)
        {
            if (service.DeleteGadget(delGadget))
            {
                //Close the window, if the operation was succesfull
                window.Close();
            }
            else
            {
                // Print an error message if the operation failed.
                MessageBox.Show("It didn't work. Check your connection to the server and try it again.");
            }

        }
    }
}
