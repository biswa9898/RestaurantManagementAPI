using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RestaurantManagement.Controllers;
using RestaurantManagement.Models;
using RestaurantManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantTest
{
    public class FoodTest
    {
        DemoContext db = new DemoContext();
        [SetUp]
        public void Setup()
        {
            var Foods = new List<Food>
            {
                new Food{Id = 1, Name = "Mixed Fried Rice" , Price = 120, Type = "Non Veg"},
                 new Food{Id = 2,Name = "Veg Fried Rice" , Price = 80, Type = "Veg"},
                  new Food{Id = 3, Name = "Chicken Fried Rice" , Price = 10, Type = "Non Veg"}

            };
            var Foodsdata = Foods.AsQueryable();
            var mockSet = new Mock<DbSet<Food>>();
            mockSet.As<IQueryable<Food>>().Setup(m => m.Provider).Returns(Foodsdata.Provider);
            mockSet.As<IQueryable<Food>>().Setup(m => m.Expression).Returns(Foodsdata.Expression);
            mockSet.As<IQueryable<Food>>().Setup(m => m.ElementType).Returns(Foodsdata.ElementType);
            mockSet.As<IQueryable<Food>>().Setup(m => m.GetEnumerator()).Returns(Foodsdata.GetEnumerator());

            var mockContext = new Mock<DemoContext>();

            mockContext.Setup(c => c.Foods).Returns(mockSet.Object);

            db = mockContext.Object;


        }

        [Test]

        public void GetFoods()
        {
            var repo = new Mock<FoodRep>(db);
            FoodController controller = new FoodController(repo.Object);
            var data = controller.GetFoods();
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]

        public void GetByFoodIdPositive()
        {
            var repo = new Mock<FoodRep>(db);
            FoodController controller = new FoodController(repo.Object);
            var data = controller.GetFood(1);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }



        [Test]

        public void GetByFoodIdNegative()

        {
            var repo = new Mock<FoodRep>(db);
            FoodController controller = new FoodController(repo.Object);
            var data = controller.GetFood(52);
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);

        }

        [Test]

        public void PostUser()
        {
            var repo = new Mock<FoodRep>(db);
            FoodController controller = new FoodController(repo.Object);
            Food food = new Food { Id = 4,Name = "Roti", Price = 20, Type = "Veg"};
            var data = controller.Post(food);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }


       
        [Test]

        public void DeleteUserPositive()
        {
            var repo = new Mock<FoodRep>(db);
            FoodController controller = new FoodController(repo.Object);
            var data = controller.Delete(2);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteUserNegative()
        {
            var repo = new Mock<FoodRep>(db);
            FoodController controller = new FoodController(repo.Object);
            var data = controller.Delete(25);
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);



        }


    }
}
    

