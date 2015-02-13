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
	[Zip]		varchar(10)		NOT NULL,
	[City]		varchar(50) 	NOT NULL,
	[State]		varchar(50) 	NOT NULL,
		CONSTRAINT [pk_Zip] PRIMARY KEY CLUSTERED ([Zip] ASC)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
) ON [PRIMARY];
GO
/* Note - Do we want these indices clustered or nonclustered? Currently using nonclustered. 
	-Daniel Collingwood 
*/
CREATE NONCLUSTERED INDEX ix_CityStateZip 
    ON dbo.CityState(Zip);
GO
CREATE NONCLUSTERED INDEX ix_CityStateCity
    ON dbo.CityState(City);
GO
CREATE NONCLUSTERED INDEX ix_CityStateState
    ON dbo.CityState(State);