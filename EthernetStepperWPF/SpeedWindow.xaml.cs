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
using System.Windows.Shapes;

namespace EthernetStepperWPF
{
    /// <summary>
    /// Логика взаимодействия для SpeedWindow.xaml
    /// </summary>
    public partial class SpeedWindow : Window
    {
        public SpeedWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Speed
        {
            get { return speedBox.Text; }
        }
    }
}
