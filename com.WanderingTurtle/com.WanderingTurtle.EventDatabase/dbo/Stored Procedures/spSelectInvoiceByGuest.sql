/********Created by Pat Banks 2/27/2015 *********/
/************* selects invoice information related to one hotel guest ***********************/

CREATE PROCEDURE spSelectInvoiceByGuest
	(@guestID int)
AS
BEGIN
	SELECT InvoiceID, Invoice.HotelGuestID, DateOpened, Invoice.Active, Invoice.DateClosed, TotalPaid, HotelGuest.LastName, HotelGuest.FirstName, HotelGuest.Room
	FROM Invoice, HotelGuest
	WHERE Invoice.HotelGuestID = @guestID
	AND Invoice.HotelGuestID = HotelGuest.HotelGuestID

END