CREATE PROCEDURE spUpdateEventType(@EventTypeID int, @EventName varchar)
AS
	UPDATE EventType
	SET EventName = @EventName
	WHERE EventTypeID = @EventTypeID AND EventName = @EventName
	RETURN @@ROWCOUNT