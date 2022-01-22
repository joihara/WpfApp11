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
        private readonly FileUtil fileUtil = new FileUtil();
        public MainWindow()
        {

            InitializeComponent();
            AddListView();
            Console.WriteLine();
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            

        }

        public void AddListView() { 
            List<StructClient> list = new List<StructClient>();

            list.Add(new StructClient("Олин Роман Алексеевич", "89991257789", "23242424"));
            list.Add(new StructClient("Олин Роман Алексеевич", "89991257789", "23242424"));
            list.Add(new StructClient("Олин Роман Алексеевич", "89991257789", "23242424"));
            list.Add(new StructClient("Олин Роман Алексеевич", "89991257789", "23242424"));

            foreach (var item in list)
            {
                listView.Items.Add(item);
            }

        }

        public void Register() {
            var userName = regUser.Text;
            var userPassword = regPass.Password;
            var reUserPassword = reRegPass.Password;

            if (userPassword == reUserPassword)
            {
                var isValidate = fileUtil.CreateUser(userName, userPassword);
                if (!isValidate)
                {
                    registerValidate.Text = "Пользователь уже существует";
                }
                else {
                    registerValidate.Text = "Пользователь Зарегистрирован";
                }
            }
            else {
                registerValidate.Text = "Пароли не совпадают";
            }

            
        }

        public void Enter() {
            var userName = user.Text;
            var userPassword = password.Password;
            var isValidate = fileUtil.CheackUser(userName, userPassword);
            if (isValidate!=null)
            {
                EnterGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                enterValidate.Text = "Неверные данные";
            }

        }

        //Кнопка Регистрация
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnterGrid.Visibility = Visibility.Hidden;
            RegisterGrid.Visibility = Visibility.Visible;
        }
        //Кнопка Вход
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Enter();
        }
        //Кнопка Зарегистрироваться
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Register();
        }
        //Кнопка Вернуться
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            regUser.Text = "";
            regPass.Password = "";
            reRegPass.Password = "";
            EnterGrid.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Hidden;
        }
    }
}
