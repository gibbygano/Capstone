CREATE TABLE [dbo].[SupplierLogin] (
    [UserID]       INT          IDENTITY (101, 1) NOT NULL,
    [UserPassword] VARCHAR (50) DEFAULT ('Password#1') NOT NULL,
    [UserName]     VARCHAR (50) NOT NULL,
    [SupplierID]   INT          NOT NULL,
    [Active]       BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SupplierLogin] PRIMARY KEY CLUSTERED ([UserID] ASC)
)	ON [PRIMARY];
	GO

CREATE UNIQUE NONCLUSTERED INDEX [UniqueUserName]
	ON [SupplierLogin]([SupplierID]);
GO
ALTER TABLE [dbo].[SupplierLogin]
ADD CONSTRAINT [fk_SupplierLogin_Supplier] FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Supplier] ([SupplierID]);
GO