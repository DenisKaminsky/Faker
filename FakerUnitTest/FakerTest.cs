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
        private MyTestClass result;

        [TestInitialize]
        public void SetUp()
        {
            _faker = new FackerProgram.Faker();
            _faker.DTOAdd(typeof(Foo));
            _faker.DTOAdd(typeof(Bar));
            result = _faker.Create<MyTestClass>();
        }

        [TestMethod]
        public void ShortGeneratorTest()
        {
            Assert.IsTrue(result._fieldShort >= short.MinValue && result._fieldShort <= short.MaxValue);
        }

        [TestMethod]
        public void IntGeneratorTest()
        {
            Assert.IsTrue(result._fieldInt >= int.MinValue && result._fieldInt <= int.MaxValue);
        }

        [TestMethod]
        public void LongGeneratorTest()
        {
            Assert.IsTrue(result._fieldLong >= long.MinValue && result._fieldLong <= long.MaxValue);
        }
        //
        [TestMethod]
        public void UShortGeneratorTest()
        {
            Assert.IsTrue(result._fieldUShort >= ushort.MinValue && result._fieldUShort <= ushort.MaxValue);
        }

        [TestMethod]
        public void UIntGeneratorTest()
        {
            Assert.IsTrue(result._fieldUInt >= uint.MinValue && result._fieldUInt <= uint.MaxValue);
        }

        [TestMethod]
        public void ULongGeneratorTest()
        {
            Assert.IsTrue(result._fieldULong >= ulong.MinValue && result._fieldULong <= ulong.MaxValue);
        }
        //
        [TestMethod]
        public void DoubleGeneratorTest()
        {
            Assert.IsTrue(result._fieldDouble >= double.MinValue && result._fieldDouble <= double.MaxValue);
        }

        [TestMethod]
        public void FloatGeneratorTest()
        {
            Assert.IsTrue(result._fieldFloat >= float.MinValue && result._fieldFloat <= float.MaxValue);
        }
        //
        [TestMethod]
        public void ByteGeneratorTest()
        {
            Assert.IsTrue(result._fieldByte >= byte.MinValue && result._fieldByte <= byte.MaxValue);
        }

        [TestMethod]
        public void SByteGeneratorTest()
        {
            Assert.IsTrue(result._fieldSByte >= sbyte.MinValue && result._fieldSByte <= sbyte.MaxValue);
        }
        //
        [TestMethod]
        public void BoolGeneratorTest()
        {
            Assert.IsTrue(result._fieldBool == true || result._fieldBool == false);
        }

        [TestMethod]
        public void CharGeneratorTest()
        {
            Assert.IsTrue((result.propertyChar >= char.MinValue) && (result.propertyChar <= char.MaxValue) );
        }

        [TestMethod]
        public void StringGeneratorTest()
        {
            Assert.IsTrue(result._fieldString != null && result._fieldString.Length != 0);
        }

        [TestMethod]
        public void ObjectGeneratorTest()
        {
            Assert.IsTrue(result._fieldObject != null);
        }

        [TestMethod]
        public void DateTimeGeneratorTest()
        {
            Assert.IsTrue(result._fieldDate != null);
        }

        [TestMethod]
        public void NestingTest()
        {
            Assert.IsTrue(result._foo != null);
            Assert.IsTrue(result._foo._fieldBar != null);
            Assert.IsTrue(result._bar != null);
            Assert.IsTrue(result._bar._fieldFoo != null);
        }

        [TestMethod]
        public void CycleDependencyTest()
        {
            Assert.IsTrue(result._foo._fieldTest == null);
            Assert.IsTrue(result._foo._fieldBar._fieldFoo == null);
            Assert.IsTrue(result._bar._fieldFoo._fieldTest == null);
            Assert.IsTrue(result._bar._fieldFoo._fieldBar == null);
        }


    }
}
