CREATE TABLE [dbo].[HotelGuest](
	[HotelGuestID] [int] IDENTITY(0,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Zip] [varchar](10) NOT NULL,
	[Address1] [varchar](255) NOT NULL,
	[Address2] [varchar](255) NULL,
	[PhoneNumber] [varchar](15) NULL,
	[EmailAddress] [varchar](100) NULL,
	[HotelGuestPIN] [int] NOT NULL
) ON [PRIMARY]