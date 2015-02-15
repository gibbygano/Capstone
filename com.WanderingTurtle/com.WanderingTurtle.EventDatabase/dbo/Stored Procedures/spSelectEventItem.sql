CREATE PROCEDURE spSelectEventItem(@EventItemID int)
AS
	SELECT *
	FROM EventItem
	WHERE Active = 1
		AND EventItemID = @EventItemID
	RETURN @@ROWCOUNT