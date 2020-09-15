using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RestaurantManagement.Controllers;
using RestaurantManagement.Models;
using RestaurantManagement.Repository;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantTest
{
    public class UserTest
    {
        DemoContext db = new DemoContext();
        [SetUp]
        public void Setup()
        {
            var Users = new List <Users>
            {
                 new Users{ FirstName="Biswarup",LastName="Mazumdar",Email="abc@gmail.com",Password="1232"},
                 new Users{FirstName="Prithwiman",LastName="Mazumdar",Email="def@gmail.com",Password="65843"},
                 new Users{FirstName="Subham",LastName="Debnath",Email="sdb@gmail.com",Password="gh63"}

      };

            var Usersdata = Users.AsQueryable();
            var mockSet = new Mock<DbSet<Users>>();
            mockSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(Usersdata.Provider);
            mockSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(Usersdata.Expression);
            mockSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(Usersdata.ElementType);
            mockSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(Usersdata.GetEnumerator());

            var mockContext = new Mock<DemoContext>();

            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            db = mockContext.Object;

        }

        [Test]

        public void GetUsers()
        {
           var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            var data = controller.GetUsers();
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]

        public void GetByEmailPositive()
        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            var data = controller.GetUser("abc@gmail.com");
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }



        [Test]

        public void GetByEmailNegative()

        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            var data = controller.GetUser("ghjgh@gmail.com");
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);

        }

        [Test]

        public void PostUser()
        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            Users user = new Users { FirstName = "Dishani", LastName = "Das", Email = "yth@gmail.com", Password = "632" };
            var data = controller.Post(user);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }
        [Test]

        public void PutUserPositive()
        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            Users user = new Users {FirstName = "Prithwi", LastName = "Mazumdar"};
            var data = controller.Put("def@gmail.com", user);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]

        public void PutUserNegative()
        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            Users user = new Users { FirstName = "Dishani", LastName = "Das" };
            var data = controller.Put("yth@gmail.com", user);
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);

        }

        [Test]

        public void DeleteUserPositive()
        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            var data = controller.Delete("abc@gmail.com");
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteUserNegative()
        {
            var repo = new Mock<UsersRep>(db);
            UsersController controller = new UsersController(repo.Object);
            var data = controller.Delete("fghj@gmail.com");
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);


        }


    }
}