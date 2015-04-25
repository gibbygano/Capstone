CREATE PROCEDURE [dbo].[spArchiveInvoiceGuestBooking]
@guestID int

AS
	
BEGIN

DECLARE @bookingrowCount int, @invoiceRowCount int, @guestRowCount int, @invoiceTotalAmount  money, @bookingArchiveCount int

SELECT @invoiceTotalAmount = SUM(TotalCharge)
	FROM Booking
	WHERE Booking.GuestID = @guestID

SELECT @bookingrowCount = COUNT(*)
		FROM Booking
		WHERE Booking.GuestID = @guestID

	UPDATE Booking
		SET Active = 0
		WHERE Booking.GuestID = @guestID
	
	SET @bookingArchiveCount = @@ROWCOUNT

IF @bookingrowCount = @bookingArchiveCount

	UPDATE Invoice
		SET active = 0, 
			dateClosed = CURRENT_TIMESTAMP,
			totalPaid = @invoiceTotalAmount
	WHERE Invoice.HotelGuestID = @guestID

	SET  @invoiceRowCount = @@ROWCOUNT

IF @invoiceRowCount = 1

	UPDATE HotelGuest
		SET Active = 0,
			GuestPIN = null,
			Room = null
		WHERE HotelGuest.HotelGuestID = @guestID

RETURN @@ROWCOUNT
END