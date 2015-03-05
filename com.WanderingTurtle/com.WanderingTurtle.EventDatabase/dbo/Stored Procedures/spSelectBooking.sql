/* ------------------------------Select- General --------------created by: Tony Noel-----------------*/
CREATE PROCEDURE [dbo].[spSelectBooking]
	(@bookingID int)
AS
BEGIN
	SELECT BookingID, GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Cancel, Refund, Active
	FROM Booking 
	WHERE BookingID = @bookingID
END

	RETURN @@ROWCOUNT