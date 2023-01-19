using System.ComponentModel;

namespace CrissCargoApp.Enum
{
    public enum OrderStatus
    {
        [Description("For Pending Order")]
        PendingOrder = 1,
        [Description("For precessing orders")]
        Processing  = 2,
        [Description("For awaiting payment")]
        AwaitingPayment = 3,
        [Description("For procuring order")]
        Procuring = 4,
        [Description("For awaiting delivery")]
        AwaitingDelivery  = 5,
        [Description("For sorting and packing orders")]
        SortingAndPacking  = 6,
        [Description("For shipped orders")]
        Shipped = 7, 
        [Description("For orders arrived Lagos")]
        ArrivedDestination = 8, 
        [Description("For picked up orders")]
        PickedUp = 9,
        [Description("For  completed orders")]
        Completed = 10,
    }

    public enum PaymentStatus
    {
        [Description("For Unpaid")]
        Unpaid = 1,
        [Description("For Paid")]
        Paid = 2,

    }


}
