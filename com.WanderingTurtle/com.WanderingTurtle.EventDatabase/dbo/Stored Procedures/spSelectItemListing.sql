CREATE PROCEDURE spSelectItemListing(@ItemListID int)
AS
	SELECT StartDate, EndDate, ItemListID, EventItemID, Price, QuantityOffered, ProductSize
	FROM ItemListing
	WHERE ItemListID = @ItemListID
	AND Active = 1
	RETURN @@ROWCOUNT