--Hunter Lind
--Delete method only for use in EventTypeAccessorTest
CREATE PROCEDURE [dbo].[spDeleteTestEventType](

        @EventName          varchar(50))
AS
        DELETE FROM EventType
        WHERE
        EventName = @EventName
		RETURN @@ROWCOUNT