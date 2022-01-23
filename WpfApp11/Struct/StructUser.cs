using System;
using WpfApp11.Enum;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructUser
    {
        public string Name { get; set; } //Имя пользователя
        public string Password { get; set; } //Пароль пользователя
        public EnumTypeUser TypeUser { get; set; } //Тип пользователя
    }
}
