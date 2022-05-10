using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        
        public MainWindow()
        {

            InitializeComponent();
            SwitchVisibility(EnumTypeSwitch.View);
            Debug();

            
            Console.WriteLine();
        }

        #region Value
        private readonly FileUtil fileUtil = new FileUtil();
        List<StructClient> list = new List<StructClient>();
        private string User;
        /// <summary>
        /// Выбранный клиент из списка
        /// </summary>
        private StructClient SelectClient { set; get; }
        /// <summary>
        /// Номер клинта в списке
        /// </summary>
        private int selectIdinTable { set; get; }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        private EnumTypeUser LTypeUser { set; get; }
        #endregion

        #region Debug
        private void Debug()
        {
            var userName = "joihara";
            var userPassword = "adgjm";
            Enter(userName, userPassword);
        }
        #endregion 

        #region Buttons
        //Кнопка Регистрация
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SwitchVisibility(EnumTypeSwitch.Register);
        }
        //Кнопка Вход
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Enter(user.Text, password.Password);
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
            SwitchVisibility(EnumTypeSwitch.Enter);
        }
        private void UpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            var update = new RecordChange(SelectClient, LTypeUser, User);
            update.ShowDialog();

            if (!update.isCancel)
            {
                fileUtil.EditClient(listView.SelectedIndex, update.TempClient);
                UpdateTable();
            }

            UpdateRecord.IsEnabled = false;
        }
        /// <summary>
        /// Добавить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new AddRecord();
            addDialog.ShowDialog();
            if (!addDialog.isCancel)
            {
                fileUtil.AddClient(addDialog.client);
                UpdateTable();
            }
            UpdateRecord.IsEnabled = false;
        }

        /// <summary>
        /// Выход из учётной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitUser_Click(object sender, RoutedEventArgs e)
        {
            SwitchVisibility(EnumTypeSwitch.Enter);
        }

        private void password_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Enter(user.Text, password.Password);
            }
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecord.IsEnabled = false;
            DeleteRecord.Visibility = Visibility.Collapsed;
            fileUtil.DeleteClient(selectIdinTable);
            UpdateTable();
        }
        /// <summary>
        /// Открытие информации о клиенте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenClient_Click(object sender, RoutedEventArgs e)
        {
            LLastName.Content = "Фамилия: " + SelectClient.Last_name;
            LFirstName.Content = "Имя: " + SelectClient.First_name;
            LSecondName.Content = "Отчество: " + SelectClient.Second_name;
            LTypeClient.Content = "Клиент: " + SelectClient.TypeUser.GetDescription();

            SwitchVisibility(EnumTypeSwitch.ClientInfo);
        }

        private void ExitToClients_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
            SwitchVisibility(EnumTypeSwitch.View);
        }
        #endregion

        #region Func
        /// <summary>
        /// Обновление Таблицы с данными
        /// </summary>
        private void UpdateTable()
        {
            OpenClient.IsEnabled = false;
            DeleteRecord.IsEnabled = false;
            DeleteRecord.Visibility = Visibility.Collapsed;
            UpdateRecord.IsEnabled = false;
            list = fileUtil.ReadClients(LTypeUser).ToList();
            listView.Items.Clear();
            foreach (var item in list)
            {
                listView.Items.Add(item);
            }
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        public void Register()
        {
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
                else
                {
                    registerValidate.Text = "Пользователь Зарегистрирован";
                }
            }
            else
            {
                registerValidate.Text = "Пароли не совпадают";
            }


        }

        /// <summary>
        /// Проверка входа с учётными данными
        /// </summary>
        public void Enter(string userName, string userPassword)
        {
            User = userName;
            var isValidate = fileUtil.CheackUser(userName, userPassword);
            if (isValidate != null)
            {
                SwitchVisibility(EnumTypeSwitch.View);
                LTypeUser = isValidate.Value.TypeUser;

                UserName.Content = $"Пользователь: {userName}";
                UserType.Content = $"Группа: {LTypeUser}";
                if (LTypeUser == EnumTypeUser.Consultant)
                {
                    AddRecord.Visibility = Visibility.Collapsed;
                }
                else
                {
                    AddRecord.Visibility = Visibility.Visible;
                }
                AddRecord.IsEnabled = LTypeUser != EnumTypeUser.Consultant;
                UpdateTable();
            }
            else
            {
                enterValidate.Text = "Неверные данные";
            }

        }
        /// <summary>
        /// Переключение между слоями
        /// </summary>
        /// <param name="type"></param>
        private void SwitchVisibility(EnumTypeSwitch type)
        {
            bool isEnter = type == EnumTypeSwitch.Enter;
            bool isRegister = type == EnumTypeSwitch.Register;
            bool isView = type == EnumTypeSwitch.View;
            bool isClientInfo = type == EnumTypeSwitch.ClientInfo;

            VisibilityEnter(isEnter, EnterGrid);
            VisibilityEnter(isRegister, RegisterGrid);
            VisibilityEnter(isView, ViewGrid);
            VisibilityEnter(isClientInfo, ClientInfo);

        }

        /// <summary>
        /// Переключение видимости для сетки
        /// </summary>
        /// <param name="view"></param>
        /// <param name="grid"></param>
        private void VisibilityEnter(bool view, Grid grid)
        {
            if (view)
            {
                grid.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Visibility = Visibility.Collapsed;
            }

        }
        #endregion

        #region ListBox
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectIdinTable = listView.SelectedIndex;
            if (selectIdinTable != -1)
            {
                SelectClient = list[selectIdinTable];
                UpdateRecord.IsEnabled = true;
                OpenClient.IsEnabled = true;
                if (LTypeUser == EnumTypeUser.Administrator)
                {
                    DeleteRecord.IsEnabled = true;
                    DeleteRecord.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion


























    }
    /// <summary>
    /// Нумерация слоя для отображения
    /// </summary>
    enum EnumTypeSwitch { 
        Enter, Register, View, ClientInfo
    }
}
