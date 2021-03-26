using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MainWindow.CloseForm();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = this.Owner as MainWindow;
            receiverUDP.Stopreceive();
            main.Show();
            Hide();
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


                    while (receivingUdpClient.EnableBroadcast)
                    {
                        // Ожидание дейтаграммы
                        byte[] receiveBytes = receivingUdpClient.Receive(
                           ref RemoteIpEndPoint);
                        
                        // Преобразуем и отображаем данные
                        string returnData = Encoding.UTF8.GetString(receiveBytes);
                        string dataget = returnData.ToString();

                        


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
