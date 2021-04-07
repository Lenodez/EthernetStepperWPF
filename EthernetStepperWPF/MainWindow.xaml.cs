using System.Windows;


namespace EthernetStepperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public delegate void Method();
        private static Method close;

        public MainWindow()
        {
            InitializeComponent();
            loginButton.IsEnabled = true;
            logoutButton.IsEnabled = false;
            button.IsEnabled = false;

            close = new Method(Close);

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = true;
            localportTextBox.IsReadOnly = true;
            remoteportTextBox.IsReadOnly = true;
            remoteadressTextBox.IsReadOnly = true;
            sendportbox.IsReadOnly = true;
            loginButton.IsEnabled = false;
            logoutButton.IsEnabled = true;
        }





        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            localportTextBox.IsReadOnly = false;
            remoteportTextBox.IsReadOnly = false;
            remoteadressTextBox.IsReadOnly = false;
            sendportbox.IsReadOnly = false;
            loginButton.IsEnabled = true;
            logoutButton.IsEnabled = false;

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







}
