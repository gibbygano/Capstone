CREATE PROCEDURE [dbo].[spArchiveEventType]
	@EventTypeID int
AS
	UPDATE 
	  EventType 
	SET
		Active = 0
	WHERE
		EventTypeID = @EventTypeID

RETURN @@ROWCOUNT
