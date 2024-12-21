using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Common.Application.Objects
{
    public static class SystemCommonMessage
    {

        public static string LoginFaild { get; set; } = "نام کاربری یا رمز عبور صحیح نمی باشد";
        public static string InformationWasSuccessfullyRecorded { get; set; } = "اطلاعات با موفقیت ثبت شد";
        public static string OperationDoneSuccessfully { get; set; } = "عملیات با موفقیت انجام شد";
        public static string OperationStoppedByError { get; set; } = "عملیات با خطا متوقف شد";
        public static string InformationFetchedSuccessfully { get; set; } = "اطلاعات با موفقیت واکشی شد";
        public static string InformationWasSuccessfullyEdited { get; set; } = "اطلاعات با موفقیت ویرایش شد";
        public static string InformationWasSuccessfullyDeleted { get; set; } = "اطلاعات با موفقیت حذف شد";
        public static string InputDataIsIncorrect { get; set; } = "داده های ورودی صحیح نمی باشد";
        public static string DataWasNotFound { get; set; } = "داده مورد نظر یافت نشد";
        public static string NoAccessToThisSection { get; set; } = "شما مجوز دسترسی به این بخش را ندارید";
        public static string NotAllowedToPerformThisOperation { get; set; } = "مجاز به انجام این عملیات نمی باشید";
        public static string IdentifierIsNotValid { get; set; } = "شناسه ورودی معتبر نمی باشد";
        public static string UsernameNotRegisteredForOrder { get; set; } = "نام کاربری برای سفارش ثبت نشده است";
        public static string YourMinimumOrderDiscountMustBeToApply { get; set; } = "برای اعمال کد تخفیف حداقل سفارش شما باید # باشد";
        public static string OrderNotFound { get; set; } = "سفارش یافت نشد";
        public static string UnableToDeleteCoupenWhenOrderIsBlock { get; set; } = "سفارش در حالت پرداخت می باشد حذف کد تخفیف ممکن نیست";
        public static string CouponHasExpired { get; set; } = "زمان کد تخفیف منقضی شده است";
        public static string CopounHasAlreadyBeenUsed { get; set; } = "این کد تخفیف قبلا استفاده شده است";
        public static string CoupenIsValidForFirstOrder { get; set; } = "این کد تخفیف برای اولین سفارش معتبر است";
        public static string UnableToRegisterCoupenWhenOrderIsBlock { get; set; } = "سفارش در حالت پرداخت می باشد ثبت کد تخفیف ممکن نیست";
        public static string OrderIsEmpty { get; set; } = "سبد خرید خالی است";
        public static string CouponSuccessfullyRegistered { get; set; } = "کد تخفیف با موفقیت ثبت شد";
        public static string CouponSuccessfullyRemoved { get; set; } = "کد تخفیف با موفقیت حذف شد";
        public static string NotEnoughInventory { get; set; } = "موجودی کافی نیست";
        public static string InvoiceNotFound { get; set; } = "صورتحساب یافت نشد";
        public static string DataWasNotRecordedDueToAnError { get; set; } = "اطلاعات به علت خطا ثبت نشد";
        public static string AmountEnteredIsNotCorrect { get; set; } = "مبلغ وارد شده صحیح نمی باشد";
    }
}
