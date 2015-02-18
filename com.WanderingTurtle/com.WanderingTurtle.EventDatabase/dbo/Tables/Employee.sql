CREATE TABLE [dbo].[Employee] (
	[employeeID] 					[int]					NOT NULL IDENTITY(100, 1),
	[firstName]						[varchar](50)			NOT NULL,
	[lastName]						[varchar](50)			NOT NULL,
	[empPassword]					[varchar](8)            NOT NULL,
	[empLevel]						[int]					NOT NULL,
	[active]						[bit]					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_employee] PRIMARY KEY CLUSTERED
([employeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
