CREATE PROCEDURE [dbo].[spInsertEventType]
@EventName varchar(255)
AS
	INSERT INTO [EventType] ([EventName])
		VALUES(@EventName)

RETURN @@ROWCOUNT
