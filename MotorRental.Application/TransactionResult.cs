using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public class TransactionResult
    {
        public bool isSucess  { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public static readonly TransactionResult InforUserInvalid = new TransactionResult { isSucess = false, ErrorMessage = "Information of user Invalid" };  
        public static readonly TransactionResult MotorbikeCanNotUseNow = new TransactionResult { isSucess = false, ErrorMessage = "Motorbike can not use now" };
        public static readonly TransactionResult Success = new TransactionResult { isSucess = true};
        public static readonly TransactionResult Error = new TransactionResult { isSucess = false, ErrorMessage = "Error happening, Please try again" };
    }
}
