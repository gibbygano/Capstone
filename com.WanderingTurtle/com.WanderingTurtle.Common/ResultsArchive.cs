namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Pat Banks
    /// Created: 2015/03/23
    /// 
    /// Enum to hold results for Archiving
    /// </summary>
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