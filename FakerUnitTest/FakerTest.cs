using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FackerProgram;
using Faker;

namespace FakerUnitTest
{
    [TestClass]
    public class FakerTest
    {
        private FackerProgram.Faker _faker;
        private MyTestClass test;

        [TestInitialize]
        public void SetUp()
        {
            _faker = new FackerProgram.Faker();
            _faker.DTOAdd(typeof(Foo));
            _faker.DTOAdd(typeof(Bar));
            test = _faker.Create<MyTestClass>();
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
