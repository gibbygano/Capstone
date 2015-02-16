/**************************created by: Tony Noel- not all tables created yet********************************************/
CREATE PROCEDURE [dbo].[spSelectBookingFull]
AS
BEGIN
	SELECT  ItemListing.ItemListID, ItemListing.QuantityOffered, ItemListing.StartDate, ItemListing.EndDate, ItemListing.EventItemID, EventItem.EventItemName, EventItem.EventDescription
	FROM ItemListing, EventItem
	WHERE ItemListing.EventItemID = EventItem.EventItemID
END

	RETURN @@ROWCOUNT