using System;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructClient
    {
        private string first_name; //Имя
        private string last_name; //Фамилия
        private string second_name; //Отчество
        private string phone_number; //Номер телефона
        private string passport_number_and_series; //Номер и серия паспорта


        /// <summary>
        /// Данные клиента
        /// </summary>

        public StructClient(string fullName, string phone_number, string passport_number_and_series) {
            var names = fullName.Split(' ');
            first_name = names[0];
            last_name = names[1];
            second_name = names[2];

            this.phone_number = phone_number;
            this.passport_number_and_series = passport_number_and_series;
        }

        public string FullName(string first,string last,string second) { 
            return first + " " + last + " " + second;
        }
    }
}
