CREATE PROCEDURE spSelectLists(@ItemListID int, @SupplierID int)
AS
	SELECT SupplierID, ItemListID, DateListed
	FROM Lists
	WHERE SupplierID = @SupplierID
		AND ItemListID = @ItemListID
	RETURN @@ROWCOUNT