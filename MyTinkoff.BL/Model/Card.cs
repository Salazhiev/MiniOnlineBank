namespace MyTinkoff.BL.Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Класс карты.
    /// </summary>
    [Serializable] 
    public class Card
    {
        /// <summary>
        /// Номер карты #### #### #### ####.
        /// </summary>
        public string NumberCards { get; set; }
        /// <summary>
        /// Пароль карты ########.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Количество денег на счету.
        /// </summary>
        public int MoneyOTC { get; set; } = 0;
        /// <summary>
        /// Пользователь который имеет данную карту.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Создать карту для пользователя.
        /// </summary>
        /// <param name="numberCards"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Card(string numberCards,string password, User user)
        {
            if(string.IsNullOrWhiteSpace(numberCards))
                throw new ArgumentNullException("Номер карты не может быть null",nameof(numberCards));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Пароль карты не может быть null", nameof(password));
            if (user == null)
                throw new ArgumentNullException("Пароль карты не может быть null", nameof(password));

            NumberCards = numberCards;
            Password = password;
            User = user;
        }
        /// <summary>
        /// Посмотреть счет на карте.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"На счету: {MoneyOTC}";
    }
}