/**************************created by: Pat Banks ********************************************/

CREATE PROCEDURE [dbo].[spSelectOneListingFull]
 (@itemListID  INT)
AS

BEGIN
	SELECT  ItemListing.ItemListID, ItemListing.MaxNumberOfGuests, ItemListing.CurrentNumberOfGuests, ItemListing.StartDate, ItemListing.EndDate, ItemListing.EventItemID, EventItem.EventItemName, EventItem.EventDescription, ItemListing.Price
	FROM ItemListing, EventItem
	WHERE ItemListing.ItemListID = @itemListID 
	AND ItemListing.EventItemID = EventItem.EventItemID
END