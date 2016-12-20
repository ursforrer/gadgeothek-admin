using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.websocket;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

namespace ch.hsr.wpf.gadgeothek.main.vms
{
    class GadgetViewModel : Base
    {
        public ObservableCollection<Gadget> Gadgets { get; set; }
        private LibraryAdminService service;
        private websocket.WebSocketClient client;

        private Gadget _selGadget;
        public Gadget selGadget
        {
            get { return _selGadget; }
            set { SetProperty(ref _selGadget, value, nameof(selGadget)); }
        }

        public ICommand delGadgetCom { get; set; }
        public ICommand newGadgetCom { get; set; }

        public GadgetViewModel()
        {
            initalLoadGadgets();

            newGadgetCom = new RelayCommand<Gadget>((x) => funcNewGadget(), (x) => true);
            delGadgetCom = new RelayCommand<Gadget>((x) => funcDelGadget(), (x) => true);

            client.NotificationReceived += (o, e) =>
            {
                Console.WriteLine("WebSocket::Notification: " + e.Notification.Target + " > " + e.Notification.Type);

                // demonstrate how these updates could be further used
                if (e.Notification.Target == typeof(Gadget).Name.ToLower())
                {
                    // deserialize the json representation of the data object to an object of type Gadget
                    var gadget = e.Notification.DataAs<Gadget>();
                    switch (e.Notification.Type)
                    {
                        case WebSocketClientNotificationTypeEnum.Add:
                            Gadgets.Add(gadget);
                            break;
                        case WebSocketClientNotificationTypeEnum.Delete:
                            Gadgets.Remove(gadget);
                            break;
                    }
                }
            };

            var bgTask = client.ListenAsync();
        }

        private void funcNewGadget()
        {
            var window = new newGadget();
            window.Show();
        }

        private void funcDelGadget()
        {
            //var gadget = (Gadget)gadgetsview.SelectedItem;
            if (selGadget == null)
            {
                // No Gadget is selected
                MessageBox.Show("Please first selecet a item and press than Deleted Gadget.");
            }
            else
            {
                var deletewindow = new deleteWindow(selGadget);
                deletewindow.Show();
            }

        }

        private void initalLoadGadgets()
        {
            // Load inital all Gadgets and save settings
            service = new LibraryAdminService(ConfigurationManager.AppSettings["serverGadgeothek"]);
            client = new websocket.WebSocketClient(ConfigurationManager.AppSettings["serverGadgeothek"]);
            Gadgets = new ObservableCollection<Gadget>(service.GetAllGadgets());
        }
    }
}
