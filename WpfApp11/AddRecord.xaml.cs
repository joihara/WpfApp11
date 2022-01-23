using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using WpfApp11.Enum;
using WpfApp11.Struct;

namespace WpfApp11
{
    /// <summary>
    /// Логика взаимодействия для RecordChange.xaml
    /// </summary>
    public partial class AddRecord : Window
    {
        public StructClient client = new StructClient();

        public AddRecord()
        {
            InitializeComponent();
            


        }

        private void Chancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.First_name = ((TextBox)sender).Text;
        }

        private void RLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Last_name = ((TextBox)sender).Text;
        }

        private void RSecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Second_name = ((TextBox)sender).Text;
        }

        private void RPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Phone_number = ((TextBox)sender).Text;
        }

        private void RSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Passport_number_and_series = ((TextBox)sender).Text;
        }
    }
}
