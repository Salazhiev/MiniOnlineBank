namespace MyTinkoff.BL.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Класс для управления пользователями
    /// </summary>
    public class UserController : ControllerBase, ILS
    {
        /// <summary>
        /// Данные пользователей.
        /// </summary>
        public List<User> Users { get; }

        /// <summary>
        /// Пользователь с которым идет работа на данный момент
        /// </summary>
        public User TheGapUser { get; }
        
        /// <summary>
        /// Вспомогательный флаг.
        /// </summary>
        public bool Flag { get; set; } = false;

        /// <summary>
        /// Конструктор,основная ф-я присвоить TheGapUser пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public UserController(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Пользователь не может быть null", nameof(user));
            Users = GetAll<User>();

            //Получить пользователя с такими данными если он есть.
            TheGapUser = Users.SingleOrDefault(t => t.Name == user.Name & t.LastName == user.LastName & t.FirstName == user.FirstName & t.Age == user.Age);


            if(TheGapUser == null)
            {
                TheGapUser = user;
                Users.Add(user);
                Flag = true;
                SaveAll(Users);
            }
        }

        /// <summary>
        /// Сохранить.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void SaveAll<T>(List<T> obj)
        {
            Save(obj);
        }

        /// <summary>
        /// Получить
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>()
        {
            return Load<T>() ?? new List<T>();
        }
    }
}