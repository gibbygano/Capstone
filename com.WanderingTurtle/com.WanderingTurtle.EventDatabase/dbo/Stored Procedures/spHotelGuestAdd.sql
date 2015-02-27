CREATE PROCEDURE [dbo].[spHotelGuestAdd]
	@firstName varchar(50),
	@lastName varchar(50),
	@zip varchar(10),
	@address1 varchar(255),
	@address2 varchar(255),
	@phoneNumber varchar(15),
	@email varchar(100),
	@room int
AS
BEGIN
	INSERT INTO [HotelGuest] ([FirstName],[LastName],[Zip],[Address1],[Address2],[PhoneNumber],[Room],[EmailAddress]) 
	VALUES (@firstName, @lastName, @zip, @address1, @address2, @phoneNumber, @email, @room)
	 
	RETURN @@ROWCOUNT

END