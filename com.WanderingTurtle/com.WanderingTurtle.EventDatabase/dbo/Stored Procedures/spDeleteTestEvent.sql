--Hunter Lind
--Delete method only for use in EventAccessorTest
CREATE PROCEDURE [dbo].[spDeleteTestEvent](

        @EventItemName          varchar(50))
AS
        DELETE FROM EventItem
        WHERE
        EventItemName = @EventItemName
		RETURN @@ROWCOUNT