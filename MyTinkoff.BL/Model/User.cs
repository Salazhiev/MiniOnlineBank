namespace MyTinkoff.BL
{
    using System;

    /// <summary>
    /// Класс пользователя
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество.
        /// </summary>
        public string FirstName{ get; set; }
        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime Age { get; set; }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="age"></param>
        /// <exception cref="Exception"></exception>
        public User(string name, string lastName, string firstName, DateTime age)
        {
            if (age == null) throw new Exception("Возраст должен быть больше нуля");
            CheckNull(name, "Имя");
            CheckNull(lastName, "Фамилия");
            CheckNull(firstName, "Отчество");
            
            Name = name;
            LastName = lastName;
            FirstName = firstName;
            Age = age;
        }

        /// <summary>
        /// Проверка на null.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="checkName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void CheckNull(string name, string checkName)
        {
            if(name == null || name == "")
                throw new ArgumentNullException($"{checkName} Не может быть null", nameof(name));
        }

        /// <summary>
        /// Данные о пользователе
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Имя:{Name} Фамилия:{LastName} Отчество:{FirstName} Возраст:{Age}";
    }
}