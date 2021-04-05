using System;
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

            close = new Method(Close);

        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {


            localportTextBox.IsReadOnly = true;
            remoteportTextBox.IsReadOnly = true;
            remoteadressTextBox.IsReadOnly = true;



            try
            {
                loginButton.IsEnabled = false;
                logoutButton.IsEnabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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







}
