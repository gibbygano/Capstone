CREATE TABLE [dbo].[HotelGuest] (
    [HotelGuestID] INT           IDENTITY (0, 1) NOT NULL,
    [FirstName]    VARCHAR (50)  NOT NULL,
    [LastName]     VARCHAR (50)  NOT NULL,
    [Zip]          VARCHAR (10)  NOT NULL,
    [Address1]     VARCHAR (255) NOT NULL,
    [Address2]     VARCHAR (255) NULL,
    [PhoneNumber]  VARCHAR (15)  NULL,
    [EmailAddress] VARCHAR (100) NULL,
	[Room]		   int			 NULL UNIQUE,
    [Active]       BIT           NOT NULL,
    CONSTRAINT [pk_HotelGuest] PRIMARY KEY CLUSTERED ([HotelGuestID] ASC)
) ON [PRIMARY];

GO
ALTER TABLE [dbo].[HotelGuest]
    ADD CONSTRAINT [fk_HotelGuest_CityState] FOREIGN KEY ([Zip]) REFERENCES [dbo].[CityState] ([Zip]);
GO
ALTER TABLE [dbo].[HotelGuest]
    ADD DEFAULT 1 FOR [Active];