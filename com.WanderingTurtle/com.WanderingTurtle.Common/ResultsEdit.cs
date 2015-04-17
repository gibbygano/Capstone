namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Enum to hold results for editing
    /// Pat Banks
    /// </summary>
    /// <remarks>
    /// </remarks>
    public enum ResultsEdit
    {
        //item could not be found
        NotFound = 0,

        //worked
        Success,

        //cannot change something that has already happened
        CannotEditTooOld,

        //Can change record
        OkToEdit,

        //concurrency error
        ChangedByOtherUser,

        //already cancelled
        Cancelled,

        QuantityZero,
        DatabaseError,
        ListingFull
    }
}