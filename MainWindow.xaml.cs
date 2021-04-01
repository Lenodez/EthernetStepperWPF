using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace EthernetStepperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Sender senderUDP = new Sender();
        public delegate void Method();
        private static Method close;

        public MainWindow()
        {
            InitializeComponent();
            loginButton.IsEnabled = true;
            logoutButton.IsEnabled = false;
            sendButton.IsEnabled = false;
            commandTextBox.IsReadOnly = true;
            close = new Method(Close);

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            senderUDP.Remoteport = Int16.Parse(remoteportTextBox.Text);
            senderUDP.Sendport = Int16.Parse(sendportbox.Text);
            senderUDP.IP = IPAddress.Parse(remoteadressTextBox.Text);
            localportTextBox.IsReadOnly = true;
            remoteportTextBox.IsReadOnly = true;
            remoteadressTextBox.IsReadOnly = true;



            try
            {
                loginButton.IsEnabled = false;
                logoutButton.IsEnabled = true;
                sendButton.IsEnabled = true;
                commandTextBox.IsReadOnly = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            Task Sending = Task.Run(() =>
            senderUDP.Send(commandTextBox.Text));
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(this);
            window1.Owner = this;
            window1.Show();
            Hide();

        }
        public static void CloseForm()
        {
            close.Invoke();
        }

    }



    public class Sender
    {
        int sendport;
        int remoteport;
        IPAddress remoteIPAddress;
        private UdpClient sender;
        public int Sendport
        {
            set { sendport = value; }
        }
        public int Remoteport
        {
            set { remoteport = value; }
        }

        public IPAddress IP
        {
            set { remoteIPAddress = value; }
        }


        public void Send(string datagram)
        {
            sender = new UdpClient(sendport);
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remoteport);
            try
            {
                // Преобразуем данные в массив байтов
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);

                // Отправляем данные
                sender.Send(bytes, bytes.Length, endPoint);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закрыть соединение
                sender.Close();
            }
        }

    }

}
