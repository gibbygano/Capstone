CREATE PROCEDURE spUpdateEventItem
	(
	@EventItemName 					varchar(255), 
	@CurrentNumberOfGuests 			int, 
	@MaxNumberOfGuests 				int, 
	@MinNumberOfGuests 				int, 
	@EventTypeID 					int, 
	@PricePerPerson 				money, 
	@EventOnsite 					bit, 
	@Transportation 				bit, 
	@EventDescription 				varchar(255),
	@EventItemID					int,
	@originalEventItemName 			varchar(255), 
	@originalCurrentNumberOfGuests 	int, 
	@originalMaxNumberOfGuests 		int, 
	@originalMinNumberOfGuests 		int, 
	@originalEventTypeID 			int, 
	@originalPricePerPerson 		money, 
	@originalEventOnsite 			bit, 
	@originalTransportation 		bit, 
	@originalEventDescription 		varchar(255)
	)
AS
	UPDATE EventItem SET
		EventItemName = @EventItemName, 
		CurrentNumberOfGuests = @CurrentNumberOfGuests, 
		MaxNumberOfGuests = @MaxNumberOfGuests, 
		MinNumberOfGuests = @MinNumberOfGuests, 
		EventTypeID = @EventTypeID, 
		PricePerPerson = @PricePerPerson, 
		EventOnsite = @EventOnsite, 
		Transportation = @Transportation, 
		EventDescription = @EventDescription
	WHERE
		Active = 1
		AND EventItemID = @EventItemID
		AND EventItemName = @originalEventItemName
		AND CurrentNumberOfGuests = @originalCurrentNumberOfGuests
		AND MaxNumberOfGuests = @originalMaxNumberOfGuests
		AND MinNumberOfGuests = @originalMinNumberOfGuests
		AND EventTypeID = @originalEventTypeID
		AND PricePerPerson = @originalPricePerPerson
		AND EventOnsite = @originalEventOnsite
		AND Transportation = @originalTransportation
		AND EventDescription = @originalEventDescription
	RETURN @@ROWCOUNT