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
    public partial class RecordChange : Window
    {
        private StructClient TempClient { set; get; }
        private TextBox[] textBoxes = null;

        public RecordChange(int index, StructClient client, EnumTypeUser typeUser)
        {
            InitializeComponent();
            TempClient = client;
            
            textBoxes = Base.Children.OfType<TextBox>().ToArray();

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

    }
}
