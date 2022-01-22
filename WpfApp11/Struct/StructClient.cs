using System;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructClient
    {
        public string first_name { set; get; } //Имя
        public string last_name { set; get; } //Фамилия
        public string second_name { set; get; } //Отчество
        public string phone_number { set; get; } //Номер телефона
        public string passport_number_and_series { set; get; } //Номер и серия паспорта
        public string dates_of_the_time_change { set; get; } //Время изменения записи

    }
}
