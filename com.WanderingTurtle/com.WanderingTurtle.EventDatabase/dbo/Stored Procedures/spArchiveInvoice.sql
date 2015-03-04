/************* Created by:  Pat Banks 03/03/2015 ***********************/
/************* Updates Invoice with archive information:  Total Paid, date closed and changes active to false  ***********************/
 
 CREATE PROCEDURE [dbo].[spArchiveInvoice]
	@hotelGuestID	INT,
	@active			BIT,
	@dateOpened		DateTime,
	@dateClosed		DateTime,
	@totalPaid		money,
	
	@original_invoiceID  	INT,
	@original_hotelGuestID	INT,
	@original_active		BIT,
	@original_dateOpened	DateTime
AS
BEGIN
UPDATE Invoice
	SET hotelGuestID = @hotelGuestID,
		active = @active,
		dateOpened = @dateOpened,
		dateClosed = @dateClosed,
		totalPaid = @totalPaid
	WHERE invoiceID = @original_invoiceID
		AND hotelGuestID = @original_hotelGuestID
		AND active = @original_active
		AND dateOpened = @original_dateOpened

RETURN @@ROWCOUNT
END