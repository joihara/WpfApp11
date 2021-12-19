using System;
using System.Security.Cryptography;
using System.Text;
using WpfApp11.Struct;

namespace WpfApp11.Library
{
    public class Crypt
    {
        /// <summary>
        /// Получение хеша пароля
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Проверка пароля
        /// </summary>
        /// <param name="enterPassword"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool GetPass(string enterPassword, StructUser user) {
            return user.Password.Equals(GetHash(enterPassword));
        }
    }
}
