CREATE PROCEDURE spSelectItemListing(@ItemListID int)
AS
	SELECT StartDate, EndDate, ItemListID, EventItemID, Price, QuantityOffered, ProductSize, SupplierID, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests
	FROM ItemListing
	WHERE ItemListID = @ItemListID
	AND Active = 1
	RETURN @@ROWCOUNT