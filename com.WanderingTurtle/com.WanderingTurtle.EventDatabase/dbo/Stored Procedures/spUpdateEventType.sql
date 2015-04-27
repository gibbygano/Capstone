CREATE PROCEDURE spUpdateEventType
(@EventName varchar(255), 
 @originalEventTypeID int, 
 @originalEventName varchar(255))

AS
   UPDATE EventType
	  SET EventName = @EventName
	WHERE EventTypeID = @originalEventTypeID
	  AND EventName = @originalEventName
   RETURN @@ROWCOUNT