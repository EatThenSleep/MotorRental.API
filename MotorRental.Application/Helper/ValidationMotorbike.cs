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
    public static class ValidationMotorbike
    {
        public static bool CheckMotorbikeFree(Motorbike existingMobike)
        {
            if (existingMobike == null) return false;
            return existingMobike.status == SD.Status_Enable;
        }
        public static bool CheckIformationInvalid(User existingUser)
        {
            if (existingUser == null) return false;
            if (string.IsNullOrEmpty(existingUser.Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.PhoneNumber))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.IdentityNumber))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.IdentityImagePre))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.IdentityImageBack))
            {
                return false;
            }
            return true;
        }

        public static MotorbikeFindCreterias ProcessCreterias(MotorbikeFindCreterias creterias)
        {
            if (creterias.FilterStatus == 0
                && creterias.FilterType == 0
                && string.IsNullOrEmpty(creterias.Name)
                && string.IsNullOrEmpty(creterias.LicensePlate)
                && creterias.Skip == 0
                && creterias.Take == int.MaxValue)
            {
                return MotorbikeFindCreterias.Empty;
            }

            if (creterias.FilterStatus > 3 || creterias.FilterStatus < 1) creterias.FilterStatus = 0;
            if (creterias.FilterType > 3 || creterias.FilterType < 1) creterias.FilterType = 0;

            return creterias;
        }
    }
}
