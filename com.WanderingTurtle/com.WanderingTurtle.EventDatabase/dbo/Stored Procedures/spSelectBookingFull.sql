/**************************created by: Tony Noel- not all tables created yet********************************************/
CREATE PROCEDURE [dbo].[spSelectBookingFull]
AS
BEGIN
	SELECT  ItemListing.ItemListID, ItemListing.MaxNumberOfGuests, ItemListing.CurrentNumberOfGuests, ItemListing.StartDate, ItemListing.EndDate, ItemListing.EventItemID, EventItem.EventItemName, EventItem.EventDescription, ItemListing.Price
	FROM ItemListing, EventItem
	WHERE ItemListing.EventItemID = EventItem.EventItemID
END

	RETURN @@ROWCOUNT