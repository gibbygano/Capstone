CREATE TABLE [dbo].[ItemListing](
	[ItemListID] [int] IDENTITY(100,1) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[EventItemID] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[QuantityOffered] [int] NOT NULL,
	[ProductSize] [varchar](50) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[EventStartTime] [dateTime2] NOT NULL,
	[EventEndTime] [dateTime2] NOT NULL,
	[Active]	[bit] NOT NULL
 CONSTRAINT [PK_ItemListing] PRIMARY KEY CLUSTERED 
(
	[ItemListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE INDEX ItemListingIndex
ON [ItemListing] (ItemListID, StartDate, EndDate, Price, QuantityOffered)