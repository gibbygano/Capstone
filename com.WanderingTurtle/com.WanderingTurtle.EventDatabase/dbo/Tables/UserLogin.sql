/* Summary: this creates the table that stores the login information of everyone except the hotel guests 
   Parameters: none
   Expectations: none */
CREATE TABLE UserLogin (
	userID			int			NOT NULL IDENTITY(100,1) UNIQUE,
	userPassword	varchar(50)	NOT NULL UNIQUE,
	userLevel		char		NOT NULL,
	CONSTRAINT		[pk_userID]	PRIMARY KEY([userID] ASC)
) ON [PRIMARY];