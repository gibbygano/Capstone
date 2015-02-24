CREATE TABLE [dbo].[ItemListing] (
    [ItemListID]            INT          IDENTITY (100, 1) NOT NULL,
    [StartDate]             DATETIME     NOT NULL,
    [EndDate]               DATETIME     NOT NULL,
    [EventItemID]           INT          NOT NULL,
    [Price]                 MONEY        NOT NULL,
    [QuantityOffered]       INT          NOT NULL,
    [ProductSize]           VARCHAR (50) NOT NULL,
    [SupplierID]            INT          NOT NULL,
    [Active]                BIT          NOT NULL,
    [CurrentNumberOfGuests] INT          NOT NULL,
    [MaxNumberOfGuests]     INT          NOT NULL,
    [MinNumberOfGuests]     INT          NULL,
    CONSTRAINT [PK_ItemListing] PRIMARY KEY CLUSTERED ([ItemListID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [ItemListingIndex]
    ON [dbo].[ItemListing]([ItemListID] ASC, [StartDate] ASC, [EndDate] ASC, [Price] ASC, [QuantityOffered] ASC);
GO
ALTER TABLE [dbo].[ItemListing]
    ADD DEFAULT (0) FOR [CurrentNumberOfGuests];
GO
ALTER TABLE [dbo].[ItemListing]
    ADD DEFAULT (0) FOR [MaxNumberOfGuests];
GO
ALTER TABLE [dbo].[ItemListing]
    ADD DEFAULT (0) FOR [MinNumberOfGuests];