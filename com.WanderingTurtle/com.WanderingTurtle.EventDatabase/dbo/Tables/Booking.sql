CREATE TABLE [dbo].[Booking] (
	[BookingID] 	int				NOT NULL IDENTITY(100,1),
	[GuestID]	    int				NOT NULL,
	[EmployeeID]    int             NULL,
	[ItemListID]	int				NOT NULL,
	[Quantity]		int				NULL,
	[DateBooked]    DATETIME		NULL,
	[Discount]		decimal(3,2)    NOT NULL Default '0.0',
	[Active]		bit				NOT NULL Default '1',
	[TicketPrice]   decimal(8,2)	NULL,
	[ExtendedPrice] decimal(12,2)	NULL,
	[TotalCharge]	decimal(12,2)	NULL
		CONSTRAINT [pk_BookingID] PRIMARY KEY CLUSTERED ([BookingID] ASC)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
	
	CONSTRAINT [fk_GuestIDBooking] FOREIGN KEY ([GuestID])
		REFERENCES [dbo].[HotelGuest] (HotelGuestID)
			ON UPDATE NO ACTION
			ON DELETE NO ACTION,
			
	CONSTRAINT [fk_EmployeeIDBooking] FOREIGN KEY ([EmployeeID])
		REFERENCES [dbo].[Employee] (EmployeeID)
			ON UPDATE NO ACTION
			ON DELETE NO ACTION,
	CONSTRAINT [fk_ItemListIDBooking] FOREIGN KEY ([ItemListID])
		REFERENCES [dbo].[ItemListing] (ItemListID)
			ON UPDATE NO ACTION
			ON DELETE NO ACTION,
	
	) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX ix_GuestBookingID 
    ON dbo.Booking(BookingID);
GO
CREATE NONCLUSTERED INDEX ix_BookingDateBooked 
    ON dbo.Booking(DateBooked);