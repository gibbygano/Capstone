CREATE PROCEDURE spDeleteEventItem
	(
	@EventItemName 			varchar(255), 
	@EventTypeID 			int, 
	@EventOnsite 			bit, 
	@Transportation 		bit, 
	@EventDescription 		varchar(255), 
	@EventItemID 			int)
AS
	UPDATE EventItem SET
		Active = 0
	WHERE
		Active = 1
		AND EventItemID = @EventItemID
		AND EventItemName = @EventItemName
		AND EventTypeID = @EventTypeID
		AND EventOnsite = @EventOnsite
		AND Transportation = @Transportation
		AND EventDescription = @EventDescription
	RETURN @@ROWCOUNT