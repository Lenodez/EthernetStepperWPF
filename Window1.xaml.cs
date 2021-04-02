using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace EthernetStepperWPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window

    {
        ReceiverUDP receiverUDP = new ReceiverUDP();
        Sender senderUDP = new Sender();
        public Window1(MainWindow Main)
        {
            InitializeComponent();
            MainWindow main = this.Owner as MainWindow;
            receiverUDP.Localport = Int16.Parse(Main.localportTextBox.Text);
            senderUDP.Remoteport = Int16.Parse(Main.remoteportTextBox.Text);
            senderUDP.Sendport = Int16.Parse(Main.sendportbox.Text);
            senderUDP.IP = IPAddress.Parse(Main.remoteadressTextBox.Text);
            Task Receiving = Task.Run(() =>
            receiverUDP.StartReceive());
            Task task = Task.Run(() =>
            Message());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MainWindow.CloseForm();
        }

        private void Message()
        {
            while (receiverUDP.isAlive)
            {
                this.Dispatcher.Invoke(() =>
                {
                    receivedBox.Text = receiverUDP.Message;
                });
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            receiverUDP.Stopreceive();
            main.Show();
            Hide();
        }


        

        private void leftButton_Click(object sender, RoutedEventArgs e)
        {

            senderUDP.Send("left");
        }

        private void rightButton_Click(object sender, RoutedEventArgs e)
        {

            senderUDP.Send("Right");
        }

        private void speedButton_Click(object sender, RoutedEventArgs e)
        {

            senderUDP.Send("s" + speedBox.Text);
        }

        private void distanceButton_Click(object sender, RoutedEventArgs e)
        {

            senderUDP.Send("d" + distanceBox.Text);
        }
    }
}
