CREATE TABLE [dbo].[SupplierApplication] (
    [ApplicationID]      INT           IDENTITY (100, 1) NOT NULL,
    [CompanyName]        VARCHAR (255) NOT NULL,
    [CompanyDescription] VARCHAR (255) NULL,
    [FirstName]          VARCHAR (50)  NOT NULL,
    [LastName]           VARCHAR (50)  NOT NULL,
    [Address1]           VARCHAR (255) NOT NULL,
    [Address2]           VARCHAR (255) NULL,
    [Zip]                VARCHAR (10)  NOT NULL,
    [PhoneNumber]        VARCHAR (15)  NOT NULL,
    [EmailAddress]       VARCHAR (100) NOT NULL,
    [ApplicationDate]    DATETIME          NOT NULL,
	[ApplicationStatus]  VARCHAR(25)   DEFAULT ('Pending'),
    [LastStatusDate]       DATETIME     NOT NULL,
    [Remarks]				VARCHAR(255) NULL, 
    CONSTRAINT [PK_SupplierApplication] PRIMARY KEY CLUSTERED ([ApplicationID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
CREATE NONCLUSTERED INDEX [SupplierApplicationIndex]
    ON [dbo].[SupplierApplication]([ApplicationID] ASC);
GO
CREATE NONCLUSTERED INDEX [SupplierApplicationNameIndex]
    ON [dbo].[SupplierApplication]([CompanyName] ASC);
GO
