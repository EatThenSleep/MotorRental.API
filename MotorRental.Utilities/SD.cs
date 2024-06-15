using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Utilities
{
    public static class SD
    {
        public static int Xe_So = 1;
        public static int Xe_Tay_Ga = 2;
        public static int Xe_Tay_Con = 3;

        public static int Status_Enable = 1;
        public static int Status_Busy = 2;
        public static int Status_Maintain = 3;

        public static int Status_Appointment_Process = 0;
        public static int Status_Appointment_Accepted = 1;
        public static int Status_Appointment_Cancel = 2;
        public static int Status_Appointment_Done = 3;

        public static int Status_Payment_Not = 0;
        public static int Status_Payment_Payed = 1;

        public static readonly string[] RoleForUser = ["Owner", "Visitor"];

        public static readonly string VISTOR = "Visitor";
        public static readonly string ADMIN = "Admin";
        public static readonly string OWNER = "Owner";

        public static readonly string Payment_Stripe = "Stripe";


        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }

        public enum ContentType
        {
            Json,
            MultipartFormData
        }
    }
}
