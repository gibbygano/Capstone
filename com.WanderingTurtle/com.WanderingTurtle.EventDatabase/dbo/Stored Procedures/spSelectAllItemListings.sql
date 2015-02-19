CREATE PROCEDURE spSelectAllItemListings
AS
	SELECT StartDate, EndDate, ItemListID, EventItemID, Price, QuantityOffered, ProductSize
	FROM ItemListing
	WHERE Active = 1