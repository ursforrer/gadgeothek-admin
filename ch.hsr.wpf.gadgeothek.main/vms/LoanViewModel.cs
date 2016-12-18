using ch.hsr.wpf.gadgeothek.domain;
using ch.hsr.wpf.gadgeothek.service;
using ch.hsr.wpf.gadgeothek.websocket;
using System;
using System.Collections.ObjectModel;
using System.Configuration;

namespace ch.hsr.wpf.gadgeothek.main.vms
{
    class LoanViewModel : Base
    {
        public ObservableCollection<Loan> Loans { get; set; }
        private LibraryAdminService service;
        private websocket.WebSocketClient client;

        private Gadget _selGadget;
        public Gadget selGadget
        {
            get { return _selGadget; }
            set { SetProperty(ref _selGadget, value, nameof(selGadget)); }
        }

        public LoanViewModel()
        {
            initalLoadLoans();

            client.NotificationReceived += (o, e) =>
            {
                Console.WriteLine("WebSocket::Notification: " + e.Notification.Target + " > " + e.Notification.Type);

                // demonstrate how these updates could be further used
                if (e.Notification.Target == typeof(Loan).Name.ToLower())
                {
                    // deserialize the json representation of the data object to an object of type Gadget
                    var loan = e.Notification.DataAs<Loan>();
                    switch (e.Notification.Type)
                    {
                        case WebSocketClientNotificationTypeEnum.Add:
                            Loans.Add(loan);
                            break;
                        case WebSocketClientNotificationTypeEnum.Delete:
                            Loans.Remove(loan);
                            break;
                    }
                }
            };

            var bgTask = client.ListenAsync();
        }

        private void initalLoadLoans()
        {
            // Load inital all Gadgets and save settings
            service = new LibraryAdminService(ConfigurationManager.AppSettings["serverGadgeothek"]);
            client = new websocket.WebSocketClient(ConfigurationManager.AppSettings["serverGadgeothek"]);
            Loans = new ObservableCollection<Loan>(service.GetAllLoans());
        }
    }
}
