using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace EthernetStepperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReceiverUDP receiverUDP = new ReceiverUDP();
        Sender senderUDP = new Sender();        

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
            receiverUDP.Localport = Int16.Parse(localportTextBox.Text);
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
            receiverUDP.Stopreceive();                    
            
        }
        
    }

    public class ReceiverUDP
    {
        private UdpClient receivingUdpClient;
        int localport;
        string message = null;

        public int Localport
        {
            get { return localport; }
            set { localport = value; }
        }

        public string Message
        {
            get { return message; }

        }

        public void StartReceive()
        {
            
            // Создаем UdpClient для чтения входящих данных
            receivingUdpClient = new UdpClient(localport);

            IPEndPoint RemoteIpEndPoint = null;

            try
            {


                while (true)
                {
                    // Ожидание дейтаграммы
                    byte[] receiveBytes = receivingUdpClient.Receive(
                       ref RemoteIpEndPoint);

                    // Преобразуем и отображаем данные
                    string returnData = Encoding.UTF8.GetString(receiveBytes);
                    string dataget = returnData.ToString();             
                    
                    message = dataget;
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Stopreceive()
        {
            receivingUdpClient.Close();
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
