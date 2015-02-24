CREATE TABLE [dbo].[Lists] (
    [SupplierID] INT  NOT NULL,
    [ItemListID] INT  NOT NULL,
    [DateListed] DATE NOT NULL,
    CONSTRAINT [PK_Lists] PRIMARY KEY CLUSTERED ([SupplierID] ASC, [ItemListID] ASC) ON [PRIMARY]
) ON [PRIMARY];