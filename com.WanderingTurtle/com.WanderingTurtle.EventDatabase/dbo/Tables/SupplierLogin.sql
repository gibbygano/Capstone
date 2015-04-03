CREATE TABLE [dbo].[SupplierLogin](
	[UserID]		int			NOT NULL IDENTITY(101, 1),
	[UserPassword]	varchar(50)	NOT NULL DEFAULT 'Password#1',
	[UserName]		varchar(50)	NOT NULL UNIQUE,
	[Active]			bit			NOT NULL DEFAULT 1,
	CONSTRAINT	[PK_SupplierLogin]	PRIMARY KEY ([UserID] ASC) ON [PRIMARY]
) ON [PRIMARY];