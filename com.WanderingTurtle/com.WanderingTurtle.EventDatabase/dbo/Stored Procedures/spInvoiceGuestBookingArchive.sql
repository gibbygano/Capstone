CREATE PROCEDURE [dbo].[spInvoiceGuestBookingArchive]
	(@original_BookingID     int,
	@original_GuestID		int,
	@original_EmployeeID	int,
	@original_ItemListID     int,
	@original_Quantity     int,
	@original_DateBooked	datetime,
	@original_Discount			decimal(3,2),
	@original_Active            bit,
	@original_TicketPrice	decimal(8,2),
	@original_ExtendedPrice decimal(12,2),
	@original_TotalCharge	decimal(12,2),
	
	@totalPaid				decimal(12,2),

	@original_invoiceID  	INT,
	@original_hotelGuestID	INT,
	@original_active		BIT,
	@original_dateOpened	DateTime,

	@original_hotelGuestID int,
	@original_firstName varchar(50),
	@original_lastName varchar(50),
	@original_zip char(5),
	@original_address1 varchar(255),
	@original_address2 varchar(255),
	@original_phoneNumber varchar(15),
	@original_emailAddress varchar(100),
	@original_room char(4),
	@original_active bit,
	@original_guestpin char(5))


AS
	
BEGIN

	DECLARE @bookingrowCount int, @invoiceRowCount int, @guestRowCount int	
	UPDATE Booking
		SET Active = 0
	WHERE BookingID = @original_BookingID
		AND GuestID = @original_GuestID
		AND EmployeeID = @original_EmployeeID
		AND ItemListID = @original_ItemListID
		AND Quantity = @original_Quantity
		AND DateBooked = @original_DateBooked
		AND Discount = @original_Discount
		AND Active = @original_Active
		AND TicketPrice = @original_TicketPrice
		AND ExtendedPrice = @original_ExtendedPrice
		AND TotalCharge = @original_TotalCharge	
	
		SET  @bookingrowCount = @@ROWCOUNT

IF @bookingrowCount  > 1

UPDATE Invoice
	SET active = 0, 
		dateClosed = CURRENT_TIMESTAMP,
		totalPaid = @totalPaid
	WHERE invoiceID = @original_invoiceID
		AND hotelGuestID = @original_hotelGuestID
		AND active = @original_active
		AND dateOpened = @original_dateOpened

		SET  @invoiceRowCount = @@ROWCOUNT
