CREATE PROCEDURE spUpdateEventItem
	(
	@EventItemName 					varchar(255), 
	@EventStartTime 				datetime2, 
	@EventEndTime 					datetime2, 
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
	@originalEventStartTime 		datetime2, 
	@originalEventEndTime 			datetime2, 
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
		EventStartTime = @EventStartTime, 
		EventEndTime = @EventEndTime, 
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
		AND EventStartTime = @originalEventStartTime
		AND EventEndTime = @originalEventEndTime
		AND CurrentNumberOfGuests = @originalCurrentNumberOfGuests
		AND MaxNumberOfGuests = @originalMaxNumberOfGuests
		AND MinNumberOfGuests = @originalMinNumberOfGuests
		AND EventTypeID = @originalEventTypeID
		AND PricePerPerson = @originalPricePerPerson
		AND EventOnsite = @originalEventOnsite
		AND Transportation = @originalTransportation
		AND EventDescription = @originalEventDescription
	RETURN @@ROWCOUNT