CREATE TABLE [dbo].[ItemListing] (
    [ItemListID]            INT          IDENTITY (100, 1) NOT NULL,
    [StartDate]             DATETIME     NOT NULL,
    [EndDate]               DATETIME     NOT NULL,
    [EventItemID]           INT          NOT NULL,
    [Price]                 MONEY        NOT NULL,

    [SupplierID]            INT          NOT NULL,
    [Active]                BIT          NOT NULL,
    [CurrentNumberOfGuests] INT          NOT NULL,
    [MaxNumberOfGuests]     INT          NOT NULL,
    [MinNumberOfGuests]     INT          NULL,
    CONSTRAINT [PK_ItemListing] PRIMARY KEY CLUSTERED ([ItemListID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [ItemListingIndex]
    ON [dbo].[ItemListing]([ItemListID] ASC, [StartDate] ASC, [EndDate] ASC, [Price] ASC);
GO
ALTER TABLE [dbo].[ItemListing]
    ADD DEFAULT (0) FOR [CurrentNumberOfGuests];
GO
ALTER TABLE [dbo].[ItemListing]
    ADD DEFAULT (0) FOR [MaxNumberOfGuests];
GO
ALTER TABLE [dbo].[ItemListing]
    ADD DEFAULT (0) FOR [MinNumberOfGuests];
GO
ALTER TABLE [dbo].[ItemListing]
ADD CONSTRAINT [fk_ItemListing_EventItemID] FOREIGN KEY ([EventItemID]) REFERENCES [dbo].[EventItem] ([EventItemID]);
GO
ALTER TABLE [dbo].[ItemListing]
ADD CONSTRAINT [fk_ItemListing_Supplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Supplier] ([SupplierID]);
GO