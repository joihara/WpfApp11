using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WpfApp11.Struct;

namespace WpfApp11.Library
{
    public class FileUtil
    {
        private bool confidentialitySee;
        public FileUtil(IUser s) {
            confidentialitySee = s.Edit();
        }


        /// <summary>
        /// Название файла для сохранения списка клиентов
        /// </summary>
        private readonly string filename = "Client.db";

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

            if (!confidentialitySee) {
                int index = 0;
                foreach (var item in outer)
                {
                    var series = item.Series_passport_number;

                    outer[index++].Series_passport_number = new string('*', series.Length);
                }
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
        public bool EditClient(int id, StructClient client) {
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

        
    }
}
