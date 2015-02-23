CREATE PROCEDURE spDeleteSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255) )
AS
	DELETE FROM SupplierFeedbackRecord
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @Rating
		AND Notes = @Notes
	RETURN @@ROWCOUNT