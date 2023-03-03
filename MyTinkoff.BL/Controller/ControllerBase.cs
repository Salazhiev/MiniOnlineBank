namespace MyTinkoff.BL.Controller
{
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Класс в котором реализуется сохранение и получение данных.
    /// </summary>
    public abstract class ControllerBase
    {
        /// <summary>
        /// Сохранить.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        protected void Save<T>(List<T> values)
        {
            var formatter = new BinaryFormatter();

            using (var file = new FileStream(typeof(T).Name + ".file.bin", FileMode.OpenOrCreate))
            {
                formatter.Serialize(file, values);
            }
        }

        /// <summary>
        /// Получить.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected List<T> Load<T>()
        {
            var formatter = new BinaryFormatter();

            using (var file = new FileStream(typeof(T).Name + ".file.bin", FileMode.OpenOrCreate))
            {
                if(file.Length > 0 && formatter.Deserialize(file) is List<T> items) return items;
                return null;
            }
        }
    }
}