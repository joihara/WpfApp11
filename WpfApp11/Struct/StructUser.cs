using System;
using WpfApp11.Enum;
using WpfApp11.Library;

namespace WpfApp11.Struct
{
    [Serializable]
    public struct StructUser
    {
        public string Name { get; }
        public string Password { get; }
        public EnumTypeUser TypeUser { get; }

        public StructUser(string name, string password, EnumTypeUser typeUser)
        {
            Name = name;
            Password = Crypt.GetHash(password);
            TypeUser = typeUser;
        }
    }
}
