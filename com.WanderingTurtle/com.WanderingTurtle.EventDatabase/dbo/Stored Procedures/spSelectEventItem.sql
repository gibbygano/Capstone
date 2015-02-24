CREATE PROCEDURE spSelectEventItem(@EventItemID int)
AS
	SELECT EventItemID, EventItemName, EventTypeID, EventOnsite, Transportation, EventDescription, Active
	FROM EventItem
	WHERE Active = 1
		AND EventItemID = @EventItemID
	RETURN @@ROWCOUNT