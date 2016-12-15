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
using ch.hsr.wpf.gadgeothek.websocket;

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
            var client = new websocket.WebSocketClient(serverUrl);
            RestServiceBase.IsLogging = true;

            DataContext = this;

            Gadgets = new ObservableCollection<Gadget>(service.GetAllGadgets());
            Loans = new ObservableCollection<Loan>(service.GetAllLoans());


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
                else if (e.Notification.Target == typeof(Loan).Name.ToLower())
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
                else
                {
                    // Do nothing
                }
            };

            var bgTask = client.ListenAsync();


        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            newGadget win2 = new newGadget();
            win2.Show();
        }

        private void removebutton_Click(object sender, RoutedEventArgs e)
        {
            var gadget = (Gadget)gadgets.SelectedItem;
            if (gadgets.SelectedItem == null)
            {
                // No Gadget is selected
                MessageBox.Show("Please first selecet a item and press than Deleted Gadget.");
            }
            else
            {
                deleteWindow win3 = new deleteWindow(gadget);
                win3.Show();
                win3.yes.Click += delegate
                {
                    if (service.DeleteGadget(gadget))
                    {
                        // Close the window, if the operation was succesfull
                        win3.Close();
                    }
                    else
                    {
                        // Print an error message if the operation failed.
                        MessageBox.Show("It didn't work. Check your connection to the server and try it again.");
                    }
                };
            }

        }
    }
}
