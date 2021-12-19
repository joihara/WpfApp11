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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp11.Library;
using WpfApp11.Struct;

namespace WpfApp11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            StructUser user = new StructUser { 
                Name = "Admin", 
                Password="Admin",
                TypeUser = Enum.EnumTypeUser.Administrator
            };

            List<StructUser> users = new List<StructUser>
            {
                user
            };

            SerializeConfig<StructUser[]>.Serialize("db.xml", users.ToArray());
            var read = SerializeConfig<StructUser[]>.DeSerialize("db.xml");
            Console.WriteLine();
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            var userName = user.Text;
            var userPassword = password.Password;

        }
    }
}
