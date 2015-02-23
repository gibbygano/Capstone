CREATE PROCEDURE spInsertSupplierFeedbackRecord( 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255) )
AS
	INSERT INTO SupplierFeedbackRecord(SupplierID, EmployeeID, Rating, Notes)
		VALUES (@SupplierID, @EmployeeID, @Rating, @Notes)
	RETURN @@ROWCOUNT