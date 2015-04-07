CREATE PROCEDURE spUpdateEventItem
	(
	@EventItemName 					varchar(255),
	@EventTypeID 					int, 
	@EventOnsite 					bit, 
	@Transportation 				bit, 
	@EventDescription 				varchar(255),
	
	@originalEventItemID			int,
	@originalEventItemName 			varchar(255), 
	@originalEventTypeID 			int,
	@originalEventOnsite 			bit, 
	@originalTransportation 		bit, 
	@originalEventDescription 		varchar(255)
	)
AS
	UPDATE EventItem SET
		EventItemName = @EventItemName, 
		EventTypeID = @EventTypeID, 
		EventOnsite = @EventOnsite, 
		Transportation = @Transportation, 
		EventDescription = @EventDescription
	WHERE
		Active = 1
		AND EventItemID = @originalEventItemID
		AND EventItemName = @originalEventItemName
		AND EventTypeID = @originalEventTypeID
		AND EventOnsite = @originalEventOnsite
		AND Transportation = @originalTransportation
		AND EventDescription = @originalEventDescription
	RETURN @@ROWCOUNT