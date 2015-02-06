CREATE PROCEDURE spInsertEventItem
	(
	@EventItemName 			varchar(255), 
	@EventStartTime 		datetime2, 
	@EventEndTime 			datetime2, 
	@CurrentNumberOfGuests 	int, 
	@MaxNumberOfGuests 		int, 
	@MinNumberOfGuests 		int, 
	@EventTypeID 			int, 
	@PricePerPerson 		money, 
	@EventOnsite 			bit, 
	@Transportation 		bit, 
	@EventDescription 		varchar(255), 
	@EventItemID 			int OUTPUT)
AS
INSERT INTO EventItem(EventItemName, EventStartTime, EventEndTime, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active) 
VALUES (@EventItemName, @EventStartTime, @EventEndTime, @CurrentNumberOfGuests, @MaxNumberOfGuests, @MinNumberOfGuests, @EventTypeID, @PricePerPerson, @EventOnsite, @Transportation, @EventDescription, 1)
SET @EventItemID = SCOPE_IDENTITY()
RETURN @@ERROR