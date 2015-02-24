/*
 ** CityState Table creation script
 *
 *	Used to create the CityState table within the Capstone project database.
 *	
 **	Table Summary:
 *	Zip		(varchar 10) (Indexed)	Primary Key
 *	City	(varchar 50) (Indexed)
 *	State	(varchar 50) (Indexed)
 *
 **	-Daniel Collingwood
 */
 
CREATE TABLE [dbo].[CityState] (
    [Zip]   VARCHAR (10) NOT NULL,
    [City]  VARCHAR (50) NOT NULL,
    [State] VARCHAR (50) NOT NULL,
    CONSTRAINT [pk_Zip] PRIMARY KEY CLUSTERED ([Zip] ASC) ON [PRIMARY]
) ON [PRIMARY];
GO
/* Note - Do we want these indices clustered or nonclustered? Currently using nonclustered. 
	-Daniel Collingwood 
*/
CREATE NONCLUSTERED INDEX [ix_CityStateZip]
    ON [dbo].[CityState]([Zip] ASC);
GO
CREATE NONCLUSTERED INDEX [ix_CityStateCity]
    ON [dbo].[CityState]([City] ASC);
GO
CREATE NONCLUSTERED INDEX [ix_CityStateState]
    ON [dbo].[CityState]([State] ASC);