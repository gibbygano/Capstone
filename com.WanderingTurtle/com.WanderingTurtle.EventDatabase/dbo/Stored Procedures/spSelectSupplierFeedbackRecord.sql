CREATE PROCEDURE spSelectSupplierFeedbackRecord(@RatingID int)
AS
	SELECT RatingID, SupplierID, EmployeeID, Rating, Notes
	FROM SupplierFeedbackRecord
	WHERE RatingID = @RatingID