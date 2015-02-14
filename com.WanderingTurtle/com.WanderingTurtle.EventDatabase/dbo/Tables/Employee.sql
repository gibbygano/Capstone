CREATE TABLE [dbo].[employee] (
	[employeeID] 					[int]					NOT NULL IDENTITY(100, 1),
	[firstName]						[varchar](50)			NOT NULL,
	[lastName]						[varchar](50)			NOT NULL,
	[userID]						[int] 					NOT NULL,
	[active]						[bit]					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_employee] PRIMARY KEY CLUSTERED
([employeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/* ***************************** adding foreign key ******************************************************** */

ALTER TABLE [dbo].[employee] WITH NOCHECK ADD CONSTRAINT [FK_employee] FOREIGN KEY([userID])
REFERENCES [dbo].[userLogin] ([userID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_employee]
GO