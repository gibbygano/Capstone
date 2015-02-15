CREATE PROCEDURE spSelectLists(@ItemListID int, @SupplierID int)
AS
	SELECT *
	FROM Lists
	WHERE SupplierID = @SupplierID
		AND ItemListID = @ItemListID
	RETURN @@ROWCOUNT