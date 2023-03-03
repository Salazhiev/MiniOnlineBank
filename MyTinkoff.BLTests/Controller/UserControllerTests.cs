namespace MyTinkoff.BL.Controller.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void UserControllerTest()
        {
            // Arrange
            var name = Guid.NewGuid().ToString();
            var lastName = Guid.NewGuid().ToString();
            var firstName = Guid.NewGuid().ToString();
            var dateTime = DateTime.Now;
            var user = new User(name, lastName, firstName, dateTime);


            // Act
            var loadUserController = new UserController(user);


            // Assert
            Assert.AreEqual(loadUserController.TheGapUser.Name, name);
            Assert.AreEqual(loadUserController.TheGapUser.FirstName, firstName);
            Assert.AreEqual(loadUserController.TheGapUser.LastName, lastName);
        }
    }
}