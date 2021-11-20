using System;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructClient
    {
        private string surname;
        private string name;
        private string patronymic;
        private string phone_number;
        private string series_passport_number;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname{ get => surname; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get => name; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get => patronymic; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone_number { get => phone_number; set => phone_number = value; }
        /// <summary>
        /// Серия, номер паспорта
        /// </summary>
        public string Series_passport_number {
            get => series_passport_number;
            set => series_passport_number = value;
        }

        /// <summary>
        /// Данные клиента
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="patronymic">Отчество</param>
        /// <param name="phone_number">Номер телефона</param>
        /// <param name="series_passport_number">Серия, номер паспорта</param>
        public StructClient(string surname, string name, string patronymic, string phone_number, string series_passport_number) {
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.phone_number = phone_number;
            this.series_passport_number = series_passport_number;
        }
    }
}
