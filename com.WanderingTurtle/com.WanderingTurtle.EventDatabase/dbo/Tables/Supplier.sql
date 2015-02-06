CREATE TABLE [dbo].[Supplier](
	[SupplierID] [int] IDENTITY(100,1) NOT NULL,
	[CompanyName] [varchar](255) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Address1] [varchar](255) NOT NULL,
	[Address2] [varchar](255) ,
	[Zip] [varchar](10) NOT NULL,
	[PhoneNumber] [varchar](15) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[SupplierTypeID] [int] NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Active] [int] NOT NULL
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE INDEX SupplierIndex
ON [Supplier] (SupplierID)