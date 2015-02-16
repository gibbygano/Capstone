/*************************created by: Tony Noel************************************************/

CREATE PROCEDURE [dbo].[spDeleteBooking]
	(@BookingID 	int,
	
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_DateBooked	datetime)
AS
BEGIN
	DELETE FROM Booking
	WHERE BookingID = @BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND DateBooked = @original_DateBooked
END		
	RETURN @@ROWCOUNT