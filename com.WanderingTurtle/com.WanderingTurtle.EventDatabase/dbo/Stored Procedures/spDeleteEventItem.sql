CREATE PROCEDURE spDeleteEventItem
	(
	@EventItemName 			varchar(255), 
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
	UPDATE EventItem SET
		Active = 0
	WHERE
		Active = 1
		AND EventItemID = @EventItemID
		AND EventItemName = @EventItemName
		AND CurrentNumberOfGuests = @CurrentNumberOfGuests
		AND MaxNumberOfGuests = @MaxNumberOfGuests
		AND MinNumberOfGuests = @MinNumberOfGuests
		AND EventTypeID = @EventTypeID
		AND PricePerPerson = @PricePerPerson
		AND EventOnsite = @EventOnsite
		AND Transportation = @Transportation
		AND EventDescription = @EventDescription
	RETURN @@ROWCOUNT