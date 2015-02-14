CREATE PROCEDURE spSelectEventType(@EventTypeID int)
AS
	SELECT *
	FROM EventType
	WHERE EventTypeID = @EventTypeID
	RETURN @@ROWCOUNT