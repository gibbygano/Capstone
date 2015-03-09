CREATE PROCEDURE spSelectAllEventItems
AS
	SELECT EventItemID, EventItemName, EventItem.EventTypeID, EventOnsite, Transportation, EventDescription, Active, EventType.EventName
	FROM EventItem
	JOIN EventType
	ON EventItem.EventTypeID = EventType.EventTypeID
	WHERE Active = 1
	