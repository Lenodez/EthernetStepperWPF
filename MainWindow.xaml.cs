using System.Windows;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NuGet.Protocol.Plugins;
using System.Threading.Tasks;

namespace EthernetStepperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IPAddress remoteIPAddress;
        private static int remotePort;
        private static int localPort;
        private static int sendPort;
        public bool isalive = false;
        Thread tReceive = null;
        UdpClient receivingUdpClient = null;
        public MainWindow()
        {
            InitializeComponent();
            loginButton.IsEnabled = true;
            logoutButton.IsEnabled = false;
            sendButton.IsEnabled = false;
            commandTextBox.IsReadOnly = true;
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            localPort = Int16.Parse(localportTextBox.Text);
            remotePort = Int16.Parse(remoteportTextBox.Text);
            sendPort = Int16.Parse(sendportbox.Text);
            remoteIPAddress = IPAddress.Parse(remoteadressTextBox.Text);
            localportTextBox.IsReadOnly = true;
            remoteportTextBox.IsReadOnly = true;
            remoteadressTextBox.IsReadOnly = true;
            isalive = true;

            try
            {
                tReceive = new Thread(new ThreadStart(Receiver));
                tReceive.Start();
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
        private void Receiver()
        {
            // Создаем UdpClient для чтения входящих данных
            receivingUdpClient = new UdpClient(localPort);

            IPEndPoint RemoteIpEndPoint = null;

            try
            {


                while (isalive)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = receivingUdpClient.Receive(
                       ref RemoteIpEndPoint);

                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    receivedMessageTextBox.Text = returnData.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            Send(commandTextBox.Text);
        }

        public static void Send(string datagram)
        {
            // Создаем UdpClient
            UdpClient sender = new UdpClient(sendPort);

            // Создаем endPoint по информации об удаленном хосте
            IPEndPoint endPoint = new IPEndPoint(remoteIPAddress, remotePort);

            try
            {
                // Преобразуем данные в массив байтов
                byte[] bytes = Encoding.UTF8.GetBytes(datagram);

                // Отправляем данные
                sender.Send(bytes, bytes.Length, endPoint);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n  " + ex.Message);
            }
            finally
            {
                // Закрыть соединение
                sender.Close();
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            EndSocket();
            isalive = false;
            tReceive.Abort();
            receivingUdpClient.Close();
        }
        private void EndSocket()
        {
            localportTextBox.IsReadOnly = false;
            remoteportTextBox.IsReadOnly = false;
            remoteadressTextBox.IsReadOnly = false;
            loginButton.IsEnabled = true;
            logoutButton.IsEnabled = false;
            commandTextBox.IsReadOnly = true;
        }
    }
}
