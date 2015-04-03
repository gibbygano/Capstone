CREATE PROCEDURE spDeleteEvent_1
	(
	@EventItemID 			int)
AS
	Delete EventItem
	WHERE EventItemID = @EventItemID;