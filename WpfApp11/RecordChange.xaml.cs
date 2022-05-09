using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfApp11.Enum;
using WpfApp11.Struct;

namespace WpfApp11
{
    /// <summary>
    /// Логика взаимодействия для RecordChange.xaml
    /// </summary>
    public partial class RecordChange : Window
    {
        private string User { set; get; }
        public StructClient TempClient { set; get; }
        private EnumTypeUser TypeUser { set; get; }
        private TextBox[] textBoxes = null;
        public bool isCancel = true;

        

        public RecordChange(StructClient client, EnumTypeUser typeUser, string UserName)
        {
            InitializeComponent();
            User = UserName;
            TypeUser = typeUser;
            TempClient = client;

            textBoxes = GetAllTextBox();



            var listEdit = arrayEdit(typeUser);

            //Операция для полей ввода
            for (int i = 0; i < textBoxes.Length; i++)
            {
                var item = textBoxes[i];
                var equalsOnlyRead = listEdit.Any(e => e == i);
                //Добавление слушателей
                if (equalsOnlyRead)
                {
                    item.TextChanged += TextChange;

                }
                else {
                    item.IsReadOnly = true;
                    item.BorderBrush = Brushes.Red;
                }
                //Добавление текста
                item.Text = client[i];
            }


        }

        private void Chancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Изменение полей
        private void TextChange(object sender, TextChangedEventArgs e)
        {
            int indexator = 0;
            foreach (var item in textBoxes)
            {
                var ChangedText = item.Text.Equals(TempClient[indexator++]);
                if (!ChangedText)
                {
                    Change.IsEnabled = true;
                    break;
                }
                else {
                    Change.IsEnabled = false;
                }
            }
            
        }

        private int[] arrayEdit(EnumTypeUser typeUser) {
            switch (typeUser) {
                case EnumTypeUser.Consultant:
                    return new int[] { 3 };
                case EnumTypeUser.Manager:
                    return new int[] { 0,1,2,3,4,5 };
                case EnumTypeUser.Administrator:
                    return new int[] { 0,1,2,3,4,5 };
                default:
                    return null;
            }
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

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            StructClient client = TempClient;

            if (TypeUser != EnumTypeUser.Consultant) {
                client.First_name = RFirstName.Text;
                client.Second_name = RSecondName.Text;
                client.Last_name = RLastName.Text;
                client.Passport_number_and_series = RSeries.Text;
            }
            client.Phone_number = RPhone.Text;
            client.Change =
                new StructChangeType {
                    DataTimeChanged = DateTime.Now.ToString(),
                    TypeUser = TypeUser,
                    NameUser = User,
                    TypeChanged = "Обновление информации"
                };

            TempClient = client;
            isCancel = false;
            Close();
        }


        private TextBox[] GetAllTextBox() {
            List<TextBox> list = new List<TextBox> ();
            list.Add((TextBox)Base.FindName("RFirstName"));
            list.Add((TextBox)Base.FindName("RLastName"));
            list.Add((TextBox)Base.FindName("RSecondName"));
            list.Add((TextBox)Base.FindName("RPhone"));
            list.Add((TextBox)Base.FindName("RSeries"));
            return list.ToArray();
        }
    }
}
