CREATE TABLE [dbo].[Employee] (
    [employeeID]  INT          IDENTITY (100, 1) NOT NULL,
    [firstName]   VARCHAR (50) NOT NULL,
    [lastName]    VARCHAR (50) NOT NULL,
    [empPassword] VARCHAR (8)  NOT NULL,
    [empLevel]    INT          NOT NULL,
    [active]      BIT          NOT NULL,
    CONSTRAINT [pk_employee] PRIMARY KEY CLUSTERED ([employeeID] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
ALTER TABLE [dbo].[Employee]
    ADD DEFAULT 1 FOR [active];