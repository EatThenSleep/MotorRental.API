using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TestFilterAppointment
    {
        [TestMethod()]
        public void ProcessCreteriasTest1()
        {
            AppointmentFindCreterias creterias = new AppointmentFindCreterias();

            creterias.FilterStatusAppointment = SD.Status_Appointment_Process;
            creterias.FilterStatusPayment = SD.Status_Payment_Not;
            creterias.Skip = 5;
            creterias.Take = 5;

            var res = ValidationAppointment.ProcessCreterias(creterias);

            Assert.AreEqual(res.FilterStatusAppointment, SD.Status_Appointment_Process);
            Assert.AreEqual(res.FilterStatusPayment, SD.Status_Payment_Not);
            Assert.AreEqual(res.Skip, 5);
            Assert.AreEqual(res.Take, 5);

        }

        [TestMethod()]
        public void ProcessCreteriasTest2()
        {
            AppointmentFindCreterias creterias = new AppointmentFindCreterias();

            creterias.FilterStatusAppointment = 4;
            creterias.FilterStatusPayment = 3;
            creterias.Skip = 0;
            creterias.Take = 2;

            var res = ValidationAppointment.ProcessCreterias(creterias);

            Assert.AreEqual(res.FilterStatusAppointment, -1);
            Assert.AreEqual(res.FilterStatusPayment, -1);
            Assert.AreEqual(res.Skip, 0);
            Assert.AreEqual(res.Take, 2);

        }

        [TestMethod()]
        public void ProcessCreteriasTest3()
        {
            AppointmentFindCreterias creterias = new AppointmentFindCreterias();

            creterias.FilterStatusAppointment = SD.Status_Appointment_Process;
            creterias.FilterStatusPayment = SD.Status_Payment_Not;
            creterias.Skip = 0;
            creterias.Take = 2;

            var res = ValidationAppointment.ProcessCreterias(creterias);

            Assert.AreEqual(res.FilterStatusAppointment, SD.Status_Appointment_Process);
            Assert.AreEqual(res.FilterStatusPayment, SD.Status_Payment_Not);
            Assert.AreEqual(res.Skip, 0);
            Assert.AreEqual(res.Take, 2);

        }

        [TestMethod()]
        public void ProcessCreteriasTest4()
        {
            AppointmentFindCreterias creterias = new AppointmentFindCreterias();

            creterias.FilterStatusAppointment = SD.Status_Appointment_Accepted;
            creterias.FilterStatusPayment = 4;
            creterias.Skip = 0;
            creterias.Take = 2;

            var res = ValidationAppointment.ProcessCreterias(creterias);

            Assert.AreEqual(res.FilterStatusAppointment, SD.Status_Appointment_Accepted);
            Assert.AreEqual(res.FilterStatusPayment, SD.Status_Payment_Not);
            Assert.AreEqual(res.Skip, 0);
            Assert.AreEqual(res.Take, 2);

        }

        [TestMethod()]
        public void ProcessCreteriasTest5()
        {
            AppointmentFindCreterias creterias = new AppointmentFindCreterias();

            creterias.FilterStatusAppointment = SD.Status_Appointment_Cancel;
            creterias.FilterStatusPayment = -1;
            creterias.Skip = 0;
            creterias.Take = 2;

            var res = ValidationAppointment.ProcessCreterias(creterias);

            Assert.AreEqual(res.FilterStatusAppointment, SD.Status_Appointment_Cancel);
            Assert.AreEqual(res.FilterStatusPayment, SD.Status_Payment_Not);
            Assert.AreEqual(res.Skip, 0);
            Assert.AreEqual(res.Take, 2);

        }

       
    }
}