CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255), 
	@originalRating int, 
	@originalNotes varchar(255) )
AS
	UPDATE SupplierFeedbackRecord
	SET
		Rating = @Rating,
		Notes = @Notes
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @originalRating
		AND Nots = @originalNotes
	RETURN @@ROWCOUNT