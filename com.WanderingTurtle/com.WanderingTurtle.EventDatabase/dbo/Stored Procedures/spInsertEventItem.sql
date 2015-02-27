CREATE PROCEDURE spInsertEventItem
	(
	@EventItemName 			varchar(255),  
	@EventTypeID 			int, 
	@EventOnsite 			bit, 
	@Transportation 		bit, 
	@EventDescription 		varchar(255))
AS
INSERT INTO EventItem(EventItemName, EventTypeID, EventOnsite, Transportation, EventDescription, Active) 
VALUES (@EventItemName,@EventTypeID, @EventOnsite, @Transportation, @EventDescription, 1)
RETURN @@ROWCOUNT