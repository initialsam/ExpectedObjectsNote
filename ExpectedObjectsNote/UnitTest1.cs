using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpectedObjects;
using System.Collections.Generic;

namespace ExpectedObjectsNote
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 測試Order物件用MStest的Assert_Equals是會失敗的()
        {
            var expected = new Order
            {
                Id = 1,
                Price = 10,
            };

            var actual = new Order
            {
                Id = 1,
                Price = 10,
            };

            //Assert.AreEqual 失敗。
            //預期: < ExpectedObjectsNote.Order >。實際: < ExpectedObjectsNote.Order >。
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void 將物件用屬性一個一個比較是會成功()
        {
            var expected = new Order
            {
                Id = 1,
                Price = 10
            };

            var actual = new Order
            {
                Id = 1,
                Price = 10
            };

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Price, actual.Price);
            //但是非常不方便 屬性多的話就不實用了
        }

        [TestMethod]
        public void 匿名型別用AssertAreEqual是成功的()
        {

            var expectedAnonymous = new
            {
                Id = 1,
                Name = "A",
                Age = 10
            };

            var actualAnonymous = new
            {
                Id = 1,
                Name = "A",
                Age = 10
            };
            //91說 兩個相同匿名型別的 instance 只有在其所有 property 都相等時，才代表相等。
            Assert.AreEqual(expectedAnonymous, actualAnonymous);
        }

        [TestMethod]
        public void 用ExpectedObjects套件就可以比較兩個物件()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            expected.ToExpectedObject().ShouldEqual(actual);

            //假設actual的Age是11
            //得到的錯誤訊息
            //For Person.Age, expected[10] but found[11].



        }

        [TestMethod]
        public void 用ExpectedObjects套件就可以比較兩個物件集合()
        {
            var expected = new List<Person>
            {
                new Person { Id=1, Name="A",Age=10},
                new Person { Id=2, Name="B",Age=20},
                new Person { Id=3, Name="C",Age=30},
            };

            var actual = new List<Person>
            {
                new Person { Id=1, Name="A",Age=10},
                new Person { Id=2, Name="B",Age=20},
                new Person { Id=3, Name="Z",Age=30},
            };

            expected.ToExpectedObject().ShouldEqual(actual);
            //假設actual Id=3 Name是Z
            //得到的錯誤訊息
            //For List`1[2].Name, expected "C" but found "Z".
        }

        [TestMethod]
        public void 用ExpectedObjects套件就可以比較兩個物件包物件()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
                Order = new Order { Id = 91, Price = 910 },
            };

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
                Order = new Order { Id = 91, Price = 910 },
            };

            expected.ToExpectedObject().ShouldEqual(actual);
            //假設actual Price是919
            //得到的錯誤訊息
            //For Person.Order.Price, expected[910] but found[919].
        }
    }

    public class Person
    {
        public Order Order;

        public int Age { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int Price { get; set; }
    }
}
