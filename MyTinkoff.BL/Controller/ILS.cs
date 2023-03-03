namespace MyTinkoff.BL.Controller
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс который наследуется для всех классов, которым надо сохранять или получать данных
    /// </summary>
    interface ILS
    {
        /// <summary>
        /// Сохранить.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void SaveAll<T>(List<T> obj);
        /// <summary>
        /// Получить.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetAll<T>();
    }
}