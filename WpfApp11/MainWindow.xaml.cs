using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp11.Enum;
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

            //LTypeUser = EnumTypeUser.Consultant; //Debug

            
            Console.WriteLine();
        }

        private void enter_button_Click(object sender, RoutedEventArgs e)
        {
            

        }
        List<StructClient> list = new List<StructClient>();

        private void UpdateTable()
        {
            list = fileUtil.ReadClients(LTypeUser).ToList();
            listView.Items.Clear();
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
                View.Visibility = Visibility.Visible;
                LTypeUser = isValidate.Value.TypeUser;
                UpdateTable();
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

        private StructClient SelectClient { set; get; }
        private EnumTypeUser LTypeUser { set; get; }

        private void UpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            new RecordChange(listView.SelectedIndex ,SelectClient, LTypeUser).ShowDialog();
            UpdateRecord.IsEnabled = false;
            UpdateTable();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectClient = list[listView.SelectedIndex];
            UpdateRecord.IsEnabled = true;
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new AddRecord();
            addDialog.ShowDialog();
            fileUtil.AddClient(addDialog.client);
            UpdateRecord.IsEnabled = false;
            UpdateTable();
        }
    }
}
