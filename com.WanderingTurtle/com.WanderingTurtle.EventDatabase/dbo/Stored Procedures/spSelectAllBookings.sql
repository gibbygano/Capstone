﻿/****************************created by: Tony Noel************************************/

CREATE PROCEDURE [dbo].[spSelectAllBookings]
AS
BEGIN
	SELECT BookingID, GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Discount, Active, TicketPrice, ExtendedPrice, TotalCharge
	FROM Booking 
END

	RETURN @@ROWCOUNT