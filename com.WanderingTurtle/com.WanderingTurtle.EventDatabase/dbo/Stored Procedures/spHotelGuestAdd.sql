CREATE PROCEDURE [dbo].[spHotelGuestAdd]
	@firstName varchar(50),
	@lastName varchar(50),
	@zip varchar(10),
	@address1 varchar(255),
	@address2 varchar(255),
	@phoneNumber varchar(15),
	@email varchar(100),
	@hotelGuestPIN int
AS
BEGIN
	INSERT INTO [HotelGuest] ([FirstName],[LastName],[Zip],[Address1],[Address2],[PhoneNumber],[EmailAddress],[HotelGuestPIN]) 
	VALUES (@firstName, @lastName, @zip, @address1, @address2, @phoneNumber, @email, @hotelGuestPIN)
	 
	RETURN @@ROWCOUNT

END