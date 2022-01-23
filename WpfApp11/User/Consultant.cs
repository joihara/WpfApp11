using System;
using System.Linq;
using WpfApp11.Library;
using WpfApp11.Struct;

namespace WpfApp11.User
{
    public class Consultant
    {
        private readonly FileUtil fileUtil = new FileUtil();
        #region Основные поля
        protected string LastName { set; get; }
        protected string FirstName { set; get; }
        protected string SecondName { set; get; }
        protected string PhoneNumber { set; get; }
        protected string PassportSeries { set; get; }
        protected StructChangeType ChangeType { set; get; }
        #endregion
        #region Методы доступа
        //Доступ к паспорту
        public string GetPassportSeries() {
            var countPassportChar = PassportSeries.Count();
            var result = new string('*', countPassportChar);
            return result;
        }
        //Доступ к имени клиента
        public string GetFullName() {
            return $"{FirstName}:{SecondName}:{LastName}";
        }

        //Доступ к номеру телефона
        public string GetPhoneNumber() {
            return PhoneNumber;
        }
        public bool SetPhoneNumber(string phone)
        {
            if (phone != "")
            {
                PhoneNumber = phone;
                UpdateType(Enum.EnumTypeField.Phone,"Изменение номера телефона");
                Update();
                return true;
            }
            else {
                return false;
            }
        }
        #endregion

        #region Операия обращения к файлу с данными
        public int Index { set; get; }
        protected bool Read() {
            try
            {
                var client = fileUtil.ReadClients()[Index];
                FirstName = client.First_name;
                SecondName = client.Second_name;
                LastName = client.Last_name;
                PhoneNumber = client.Phone_number;
                PassportSeries = client.Passport_number_and_series;
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        protected void UpdateType(Enum.EnumTypeField enumTypeField, string typeChanged, Enum.EnumTypeUser enumTypeUser = Enum.EnumTypeUser.Consultant) {
            ChangeType = new StructChangeType
            {
                DataTimeChanged = DateTime.Now.ToString(),
                TypeUser = enumTypeUser,
                DataChanged = enumTypeField,
                TypeChanged = typeChanged

            };
        }
        protected void Update() {
            var client = new StructClient {
                First_name = FirstName,
                Second_name = SecondName,
                Last_name = LastName,
                Phone_number = PhoneNumber,
                Passport_number_and_series = PassportSeries,
                Change = ChangeType
            };
            fileUtil.EditClient(Index, client);
        } 
        #endregion
    }
}
