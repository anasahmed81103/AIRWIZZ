namespace AIRWIZZ.Data.enums
{
    public enum PaymentStatus
    {
        Pending,        // Payment is yet to be made
        Successful,      // Payment has been successfully completed
        Failed,         // Payment attempt failed
        Refunded,       // Payment has been refunded
        Cancelled,      // Payment was cancelled
        PartiallyPaid,  // Payment is partially completed

    }
}
