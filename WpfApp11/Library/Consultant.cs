using System.Collections.Generic;
using System.Linq;

namespace WpfApp11.Library
{
    public class Consultant
    {
        private readonly FileUtil fileUtil = new FileUtil();

        public void EditPhoneNumber(int id, string phoneNumber) {
            if (phoneNumber != "")
            {
                var client = fileUtil.ReadClients()[id];
                client.phone_number = phoneNumber;
                fileUtil.EditClient(id, client);
            }
        }

        public string[] GetInfoClients()
        {
            var clients = fileUtil.ReadClients();
            List<string> ClientsList = new List<string>();
            foreach (var client in clients) {
                string isPassport = client.passport_number_and_series.Count() > 0 ? "******************" : "";
                string fullInfo = $"Фамилия: {client.second_name};" +
                                    $"Имя: {client.first_name};" +
                                    $"Отчество: {client.last_name};" +
                                    $"Номер телефона: {client.phone_number};" +
                                    $"Серия, номер паспорта: {isPassport}/";
                ClientsList.Add(fullInfo);
            }

            return ClientsList.ToArray();
        }
    }
}
