using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp11.Enum;

namespace WpfApp11.Struct
{
    public struct StructChangeType
    {
        public string DataTimeChanged { set; get; } //дата и время изменения записи;
        public EnumTypeField DataChanged { set; get; } //какие данные изменены;
        public string TypeChanged; //тип изменений;
        public EnumTypeUser TypeUser { set; get; } //кто изменил данные.      
    }
}
