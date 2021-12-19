using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp11.Struct;

namespace WpfApp11.Library
{
    public class Consultant
    {
        public virtual bool Edit() {return false;}

        private FileUtil fileUtil;

        public Consultant() {
            Init();
        }

        public void Init()
        {
            
            
        }

        public void EditPhoneNumber() { 
            
        }

        public string GetInfoClients(int idClient)
        {
            var clients = fileUtil.ReadClients();
            //string fullInfo = $"Фамилия: {}" +
            //                    $"Имя: {}" +
            //                    $"Отчество: {}" +
            //                    $"Номер телефона: {}" +
            //                    $"Серия, номер паспорта: {}";

            return "";
        }
    }
}
