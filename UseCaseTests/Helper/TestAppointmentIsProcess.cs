using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotorRental.Entities;
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
    public class TestAppointmentIsProcess
    {
        // Test objective: null
        [TestMethod()]
        public void checkAppointmentIsProcessTest1()
        {
            Appointment input1 = null;
            int input2 = SD.Status_Appointment_Accepted;

            var res = ValidationAppointment.checkAppointmentIsProcess(input1, input2);

            Assert.AreEqual(res, TransactionResult.NotBelong);
        }

        // Test objective: not null
        [TestMethod()]
        public void checkAppointmentIsProcessTest2()
        {
            Appointment input1  = new Appointment() ;
            int input2 = SD.Status_Appointment_Accepted;

            var res = ValidationAppointment.checkAppointmentIsProcess(input1, input2);

            Assert.AreNotEqual(res, TransactionResult.NotBelong);
        }

        // Test objective: != statusneed
        [TestMethod()]
        public void checkAppointmentIsProcessTest3()
        {
            Appointment input1 = new Appointment();
            input1.StatusAppointment = SD.Status_Appointment_Accepted;
            int input2 = SD.Status_Appointment_Accepted;

            var res = ValidationAppointment.checkAppointmentIsProcess(input1, input2);

            Assert.AreNotEqual(res, TransactionResult.Error);
        }

        // Test objective: != statusneed
        [TestMethod()]
        public void checkAppointmentIsProcessTest4()
        {
            Appointment input1 = new Appointment();
            input1.StatusAppointment = SD.Status_Appointment_Accepted;
            int input2 = SD.Status_Appointment_Accepted;

            var res = ValidationAppointment.checkAppointmentIsProcess(input1, input2);

            Assert.AreEqual(res, TransactionResult.Success);
        }
    }
}