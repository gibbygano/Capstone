/************* Created by:  Pat Banks 02/28/2015 ***********************/
/************* creates a list of all invoices with hotel guest information  ***********************/

CREATE PROCEDURE spSelectAllInvoices
AS
BEGIN
	SELECT InvoiceID, Invoice.HotelGuestID, DateOpened, Invoice.Active, HotelGuest.LastName, HotelGuest.FirstName, HotelGuest.Room
	FROM Invoice, HotelGuest
	WHERE Invoice.HotelGuestID = HotelGuest.HotelGuestID

END