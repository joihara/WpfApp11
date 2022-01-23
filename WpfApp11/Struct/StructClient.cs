using System;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructClient
    {
        public string First_name { set; get; } //Имя
        public string Last_name { set; get; } //Фамилия
        public string Second_name { set; get; } //Отчество
        public string Phone_number { set; get; } //Номер телефона
        public string Passport_number_and_series { set; get; } //Номер и серия паспорта
        public StructChangeType Change { set; get; } //Время изменения записи

    }
}
