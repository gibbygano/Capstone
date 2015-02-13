CREATE PROCEDURE [dbo].[spHotelGuestUpdate]
	@firstName varchar(50),
	@lastName varchar(50),
	@zip varchar(10),
	@address1 varchar(255),
	@address2 varchar(255),
	@phoneNumber varchar(15),
	@email varchar(100),
	@hotelGuestPIN int,
	
	@original_hotelGuestID int,
	@original_firstName varchar(50),
	@original_lastName varchar(50),
	@original_zip varchar(10),
	@original_address1 varchar(255),
	@original_address2 varchar(255),
	@original_phoneNumber varchar(15),
	@original_email varchar(100),
	@original_hotelGuestPIN int
AS
BEGIN
	UPDATE [HotelGuest]
		SET [FirstName] = @firstName,
			[LastName] = @lastName,
			[Zip] = @zip,
			[Address1] = @address1,
			[Address2] = @address2,
			[PhoneNumber] = @phoneNumber,
			EmailAddress = @email,
			[HotelGuestPIN] = @hotelGuestPIN
	WHERE HotelGuestID = @original_hotelGuestID
		AND [FirstName] = @original_firstName
		AND [LastName] = @original_lastName
		AND	[LastName] = @lastName
		AND	[Zip] = @zip
		AND	[Address1] = @address1
		AND	[Address2] = @address2
		AND	[PhoneNumber] = @phoneNumber
		AND	EmailAddress = @email
		AND	[HotelGuestPIN] = @hotelGuestPIN

	RETURN @@ROWCOUNT

END