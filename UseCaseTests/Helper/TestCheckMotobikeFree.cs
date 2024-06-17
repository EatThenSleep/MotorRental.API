using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotorRental.Entities;
using MotorRental.UseCase.Feature;
using MotorRental.UseCase.Helper;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Helper.Tests
{
    [TestClass()]
    public class TestCheckMotobikeFree
    {

        // test objective : null
        [TestMethod()]
        public void CheckMotobikeFreeTest1()
        {
            Motorbike input1 = null;
            var res = ValidationMotorbike.CheckMotorbikeFree(input1);

            Assert.AreEqual(res, false);
        }

        // test objective : not null
        [TestMethod()]
        public void CheckMotobikeFreeTest2()
        {
            Motorbike input1 = new Motorbike();
            var res = ValidationMotorbike.CheckMotorbikeFree(input1);

            Assert.AreEqual(res, input1.status == SD.Status_Enable);
        }


        // test objective : existingUser null
        [TestMethod()]
        public void CheckIformattionInvalidTest1()
        {

            User input1 = null;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }

        // test objective : name null
        [TestMethod()]
        public void CheckIformattionInvalidTest2()
        {
            User input1 = new User();
            input1.Name = string.Empty;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }
        // test objective : Email null
        [TestMethod()]
        public void CheckIformattionInvalidTest7()
        {
            User input1 = new User();
            input1.Name = "test";
            input1.Email = string.Empty;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }

        // test objective :  phoneNumber null
        [TestMethod()]
        public void CheckIformattionInvalidTest3()
        {
            User input1 = new User();
            input1.Name = "test";
            input1.Email = "test@gmail.com";
            input1.PhoneNumber = string.Empty;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }

        // test objective :  IdentityNumber null
        [TestMethod()]
        public void CheckIformattionInvalidTest4()
        {
            User input1 = new User();
            input1.Name = "test";
            input1.Email = "test@gmail.com";
            input1.PhoneNumber = "012345678";
            input1.IdentityNumber = string.Empty;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }

        // test objective :  IdentityImagePre null
        [TestMethod()]
        public void CheckIformattionInvalidTest5()
        {
            User input1 = new User();
            input1.Name = "test";
            input1.Email = "test@gmail.com";
            input1.PhoneNumber = "012345678";
            input1.IdentityNumber = "12345678";
            input1.IdentityImagePre = string.Empty;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }

        // test objective :  IdentityImageBack null
        [TestMethod()]
        public void CheckIformattionInvalidTest6()
        {
            User input1 = new User();
            input1.Name = "test";
            input1.Email = "test@gmail.com";
            input1.PhoneNumber = "012345678";
            input1.IdentityNumber = "12345678";
            input1.IdentityImagePre = "testImagePre";
            input1.IdentityImageBack = string.Empty;
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, false);
        }

        // test objective :  check form Invalid null
        [TestMethod()]
        public void CheckIformattionInvalidTest8()
        {
            User input1 = new User();
            input1.Name = "test";
            input1.Email = "test@gmail.com";
            input1.PhoneNumber = "012345678";
            input1.IdentityNumber = "12345678";
            input1.IdentityImagePre = "testImagePre";
            input1.IdentityImageBack = "testImageBack";
            var res = ValidationMotorbike.CheckIformationInvalid(input1);
            Assert.AreEqual(res, true);
        }

        // test objective  : FilterStatus
        [TestMethod()]
        public void ProcessCreteriasTest1()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();

            input.FilterStatus = 0;
            input.FilterType = 0;
            input.Name = string.Empty;
            input.LicensePlate = string.Empty;
            input.Skip = 0;
            input.Take = int.MaxValue;

            var res = ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(input.FilterStatus , MotorbikeFindCreterias.Empty.FilterStatus );
            Assert.AreEqual(input.FilterType, MotorbikeFindCreterias.Empty.FilterType);
            Assert.AreEqual(input.Name, MotorbikeFindCreterias.Empty.Name);
            Assert.AreEqual(input.LicensePlate, MotorbikeFindCreterias.Empty.LicensePlate);
            Assert.AreEqual(input.Skip, MotorbikeFindCreterias.Empty.Skip);
            Assert.AreEqual(input.Take, MotorbikeFindCreterias.Empty.Take);
        }



        // test objective  :  FilterStatus > 3
        [TestMethod()]
        public void ProcessCreteriasTest2()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();
            input.FilterType = 5;
          var res = ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(0, input.FilterStatus = 0);
        }

        // test objective  :  FilterStatus < 1
        [TestMethod()]
        public void ProcessCreteriasTest3()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();
            input.FilterType = -1;
            ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(0, input.FilterStatus = 0);
        }

        // test objective  :  FilterType < 3
        [TestMethod()]
        public void ProcessCreteriasTest4()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();
            input.FilterType = 5;
            ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(0, input.FilterType = 0);
        }
        // test objective  :  FilterType < 1
        [TestMethod()]
        public void ProcessCreteriasTest5()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();
            input.FilterType = -1;
            ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(0, input.FilterType = 0);
        }

        // test objective  : 1 <  FilterStatus < 3
        [TestMethod()]
        public void ProcessCreteriasTest6()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();
            input.FilterType = 2;
            var res = ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(res, input);
        }


        // test objective  : 1 <  FilterType < 3
        [TestMethod()]
        public void ProcessCreteriasTest7()
        {
            MotorbikeFindCreterias input = new MotorbikeFindCreterias();
            input.FilterType = 2;
            var res = ValidationMotorbike.ProcessCreterias(input);
            Assert.AreEqual(res, input);
        }
    }
}