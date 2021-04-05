using System.Net;
using System.Net.Sockets;
using System.Text;


namespace EthernetLib
{
    public class ReceiverUDP
    {
        private UdpClient receivingUdpClient;
        int localport;
        string message = null;
        bool isalive = true;

        public int Localport
        {
            get { return localport; }
            set { localport = value; }
        }

        public string Message
        {
            get { return message; }

        }
        public bool isAlive
        {
            get { return isalive; }
            set { isalive = value; }
        }

        public void StartReceive()
        {

            // Создаем UdpClient для чтения входящих данных
            receivingUdpClient = new UdpClient(localport);

            IPEndPoint RemoteIpEndPoint = null;
            isAlive = true;



            try
            {
                while (isAlive)
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
            catch (System.Exception ex)
            {
                
            }


        }

        public void Stopreceive()
        {
            isalive = false;
            receivingUdpClient.Close();
        }
    }
}
