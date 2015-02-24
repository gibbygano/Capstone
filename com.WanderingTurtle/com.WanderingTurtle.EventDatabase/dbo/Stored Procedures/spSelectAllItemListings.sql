CREATE PROCEDURE spSelectAllItemListings
AS
	SELECT StartDate, EndDate, ItemListID, EventItemID, Price, QuantityOffered, ProductSize, SupplierID
	FROM ItemListing
	WHERE Active = 1