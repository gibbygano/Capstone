
INSERT INTO Supplier (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, UserID, Active)
VALUES
("Francisco's Tours", "Francisco", "McHurdley", "255 East West St", null, "66685", "555-542-8796", "franciscotours@gmail.com", 100, 101, 1),
("Harry's Boat Rides", "Harry", "Bertleson", "19925 Wilmington Ave", "Suite 206", "66686",  "555-874-9663", "harrythehammer@gmail.com", 101, 102, 1),
("They're Grape Tours, LLC", "Gregory", "Allensworth", "1644 East Central Way", null, "66685", "555-766-1124", "info@theyregrapetours.com", 102, 103, 1)
GO

INSERT INTO SupplierApplication (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate)
VALUES
("Francisco's Tours", "Francisco", "McHurdley", "255 East West St", null, "66685", "555-542-8796", "franciscotours@gmail.com", "2014-12-22", 1, "2015-01-01"),
("Harry's Boat Rides", "Harry", "Bertleson", "19925 Wilmington Ave", "Suite 206", "66686",  "555-874-9663", "harrythehammer@gmail.com", "2014-02-06", 1, "2014-06-12"),
("They're Grape Tours, LLC", "Gregory", "Allensworth", "1644 East Central Way", null, "66685", "555-766-1124", "info@theyregrapetours.com", "2015-01-22", 1, "2015-02-12")
GO

INSERT INTO EventItem (EventItemName, EventStartTime, EventEndTime, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active)
VALUES
("Non-Creepy Boat Ride", "00:00:00", "01:00:00", 0, 10, 1, 100, 15.00, 0, 0, "A totally non creepy midnight boat ride down the river.", 1 ),
("Jungle Tour", "13:00:00", "15:00:00", 2, 30, 5, 101, 25.00, 0, 1, "A Majestic Jungle Tour. Not safe for kids under 12.", 1 ),
("Market Excursion", "10:00:00", "12:00:00", 5, 50, 2, 101, 5.00, 0, 1, "Tour the local marketplace. Haggle your way to some great stuff!", 1 )
GO

INSERT INTO EventType (EventName)
VALUES
("Boat Ride"),
("Tour"),
("Dinner"),
("Entertainment")
GO

INSERT INTO ItemListing (StartDate, EndDate, EventItemID, Price, QuantityOffered, ProductSize)
VALUES
("2015-03-01", "2015-03-01", 100, 15.00, 10, "One Boat"),
("2015-03-01", "2015-03-01", 101, 25.00, 30, "One Excursion"),
("2015-03-01", "2015-03-01", 102, 5.00, 50, "One Trip")
GO

INSERT INTO Lists (SupplierID, ItemListID, DateListed)
VALUES
(100,100, GETDATE()),
(101,101, GETDATE()),
(102,102, GETDATE())
GO


--STORED PROCEDURES
CREATE PROCEDURE [dbo].[spSelectSupplierApplication] (
	@ApplicationID )
AS
	SELECT ApplicationID, CompanyName, CompanyDescription, Firs, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate 
	FROM SupplierApplication
	WHERE ApplicationID = @ApplicationID

GO

--Syntax Error: Incorrect syntax near ).
--
----STORED PROCEDURES
--CREATE PROCEDURE [dbo].[spSelectSupplierApplication] (
--	@ApplicationID )
--AS
--	SELECT ApplicationID, CompanyName, CompanyDescription, Firs, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate 
--	FROM SupplierApplication
--	WHERE ApplicationID = @ApplicationID

GO


CREATE PROCEDURE [dbo].[spSelectAllSupplierApplication] (
	@ApplicationID )
AS
	SELECT ApplicationID, CompanyName, CompanyDescription, Firs, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate 
	FROM SupplierApplication

GO

--Syntax Error: Incorrect syntax near ).
--
--CREATE PROCEDURE [dbo].[spSelectAllSupplierApplication] (
--	@ApplicationID )
--AS
--	SELECT ApplicationID, CompanyName, CompanyDescription, Firs, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate 
--	FROM SupplierApplication

GO


CREATE PROCEDURE spUpdateSupplierApplication
	(
	@CompanyName 			varchar(255),
	@FirstName 				varchar(50), 
	@LastName 				varchar(50), 
	@Address1 				varchar(255),
	@Address2 				varchar(255) NULL, 
	@Zip 					varchar(10), 
	@PhoneNumber 			varchar(15), 
	@EmailAddress 			varchar(100), 
	@ApplicationDate		[date],
	@Approved				bit,
	@ApprovalDate			[date],
	@ApplicationID 			int, 

	@originalCompanyName 	varchar(255),
	@originalFirstName 		varchar(50), 
	@originalLastName 		varchar(50), 
	@originalAddress1 		varchar(255), 
	@originalAddress2 		varchar(255) NULL, 
	@originalZip 			varchar(10), 
	@originalPhoneNumber 	varchar(15), 
	@originalEmailAddress 	varchar(100), 
	@originalApplicationDate [date],
	@originalApproved		bit,
	@originalApprovalDate	[date],
	@originalApplicationID 	int	
	
	)
AS
	UPDATE SupplierApplication SET
		CompanyName = @CompanyName, 
		FirstName = @FirstName, 
		LastName = @LastName, 
		Address1 = @Address1, 
		Address2 = @Address2, 
		Zip = @Zip, 
		PhoneNumber = @PhoneNumber, 
		EmailAddress = @EmailAddress, 
		ApplicationDate = @ApplicationDate,
		Approved = @Approved,
		ApprovalDate = @ApprovalDate,
	WHERE 
		ApplicationID = @ApplicationID
		AND CompanyName = @originalCompanyName
		AND FirstName = @originalFirstName
		AND LastName = @originalLastName
		AND Address1 = @originalAddress1
		AND Address2 = @originalAddress2
		AND Zip = @originalZip
		AND PhoneNumber = @originalPhoneNumber
		AND EmailAddress = @originalEmailAddress
		AND ApplicationDate = @originalApplicationDate
		AND Approved = @originalApproved
		AND ApprovalDate = @originalApprovalDate
		AND ApplicationID = @originalApplicationID
	RETURN @@ROWCOUNT
	
CREATE PROCEDURE spInsertSupplierApplication
	(
	@CompanyName 	varchar(255),
	@FirstName		varchar(50), 
	@LastName 		varchar(50), 
	@Address1 		varchar(255), 
	@Address2 		varchar(255) NULL, 
	@Zip 			varchar(10), 
	@PhoneNumber 	varchar(15), 
	@EmailAddress 	varchar(100), 
	@ApplicationDate date, 
	@Approved		bit,
	@ApprovalDate	date
	)
AS
INSERT INTO SupplierApplication
	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate) 
VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @ApplicationDate, @Approved, @ApprovalDate)
RETURN @@ROWCOUNT


GO

--Syntax Error: Incorrect syntax near NULL.
--
--CREATE PROCEDURE spUpdateSupplierApplication
--	(
--	@CompanyName 			varchar(255),
--	@FirstName 				varchar(50), 
--	@LastName 				varchar(50), 
--	@Address1 				varchar(255),
--	@Address2 				varchar(255) NULL, 
--	@Zip 					varchar(10), 
--	@PhoneNumber 			varchar(15), 
--	@EmailAddress 			varchar(100), 
--	@ApplicationDate		[date],
--	@Approved				bit,
--	@ApprovalDate			[date],
--	@ApplicationID 			int, 
--
--	@originalCompanyName 	varchar(255),
--	@originalFirstName 		varchar(50), 
--	@originalLastName 		varchar(50), 
--	@originalAddress1 		varchar(255), 
--	@originalAddress2 		varchar(255) NULL, 
--	@originalZip 			varchar(10), 
--	@originalPhoneNumber 	varchar(15), 
--	@originalEmailAddress 	varchar(100), 
--	@originalApplicationDate [date],
--	@originalApproved		bit,
--	@originalApprovalDate	[date],
--	@originalApplicationID 	int	
--	
--	)
--AS
--	UPDATE SupplierApplication SET
--		CompanyName = @CompanyName, 
--		FirstName = @FirstName, 
--		LastName = @LastName, 
--		Address1 = @Address1, 
--		Address2 = @Address2, 
--		Zip = @Zip, 
--		PhoneNumber = @PhoneNumber, 
--		EmailAddress = @EmailAddress, 
--		ApplicationDate = @ApplicationDate,
--		Approved = @Approved,
--		ApprovalDate = @ApprovalDate,
--	WHERE 
--		ApplicationID = @ApplicationID
--		AND CompanyName = @originalCompanyName
--		AND FirstName = @originalFirstName
--		AND LastName = @originalLastName
--		AND Address1 = @originalAddress1
--		AND Address2 = @originalAddress2
--		AND Zip = @originalZip
--		AND PhoneNumber = @originalPhoneNumber
--		AND EmailAddress = @originalEmailAddress
--		AND ApplicationDate = @originalApplicationDate
--		AND Approved = @originalApproved
--		AND ApprovalDate = @originalApprovalDate
--		AND ApplicationID = @originalApplicationID
--	RETURN @@ROWCOUNT
--	
--CREATE PROCEDURE spInsertSupplierApplication
--	(
--	@CompanyName 	varchar(255),
--	@FirstName		varchar(50), 
--	@LastName 		varchar(50), 
--	@Address1 		varchar(255), 
--	@Address2 		varchar(255) NULL, 
--	@Zip 			varchar(10), 
--	@PhoneNumber 	varchar(15), 
--	@EmailAddress 	varchar(100), 
--	@ApplicationDate date, 
--	@Approved		bit,
--	@ApprovalDate	date
--	)
--AS
--INSERT INTO SupplierApplication
--	(CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate) 
--VALUES (@CompanyName, @FirstName, @LastName, @Address1, @Address2, @Zip, @PhoneNumber, @EmailAddress, @ApplicationDate, @Approved, @ApprovalDate)
--RETURN @@ROWCOUNT
--



GO

INSERT INTO Supplier (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, UserID, Active)
VALUES
("Francisco's Tours", "Francisco", "McHurdley", "255 East West St", null, "66685", "555-542-8796", "franciscotours@gmail.com", 100, 101, 1),
("Harry's Boat Rides", "Harry", "Bertleson", "19925 Wilmington Ave", "Suite 206", "66686",  "555-874-9663", "harrythehammer@gmail.com", 101, 102, 1),
("They're Grape Tours, LLC", "Gregory", "Allensworth", "1644 East Central Way", null, "66685", "555-766-1124", "info@theyregrapetours.com", 102, 103, 1)
GO

INSERT INTO SupplierApplication (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate)
VALUES
("Francisco's Tours", "Francisco", "McHurdley", "255 East West St", null, "66685", "555-542-8796", "franciscotours@gmail.com", "2014-12-22", 1, "2015-01-01"),
("Harry's Boat Rides", "Harry", "Bertleson", "19925 Wilmington Ave", "Suite 206", "66686",  "555-874-9663", "harrythehammer@gmail.com", "2014-02-06", 1, "2014-06-12"),
("They're Grape Tours, LLC", "Gregory", "Allensworth", "1644 East Central Way", null, "66685", "555-766-1124", "info@theyregrapetours.com", "2015-01-22", 1, "2015-02-12")
GO

INSERT INTO EventItem (EventItemName, EventStartTime, EventEndTime, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active)
VALUES
("Non-Creepy Boat Ride", "00:00:00", "01:00:00", 0, 10, 1, 100, 15.00, 0, 0, "A totally non creepy midnight boat ride down the river.", 1 ),
("Jungle Tour", "13:00:00", "15:00:00", 2, 30, 5, 101, 25.00, 0, 1, "A Majestic Jungle Tour. Not safe for kids under 12.", 1 ),
("Market Excursion", "10:00:00", "12:00:00", 5, 50, 2, 101, 5.00, 0, 1, "Tour the local marketplace. Haggle your way to some great stuff!", 1 )
GO

INSERT INTO EventType (EventName)
VALUES
("Boat Ride"),
("Tour"),
("Dinner"),
("Entertainment")
GO

INSERT INTO ItemListing (StartDate, EndDate, EventItemID, Price, QuantityOffered, ProductSize)
VALUES
("2015-03-01", "2015-03-01", 100, 15.00, 10, "One Boat"),
("2015-03-01", "2015-03-01", 101, 25.00, 30, "One Excursion"),
("2015-03-01", "2015-03-01", 102, 5.00, 50, "One Trip")
GO

INSERT INTO Lists (SupplierID, ItemListID, DateListed)
VALUES
(100,100, GETDATE()),
(101,101, GETDATE()),
(102,102, GETDATE())
GO
