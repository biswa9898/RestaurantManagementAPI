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
    public class OrderTest
    {
        DemoContext db = new DemoContext();
        [SetUp]
        public void Setup()
        {
            var Orders = new List<Orders>
            {
                new Orders{Email = "abc@gmail.com" ,FoodId = 1, OrderId = 8},
                new Orders{Email = "def@gmail.com" ,FoodId = 2, OrderId = 9},
                new Orders{Email = "ghi@gmail.com" ,FoodId = 3, OrderId = 10}

            };
            var Ordersdata = Orders.AsQueryable();
            var mockSet = new Mock<DbSet<Orders>>();
            mockSet.As<IQueryable<Orders>>().Setup(m => m.Provider).Returns(Ordersdata.Provider);
            mockSet.As<IQueryable<Orders>>().Setup(m => m.Expression).Returns(Ordersdata.Expression);
            mockSet.As<IQueryable<Orders>>().Setup(m => m.ElementType).Returns(Ordersdata.ElementType);
            mockSet.As<IQueryable<Orders>>().Setup(m => m.GetEnumerator()).Returns(Ordersdata.GetEnumerator());

            var mockContext = new Mock<DemoContext>();

            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            db = mockContext.Object;


        }

        [Test]

        public void GetOrders()
        {
            var repo = new Mock<OrdersRep>(db);
            OrdersController controller = new OrdersController(repo.Object);
            var data = controller.GetOrders();
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }

        [Test]

        public void GetByOrderIdPositive()
        {
            var repo = new Mock<OrdersRep>(db);
            OrdersController controller = new OrdersController(repo.Object);
            var data = controller.GetOrder(9);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }



        [Test]

        public void GetByOrderIdNegative()

        {
            var repo = new Mock<OrdersRep>(db);
            OrdersController controller = new OrdersController(repo.Object);
            var data = controller.GetOrder(52);
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);

        }

        [Test]

        public void PostUser()
        {
            var repo = new Mock<OrdersRep>(db);
            OrdersController controller = new OrdersController(repo.Object);
            Orders order = new Orders { Email = "ijk@gmail.com", FoodId = 4, OrderId = 11 };
            var data = controller.Post(order);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);

        }



        [Test]

        public void DeleteUserPositive()
        {
            var repo = new Mock<OrdersRep>(db);
            OrdersController controller = new OrdersController(repo.Object);
            var data = controller.Delete(10);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void DeleteUserNegative()
        {
            var repo = new Mock<OrdersRep>(db);
            OrdersController controller = new OrdersController(repo.Object);
            var data = controller.Delete(66);
            var result = data as ObjectResult;
            Assert.AreEqual(400, result.StatusCode);



        }

    }

}

