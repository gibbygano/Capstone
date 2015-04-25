/**************************created by: Tony Noel- ********************************************/
CREATE PROCEDURE [dbo].[spSelectItemListingDetailsList]
AS
BEGIN
	SELECT  ItemListing.ItemListID, ItemListing.MaxNumberOfGuests, ItemListing.CurrentNumberOfGuests, ItemListing.StartDate, ItemListing.EndDate, ItemListing.EventItemID, EventItem.EventItemName, EventItem.EventDescription, ItemListing.Price
	FROM ItemListing, EventItem
	WHERE ItemListing.EventItemID = EventItem.EventItemID
	AND ItemListing.StartDate > CURRENT_TIMESTAMP
END
