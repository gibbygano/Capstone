CREATE TABLE [dbo].[SupplierApplication](
	[ApplicationID] [int] IDENTITY(100,1) NOT NULL,
	[CompanyName] [varchar](255) NOT NULL,
	[CompanyDescription] [varchar](255) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Address1] [varchar](255) NOT NULL,
	[Address2] [varchar](255) ,
	[Zip] [varchar](10) NOT NULL,
	[PhoneNumber] [varchar](15) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[ApplicationDate] [date] NOT NULL,
	[Approved] [bit] NOT NULL DEFAULT 0,
	[ApprovalDate] [date] NULL
 CONSTRAINT [PK_SupplierApplication] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE INDEX SupplierApplicationIndex
ON [SupplierApplication] (ApplicationID)
GO
CREATE INDEX SupplierApplicationNameIndex
ON [SupplierApplication] (CompanyName)