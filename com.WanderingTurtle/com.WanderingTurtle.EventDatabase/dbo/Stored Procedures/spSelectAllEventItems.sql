CREATE PROCEDURE spSelectAllEventItems
AS
	SELECT EventItemID, EventItemName, MaxNumberOfGuests, CurrentNumberOfGuests, MinNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active
	FROM EventItem
	WHERE Active = 1
	