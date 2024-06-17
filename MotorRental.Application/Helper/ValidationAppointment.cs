using MotorRental.Entities;
using MotorRental.UseCase.Feature;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Helper
{
    public static class ValidationAppointment
    {
        public static AppointmentFindCreterias ProcessCreterias(AppointmentFindCreterias creterias)
        {
            if (creterias.FilterStatusAppointment != SD.Status_Appointment_Process &&
                 creterias.FilterStatusAppointment != SD.Status_Appointment_Accepted &&
                 creterias.FilterStatusAppointment != SD.Status_Appointment_Cancel &&
                 creterias.FilterStatusAppointment != SD.Status_Appointment_Done)
            {
                creterias.FilterStatusAppointment = -1;
                creterias.FilterStatusPayment = -1;
            }
            else
            {
                if(creterias.FilterStatusAppointment == SD.Status_Appointment_Process ||
                    creterias.FilterStatusAppointment == SD.Status_Appointment_Accepted ||
                    creterias.FilterStatusAppointment == SD.Status_Appointment_Cancel)
                {
                    creterias.FilterStatusPayment = SD.Status_Payment_Not;
                }
                else if(creterias.FilterStatusAppointment == SD.Status_Appointment_Done)
                {
                    creterias.FilterStatusPayment = SD.Status_Payment_Payed;
                }
                else
                {
                    creterias.FilterStatusPayment = -1;
                }
            }
            if (creterias.Skip < 0)
            {
                creterias.Skip = 0;
            }
            if (creterias.Take < 0 || creterias.Take > int.MaxValue)
            {
                creterias.Take = int.MaxValue;
            }

            return creterias;
        }

        public static TransactionResult checkAppointmentIsProcess(Appointment appointment, int statusNeed)
        {
            if (appointment == null)
            {
                return TransactionResult.NotBelong;
            }

            // check apointment is process
            if (appointment.StatusAppointment != statusNeed)
            {
                return TransactionResult.Error;
            }

            return TransactionResult.Success;
        }

    }
}
