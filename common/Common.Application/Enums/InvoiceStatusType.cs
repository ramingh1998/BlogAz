using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Application.Enums
{
    public enum InvoiceStatusType
    {
        //=============================================
        /// <summary>
        /// ارسال به درگاه
        /// </summary>
        SendToGateWay = 1,
        //=============================================
        /// <summary>
        /// پرداخت شده
        /// </summary>
        SuccessPayment = 2,
        //=============================================
        /// <summary>
        /// پرداخت ناموفق
        /// </summary>
        UnSuccessPayment = 3,
        //=============================================
    }
}
