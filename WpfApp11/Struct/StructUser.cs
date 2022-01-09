using System;
using WpfApp11.Enum;
using WpfApp11.Library;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public EnumTypeUser TypeUser { get; set; }

        public StructUser(string name, string password, EnumTypeUser typeUser)
        {
            Name = name;
            Password = password;
            TypeUser = typeUser;
        }
    }
}
