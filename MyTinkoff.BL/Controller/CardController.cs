namespace MyTinkoff.BL.Controller
{
    using MyTinkoff.BL.Model;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Класс контроллера карты.
    /// </summary>
    public class CardController : ControllerBase, ILS
    {
        /// <summary>
        /// Все карты с сохраненных данных.
        /// </summary>
        public List<Card> Cards;

        /// <summary>
        /// Карта с которой работают.
        /// </summary>
        public Card Card;

        /// <summary>
        /// Класс карты для переводов.
        /// </summary>
        public Card CardTransition;

        /// <summary>
        /// Получить карту с которой работают.
        /// </summary>
        /// <param name="card"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CardController(Card card)
        {
            if(card == null)
                throw new ArgumentNullException("Карта не может быть null",nameof(card));

            Card = card;
        }

        /// <summary>
        /// Сохранить в данные новую карту
        /// </summary>
        public void Add() // Добавить новую карту
        {
            Cards = GetAll<Card>();
            Cards.Add(Card);
            SaveAll(Cards);
        }


        /// <summary>
        /// Проверка на существование карты.
        /// </summary>
        /// <returns> Если она существует она присваивается к "Карта с которой работают </returns>
        public bool Enter()// Посмотреть есть ли карта
        {
            Cards = GetAll<Card>();
            foreach (Card cardd in Cards)
                if (Card.NumberCards == cardd.NumberCards && Card.Password == cardd.Password)
                {
                    Card = cardd;
                    return true;
                }
            return false;
        }


        /// <summary>
        /// Проверка на существование карты.
        /// </summary>
        /// <param name="number"> Номер карты которую проверяют  </param>
        /// <returns> Присваивание к "Класс карты для переводов" </returns>
        public bool Enter(string number)
        {
            Cards = GetAll<Card>();
            foreach(Card card in Cards)
                if(card.NumberCards == number)
                {
                    CardTransition = card;
                    return true;
                }
            return false;
        }


        /// <summary>
        /// Добавление денег на счет карты.
        /// </summary>
        /// <param name="a"> Количество денег </param>
        public void AddMoney(int a)
        {
            Card.MoneyOTC += a;
            SaveAll(Cards);
        }


        /// <summary>
        /// Сделать перевод на другого пользователя.
        /// </summary>
        /// <param name="theAmount"></param>
        /// <returns></returns>
        public bool MoneyTransition(int theAmount)
        {
            if(theAmount <= Card.MoneyOTC)
            {
                Card.MoneyOTC -= theAmount;
                CardTransition.MoneyOTC += theAmount;
                SaveAll(Cards);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Получить с сохраненных данных все данные карт
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>()
        {
            return Load<T>() ?? new List<T>();
        }


        /// <summary>
        /// Сохранить новые данные с картами
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void SaveAll<T>(List<T> obj)
        {
            Save(obj);
        }


        /// <summary>
        /// Вывести данные карты.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"\n\n        Ваш пароль: {Card.Password}, ваш номер: {Card.NumberCards}, счет: {Card.MoneyOTC}";
    }
}