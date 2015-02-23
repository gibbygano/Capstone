CREATE PROCEDURE spSelectEventItem(@EventItemID int)
AS
	SELECT EventItemID, EventItemName, MaxNumberOfGuests, CurrentNumberOfGuests, MinNumberOfGuests, EventTypeID, 
		PricePerPerson, EventOnsite, Transportation, EventDescription, Active
	FROM EventItem
	WHERE Active = 1
		AND EventItemID = @EventItemID
	RETURN @@ROWCOUNT