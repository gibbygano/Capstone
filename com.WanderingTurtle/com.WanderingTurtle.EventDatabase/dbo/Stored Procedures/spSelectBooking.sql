/* ------------------------------Select- General --------------created by: Tony Noel-----------------*/
CREATE PROCEDURE [dbo].[spSelectBooking]
	@bookingID int
AS
BEGIN
	SELECT * 
	FROM Booking 
	WHERE BookingID = @bookingID
END

	RETURN @@ROWCOUNT