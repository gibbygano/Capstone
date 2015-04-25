/* -------------------------Booking Stored Procedures -------
-------------------created by: Tony Noel     
Updates the number of guests in ItemListID */

CREATE PROCEDURE [dbo].[spInsertBookingUpdateListingNum]
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
	DECLARE @bookingTotal int, @bookingRowCount int

    INSERT INTO Booking(GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Discount, TicketPrice, ExtendedPrice, TotalCharge)
    VALUES(@GuestID, @EmployeeID, @ItemListID, @Quantity, @DateBooked, @Discount, @TicketPrice, @ExtendedPrice, @TotalCharge)
	
	SET @bookingRowCount = @@ROWCOUNT

IF @bookingRowCount = 1

	SELECT @bookingTotal = SUM(Quantity)
	FROM Booking
	WHERE Booking.ItemListID = @ItemListID
	
	UPDATE ItemListing
		SET CurrentNumberOfGuests = @bookingTotal  
		WHERE ItemListing.ItemListID = @ItemListID

RETURN @@ROWCOUNT