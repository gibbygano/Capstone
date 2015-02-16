CREATE PROCEDURE spUpdateEventType(@EventTypeID int, @EventName varchar, @originalEventName varchar)
AS
	UPDATE EventType
	SET EventName = @EventName
	WHERE EventTypeID = @EventTypeID AND EventName = @originalEventName
	RETURN @@ROWCOUNT