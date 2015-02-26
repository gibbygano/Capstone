CREATE TABLE [dbo].[Invoice] (
    [InvoiceID]  		INT      IDENTITY (100, 1) NOT NULL,
    [HotelGuestID]		INT      NOT NULL,
    [Active]			BIT		 NOT NULL,
	[DateOpened]		DateTime NOT NULL,
	[DateClosed]		DateTime,
	[TotalPaid]			money,
	CONSTRAINT [pk_InvoiceID] PRIMARY KEY CLUSTERED ([InvoiceID] ASC) ON [PRIMARY], 
) ON [PRIMARY];
GO
ALTER TABLE [dbo].[Invoice]
    ADD DEFAULT 1 FOR [Active];
GO
ALTER TABLE [dbo].[Invoice]
    ADD CONSTRAINT [fk_GuestIDInvoice] FOREIGN KEY ([HotelGuestID]) REFERENCES [dbo].[HotelGuest] ([HotelGuestID]);
GO