using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    public enum OrderStatus 
    {
        //added attributes to return the string value of the enum instead of the numeric values. 
        [EnumMember (Value = "Pending")]
        Pending, 
        [EnumMember (Value = "Payment Received")]
        PaymentReceived, 
        [EnumMember (Value = "Payment Failed")]
        PaymentFailed
    }
}