CREATE PROCEDURE spUpdateLists(@ItemListID int, @SupplierID int, @DateListed date, @originalDateListed date)
AS
	UPDATE Lists
	SET
		DateListed = @DateListed
	WHERE
		ItemListID = @ItemListID AND
		SupplierID = @SupplierID AND
		DateListed = @originalDateListed
	RETURN @@ROWCOUNT