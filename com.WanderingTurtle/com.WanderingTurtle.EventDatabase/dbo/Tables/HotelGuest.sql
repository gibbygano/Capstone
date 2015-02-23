CREATE TABLE [dbo].[HotelGuest](
	[HotelGuestID] [int] IDENTITY(0,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Zip] [varchar](10) NOT NULL,
	[Address1] [varchar](255) NOT NULL,
	[Address2] [varchar](255) NULL,
	[PhoneNumber] [varchar](15) NULL,
	[EmailAddress] [varchar](100) NULL,
	[Active] [bit] DEFAULT 0,
    CONSTRAINT [pk_HotelGuest] PRIMARY KEY ([HotelGuestID]), 
	CONSTRAINT [fk_HotelGuest_CityState] FOREIGN KEY([Zip]) REFERENCES [dbo].[CityState] ([Zip])
) ON [PRIMARY]
