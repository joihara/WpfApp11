using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public bool isCancel = true;

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
            Сheck();
        }

        private void RLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Last_name = ((TextBox)sender).Text;
            Сheck();
        }

        private void RSecondName_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Second_name = ((TextBox)sender).Text;
            Сheck();
        }

        private void RPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Phone_number = ((TextBox)sender).Text;
            Сheck();
        }

        private void RSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            client.Passport_number_and_series = ((TextBox)sender).Text;
            Сheck();
        }

        private static readonly Regex _regex = new Regex("[^0-9 ]+"); //настройка символов для ввода
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        /// <summary>
        /// Ввод только цифр для телефона
        /// </summary>
        private void RPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        /// <summary>
        /// Ввод только цифр для паспорта
        /// </summary>
        private void RSeries_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            isCancel = false;
            Close();
        }

        private void Сheck() {

            bool FirstName = RFirstName.Text.Length > 0;
            bool LastName = RLastName.Text.Length > 0;
            bool SecondName = RSecondName.Text.Length > 0;
            bool Phone = RPhone.Text.Length >= 2;
            bool Series = RSeries.Text.Length >= 10;

            Add.IsEnabled = FirstName && SecondName && LastName && Phone && Series;
        }

        private void ComboTypeClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ComboTypeClient.SelectedIndex;
            client.TypeUser = (EnumTypeDepartment)index;
        }
    }

}
