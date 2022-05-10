using System.ComponentModel;

namespace WpfApp11.Enum
{
    public enum EnumTypeDepartment
    {
        [Description("Обычный клиент")]
        common = 0,
        [Description("V.I.P Клиент")] 
        vip = 1,
        [Description("Юридическое лицо")] 
        legal = 2
    }


}
