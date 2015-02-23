CREATE TABLE [dbo].[SupplierFeedbackRecord](
	[RatingID] [int] NOT NULL IDENTITY(100,1),
	[SupplierID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Notes] [varchar](255) NOT NULL
 CONSTRAINT [PK_SupplierFeedbackRecord] PRIMARY KEY CLUSTERED 
(
	[RatingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE INDEX SupplierFeedbackRecordIndex
ON [SupplierFeedbackRecord] (RatingID, Rating)