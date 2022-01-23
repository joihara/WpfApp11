namespace WpfApp11.User
{
    public class Manager : Consultant
    {
        #region Методы доступа
        //Доступ к паспорту
        public new string GetPassportSeries()
        {
            return PassportSeries;
        }
        public void SetPassportSeries(string series)
        {
            PassportSeries = series;
            UpdateType(Enum.EnumTypeField.Passport, "Изменение Паспорта", Enum.EnumTypeUser.Manager);
            Update();
        }
        //Доступ к имени клиента
        public void SetFullName(string first, string second, string last)
        {
            FirstName = first;
            SecondName = second;
            LastName = last;
            UpdateType(Enum.EnumTypeField.FullName, "Изменение Имени", Enum.EnumTypeUser.Manager);
            Update();
        }
        #endregion
    }
}
