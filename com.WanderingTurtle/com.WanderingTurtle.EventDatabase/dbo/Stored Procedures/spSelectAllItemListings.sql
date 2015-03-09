CREATE PROCEDURE spSelectAllItemListings
AS
	SELECT StartDate, EndDate, ItemListID, ItemListing.EventItemID, Price, ItemListing.SupplierID, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests, EventItem.EventItemName, Supplier.CompanyName
        FROM ItemListing
                LEFT JOIN EventItem
                        ON ItemListing.EventItemID = EventItem.EventItemID
                LEFT JOIN Supplier
                        ON ItemListing.SupplierID = Supplier.SupplierID
        WHERE ItemListing.Active = 1
