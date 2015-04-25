/* ------------------------------Select- General --------------created by: Tony Noel-----------------*/
CREATE PROCEDURE [dbo].[spSelectBookingByID]
	(@bookingID int)
AS
BEGIN
	SELECT BookingID, GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Discount, Active, TicketPrice, ExtendedPrice, TotalCharge
	FROM Booking 
	WHERE BookingID = @bookingID
END

	RETURN @@ROWCOUNT