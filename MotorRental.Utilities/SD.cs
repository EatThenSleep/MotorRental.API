using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Utilities
{
    public static class SD
    {
        public const int Xe_So = 1;
        public const int Xe_Tay_Ga = 2;
        public const int Xe_Tay_Con = 3;

        public const int Status_Enable = 1;
        public const int Status_Busy = 2;
        public const int Status_Maintaint = 3;

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
