CREATE PROCEDURE spSelectEventType(@EventTypeID int)
AS
	SELECT EventTypeID, EventName
	FROM EventType
	WHERE EventTypeID = @EventTypeID
	RETURN @@ROWCOUNT