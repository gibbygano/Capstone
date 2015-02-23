CREATE PROCEDURE spSelectAllSupplierFeedbackRecords
AS
	SELECT RatingID, SupplierID, EmployeeID, Rating, Notes
	FROM SupplierFeedbackRecord