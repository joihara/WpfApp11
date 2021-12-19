using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WpfApp11.Library
{
    public static class SerializeConfig<T>
    {

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        public static void Serialize(string path, T type)
        {
            var serializer = new XmlSerializer(type.GetType());
            using (var writer = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(writer, type);
            }
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeSerialize(string path)
        {
            T type;
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(path))
            {
                type = (T)serializer.Deserialize(reader);
            }
            return type;
        }
    }
}
