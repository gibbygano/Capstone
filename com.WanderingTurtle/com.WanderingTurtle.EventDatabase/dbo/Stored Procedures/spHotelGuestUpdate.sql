CREATE PROCEDURE [dbo].[spHotelGuestUpdate]
	@firstName varchar(50),
	@lastName varchar(50),
	@zip varchar(10),
	@address1 varchar(255),
	@address2 varchar(255),
	@phoneNumber varchar(15),
	@email varchar(100),
	@active bit,
	
	@original_hotelGuestID int,
	@original_firstName varchar(50),
	@original_lastName varchar(50),
	@original_zip varchar(10),
	@original_address1 varchar(255),
	@original_address2 varchar(255),
	@original_phoneNumber varchar(15),
	@original_email varchar(100),
	@original_active bit
AS
BEGIN
	UPDATE [HotelGuest]
		SET [FirstName] = @firstName,
			[LastName] = @lastName,
			[Zip] = @zip,
			[Address1] = @address1,
			[Address2] = @address2,
			[PhoneNumber] = @phoneNumber,
			[EmailAddress] = @email,
			[Active] = @active
	WHERE HotelGuestID = @original_hotelGuestID
		AND [FirstName] = @original_firstName
		AND [LastName] = @original_lastName
		AND	[Zip] = @original_zip
		AND	[Address1] = @original_address1
		AND	[Address2] = @original_address2
		AND	[PhoneNumber] = @original_phoneNumber
		AND	[EmailAddress] = @original_email
		AND [Active] = @original_active

	RETURN @@ROWCOUNT

END