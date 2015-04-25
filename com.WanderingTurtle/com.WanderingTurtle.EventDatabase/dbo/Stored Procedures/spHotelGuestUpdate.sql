CREATE PROCEDURE [dbo].[spHotelGuestUpdate]
	@firstName varchar(50),
	@lastName varchar(50),
	@zip char(5),
	@address1 varchar(255),
	@address2 varchar(255),
	@phoneNumber varchar(15),
	@email varchar(100),
	@room char(4),
	@active bit,
	@guestpin char(6),
	
	@original_hotelGuestID int,
	@original_firstName varchar(50),
	@original_lastName varchar(50),
	@original_zip char(5),
	@original_address1 varchar(255),
	@original_address2 varchar(255),
	@original_phoneNumber varchar(15),
	@original_email varchar(100),
	@original_room char(4),
	@original_active bit,
	@original_guestpin char(6)
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
			[Room] = @room,
			[Active] = @active,
			[GuestPIN] = @guestpin
	WHERE HotelGuestID = @original_hotelGuestID
		AND [FirstName] = @original_firstName
		AND [LastName] = @original_lastName
		AND	[Zip] = @original_zip
		AND	[Address1] = @original_address1
		AND	[Address2] = @original_address2
		AND	[PhoneNumber] = @original_phoneNumber
		AND	[EmailAddress] = @original_email
		AND [Room] = @original_room
		AND [Active] = @original_active
		AND [GuestPIN] = @original_guestpin

	RETURN @@ROWCOUNT

END