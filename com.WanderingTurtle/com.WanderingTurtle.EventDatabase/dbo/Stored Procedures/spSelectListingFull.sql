/**************************created by: Tony Noel- ********************************************/
CREATE PROCEDURE [dbo].[spSelectListingFull]
(@Now	datetime)
AS
BEGIN
	SELECT  ItemListing.ItemListID, ItemListing.MaxNumberOfGuests, ItemListing.CurrentNumberOfGuests, ItemListing.StartDate, ItemListing.EndDate, ItemListing.EventItemID, EventItem.EventItemName, EventItem.EventDescription, ItemListing.Price
	FROM ItemListing, EventItem
	WHERE ItemListing.EventItemID = EventItem.EventItemID
	AND ItemListing.StartDate > @Now
END

	RETURN @@ROWCOUNT