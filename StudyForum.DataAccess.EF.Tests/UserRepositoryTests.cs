using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyForum.DataAccess.Core.Models;
using StudyForum.DataAccess.Services;
using StudyForum.Db.EF;

namespace StudyForum.DataAccess.EF.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            var context = new ApplicationDbContext();
            var mapper = TestUtils.CreateMapper();
            var userService = new UserService(context, mapper);
            var userModel = new CreateUserModel
            {
                Email = "user@test.ru",
                FirstName = "Ivan",
                GroupId = Guid.NewGuid(),
                Password = "password",
                Patronymic = "Ivanovich",
                SecondName = "Ivanov"
            };

            var task = userService.CreateUserAsync(userModel);
            task.Wait();
            var result = task.Result;

            Assert.AreNotEqual(result, Guid.Empty);
        }

        [TestMethod]
        public void GetUserAsyncTest()
        {

        }
    }
}
