/* -------------------------Booking Stored Procedures ---------------------------created by: Tony Noel*/
CREATE PROCEDURE [dbo].[spAddBooking]
	@GuestID		int,
	@EmployeeID		int,
	@ItemListID		int,
	@Quantity		int,
	@DateBooked		DateTime,
	@Discount		Decimal(3,2),
	@TicketPrice	Decimal(8,2),
	@ExtendedPrice	Decimal(12,2),
	@TotalCharge	Decimal(12,2)
	
AS
    INSERT INTO Booking(GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Discount, TicketPrice, ExtendedPrice, TotalCharge)
    VALUES(@GuestID, @EmployeeID, @ItemListID, @Quantity, @DateBooked, @Discount, @TicketPrice, @ExtendedPrice, @TotalCharge)
RETURN @@ROWCOUNT