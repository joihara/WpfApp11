using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using WpfApp11.Enum;
using WpfApp11.Struct;

namespace WpfApp11.Library
{
    public class FileUtil
    {
        #region Пользовательская база
        public bool CreateUser(string user, string pass) {

            string passMd5 = Crypt.GetHash(pass);

            var users = ReadUsers().ToList();

            var countSearch = users.Count(e => e.Name!=null && e.Name.Equals(user));
            var isValid = countSearch > 0;
            if (isValid) {
                return false;
            }
            var userReg = new StructUser
            {
                Name = user,
                Password = passMd5,
                TypeUser = Enum.EnumTypeUser.Consultant
            };
            users.Add(userReg);

            SerializeConfig<StructUser[]>.Serialize("userdb.xml", users.ToArray());
            return true;
        }

        public StructUser[] ReadUsers() {
            StructUser[] read;
            try {
                read = SerializeConfig<StructUser[]>.DeSerialize("userdb.xml");
            }
            catch(Exception) {
                read = Array.Empty<StructUser>();
            }
            

            return read;
        }

        /// <summary>
        /// Проверка на верность введённых данных учёной записи пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns>Вернёт пользователя если данные сходяться</returns>
        public StructUser? CheackUser(string user, string pass) {
            var users = ReadUsers();
            var userIsValid = users.Any(e=>e.Name!=null && e.Name.Equals(user));
            if (userIsValid)
            {
                var userSearch = users.First(e => e.Name.Equals(user));
                if (Crypt.GetPass(pass, userSearch))
                {
                    return userSearch;
                }
            }
            return null;
        }
        #endregion
        #region Клиентская база
        /// <summary>
        /// Название файла для сохранения списка клиентов
        /// </summary>
        private readonly string filename = "Client.db";

        /// <summary>
        /// Чтение списка с клиентами Зависимость от группы
        /// </summary>
        /// <returns></returns>
        public StructClient[] ReadClients(EnumTypeUser typeUser)
        {
        var outer = ReadClients();
            if (typeUser == EnumTypeUser.Consultant)
            {
                List<StructClient> svm = new List<StructClient>();
                foreach (var client in outer) {
                    var temp = client;
                    string star = "**** ******";
                    temp[4] = star;
                    svm.Add(temp);
                }

                outer = svm.ToArray();
            }
        return outer;
        }

        /// <summary>
        /// Чтение списка с клиентами
        /// </summary>
        /// <returns></returns>
        public StructClient[] ReadClients()
        {
            StructClient[] outer;

            XmlSerializer x = new XmlSerializer(typeof(StructClient[]));
            FileStream fs = FileWait();

            TextReader reader = new StreamReader(fs);
            try
            {
                outer = (StructClient[])x.Deserialize(reader);
            }
            catch (InvalidOperationException)
            {
                outer = Array.Empty<StructClient>();
            }

            return outer;
        }

        /// <summary>
        /// Добавление в список клиента
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(StructClient client)
        {
            List<StructClient> clients = new List<StructClient>();

            var reader = ReadClients();

            if (reader != null)
            {
                clients = reader.ToList();
            }

            clients.Add(client);
            FileStream fs = FileWait();
            XmlSerializer x = new XmlSerializer(typeof(StructClient[]));
            TextWriter writer = new StreamWriter(fs);
            x.Serialize(writer, clients.ToArray());
        }

        /// <summary>
        /// Изменение данных у клиента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool EditClient(int id, StructClient client)
        {
            try
            {
                List<StructClient> clients = new List<StructClient>();

                var reader = ReadClients();

                if (reader != null)
                {
                    clients = reader.ToList();
                }

                clients[id] = client;
                FileStream fs = FileWait();
                XmlSerializer x = new XmlSerializer(typeof(StructClient[]));
                TextWriter writer = new StreamWriter(fs);
                x.Serialize(writer, clients.ToArray());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Ожидание файла если он занят
        /// </summary>
        /// <returns></returns>
        private FileStream FileWait()
        {
            FileStream fs;
            while (true)
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                if (fs.CanRead)
                {
                    Thread.Sleep(1000);
                    break;
                }
            }
            return fs;
        }
        #endregion






    }
}
