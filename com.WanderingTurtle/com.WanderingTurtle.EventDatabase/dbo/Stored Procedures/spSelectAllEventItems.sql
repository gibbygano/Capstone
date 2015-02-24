CREATE PROCEDURE spSelectAllEventItems
AS
	SELECT EventItemID, EventItemName, EventTypeID, EventOnsite, Transportation, EventDescription, Active
	FROM EventItem
	WHERE Active = 1
	