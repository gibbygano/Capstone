namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Enum to hold results for Archiving
    /// Pat Banks 
    /// </summary>
    /// <remarks>
    /// </remarks>
    public enum ResultsArchive
    {
        //item could not be found
        NotFound = 0,

        //worked
        Success,

        //cannot archive if in future
        CannotArchive,

        //concurrency error
        ChangedByOtherUser,

        //pasts tests and can archive
        OkToArchive
    }
}