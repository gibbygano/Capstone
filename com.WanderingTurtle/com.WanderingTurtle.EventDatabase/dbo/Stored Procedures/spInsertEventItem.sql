CREATE PROCEDURE spInsertEventItem
	(
	@EventItemName 			varchar(255),  
	@CurrentNumberOfGuests 	int = 0, 
	@MaxNumberOfGuests 		int, 
	@MinNumberOfGuests 		int, 
	@EventTypeID 			int, 
	@PricePerPerson 		money, 
	@EventOnsite 			bit, 
	@Transportation 		bit, 
	@EventDescription 		varchar(255))
AS
INSERT INTO EventItem(EventItemName, EventTypeID, EventOnsite, Transportation, EventDescription, Active) 
VALUES (@EventItemName,@EventTypeID, @EventOnsite, @Transportation, @EventDescription, 1)
RETURN @@ROWCOUNT