namespace MyTinkoff.BL.Controller.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTinkoff.BL.Model;
    using System;

    [TestClass()]
    public class CardControllerTests
    {
        Random rnd = new Random();

        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var firstName = Guid.NewGuid().ToString();
            var dateTime = DateTime.Now;
            var user = new User(name, lastName, firstName, dateTime);

            var numberCards = rnd.Next(1000,10000)+" "+rnd.Next(1000, 10000)+" "+rnd.Next(1000, 10000)+" "+rnd.Next(1000, 10000);
            var password = rnd.Next(10000000,100000000).ToString();
            var card = new Card(numberCards, password, user);

            var CardController = new CardController(card);
            CardController.Add();


            // Act
            var newCardController = new CardController(card);


            // Assert
            Assert.AreEqual(name,newCardController.Card.User.Name);
            Assert.AreEqual(lastName,newCardController.Card.User.LastName);
            Assert.AreEqual(firstName,newCardController.Card.User.FirstName);
            Assert.AreEqual(numberCards, newCardController.Card.NumberCards);
            Assert.AreEqual(password,newCardController.Card.Password);
        }
    }
}