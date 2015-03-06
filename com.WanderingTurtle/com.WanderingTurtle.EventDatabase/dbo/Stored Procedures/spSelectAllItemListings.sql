CREATE PROCEDURE spSelectAllItemListings
AS
	SELECT StartDate, EndDate, ItemListID, EventItemID, Price, QuantityOffered, ProductSize, SupplierID, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests
	FROM ItemListing
	WHERE Active = 1