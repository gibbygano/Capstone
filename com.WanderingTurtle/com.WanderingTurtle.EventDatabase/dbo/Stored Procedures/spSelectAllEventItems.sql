CREATE PROCEDURE spSelectAllEventItems
AS
	SELECT EventItemID, EventItemName, EventItem.EventTypeID, EventOnsite, Transportation, EventDescription, EventItem.Active, EventType.EventName
	FROM EventItem
	JOIN EventType
	ON EventItem.EventTypeID = EventType.EventTypeID
	WHERE EventItem.Active = 1
	