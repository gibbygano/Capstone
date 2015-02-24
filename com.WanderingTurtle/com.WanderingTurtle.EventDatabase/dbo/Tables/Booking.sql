CREATE TABLE [dbo].[Booking] (
    [BookingID]  INT      IDENTITY (100, 1) NOT NULL,
    [GuestID]    INT      NOT NULL,
    [EmployeeID] INT      NULL,
    [ItemListID] INT      NOT NULL,
    [Quantity]   INT      NOT NULL,
    [DateBooked] DATETIME NOT NULL,
    CONSTRAINT [pk_BookingID] PRIMARY KEY CLUSTERED ([BookingID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [ix_GuestBookingID]
    ON [dbo].[Booking]([BookingID] ASC);
GO
CREATE NONCLUSTERED INDEX [ix_BookingDateBooked]
    ON [dbo].[Booking]([DateBooked] ASC);
GO
ALTER TABLE [dbo].[Booking]
    ADD CONSTRAINT [fk_ItemListIDBooking] FOREIGN KEY ([ItemListID]) REFERENCES [dbo].[ItemListing] ([ItemListID]);
GO
ALTER TABLE [dbo].[Booking]
    ADD CONSTRAINT [fk_EmployeeIDBooking] FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employee] ([employeeID]);
GO
ALTER TABLE [dbo].[Booking]
    ADD CONSTRAINT [fk_GuestIDBooking] FOREIGN KEY ([GuestID]) REFERENCES [dbo].[HotelGuest] ([HotelGuestID]);