using System;
using WpfApp11.Enum;

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
        public EnumTypeDepartment TypeUser { set; get; } //К какому отделу относится клиент

        public string this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return First_name;
                    case 1:
                        return Last_name;
                    case 2:
                        return Second_name;
                    case 3:
                        return Phone_number;
                    case 4:
                        return Passport_number_and_series;
                }

                return null;
            }
            set {
                switch (i)
                {
                    case 0:
                        First_name = value;
                        break;
                    case 1:
                        Last_name = value;
                        break;
                    case 2:
                        Second_name = value;
                        break;
                    case 3:
                        Phone_number = value;
                        break;
                    case 4:
                        Passport_number_and_series = value;
                        break;
                }
            }
        }
    }
}
