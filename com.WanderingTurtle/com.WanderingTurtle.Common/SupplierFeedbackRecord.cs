namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Matt Lapka
    /// Created: 2015/02/27
    ///
    /// Class for the creation of Supplier Feedback Record Objects with set data fields
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>

    public class SupplierFeedbackRecord
    {
        public SupplierFeedbackRecord()
        {
        }

        public SupplierFeedbackRecord(int ratingID, int supplierID, int employeeID, int rating, string ratingNotes)
        {
            RatingID = ratingID;
            SupplierID = supplierID;
            EmployeeID = employeeID;
            Rating = rating;
            RatingNotes = ratingNotes;
        }

        public int EmployeeID { get; set; }

        public int Rating { get; set; }

        public int RatingID { get; set; }

        public string RatingNotes { get; set; }

        public int SupplierID { get; set; }
    }
}