/*************************created by: Tony Noel************************************************/

CREATE PROCEDURE [dbo].[spDeleteBooking]
	(@BookingID 	int,
	
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_ItemListID	int,
	@original_Quantity		int
	)
AS
BEGIN
	DELETE FROM Booking
	WHERE BookingID = @BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND ItemListID = @original_ItemListID
		AND Quantity = @original_Quantity
END		
	RETURN @@ROWCOUNT