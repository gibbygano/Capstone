CREATE PROCEDURE spSelectSupplier(@SupplierID int)
AS
	SELECT *
	FROM Supplier
	WHERE Active = 1
		AND SupplierID = @SupplierID
	RETURN @@ROWCOUNT