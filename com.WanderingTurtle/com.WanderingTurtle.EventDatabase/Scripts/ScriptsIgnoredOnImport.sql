

CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255), 
	@originalRating int, 
	@originalNotes varchar(255) )
AS
	UPDATE SupplierFeedbackRecord
	SET
		Rating = @Rating,
		Notes = @Notes,
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @originalRating
		AND Nots = @originalNotes
	RETURN @@ROWCOUNT

GO

--Syntax Error: Incorrect syntax near WHERE.
--
--CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
--	@RatingID int, 
--	@SupplierID int, 
--	@EmployeeID int, 
--	@Rating int, 
--	@Notes varChar(255), 
--	@originalRating int, 
--	@originalNotes varchar(255) )
--AS
--	UPDATE SupplierFeedbackRecord
--	SET
--		Rating = @Rating,
--		Notes = @Notes,
--	WHERE
--		RatingID = @RatingID
--		AND SupplierID = @SupplierID
--		AND EmployeeID = @EmployeeID
--		AND Rating = @originalRating
--		AND Nots = @originalNotes
--	RETURN @@ROWCOUNT

GO


CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255) )
AS
	DELETE FROM SupplierFeedbackRecord
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @Rating
		AND Notes = @Notes
END
	RETURN @@ROWCOUNT

GO

--Syntax Error: Incorrect syntax near RETURN.
--
--CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
--	@RatingID int, 
--	@SupplierID int, 
--	@EmployeeID int, 
--	@Rating int, 
--	@Notes varChar(255) )
--AS
--	DELETE FROM SupplierFeedbackRecord
--	WHERE
--		RatingID = @RatingID
--		AND SupplierID = @SupplierID
--		AND EmployeeID = @EmployeeID
--		AND Rating = @Rating
--		AND Notes = @Notes
--END
--	RETURN @@ROWCOUNT



GO


CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
	@RatingID int, 
	@SupplierID int, 
	@EmployeeID int, 
	@Rating int, 
	@Notes varChar(255), 
	@originalRating int, 
	@originalNotes varchar(255) )
AS
	UPDATE SupplierFeedbackRecord
	SET
		Rating = @Rating,
		Notes = @Notes,
	WHERE
		RatingID = @RatingID
		AND SupplierID = @SupplierID
		AND EmployeeID = @EmployeeID
		AND Rating = @originalRating
		AND Nots = @originalNotes
	RETURN @@ROWCOUNT

GO

--Syntax Error: Incorrect syntax near WHERE.
--
--CREATE PROCEDURE spUpdateSupplierFeedbackRecord( 
--	@RatingID int, 
--	@SupplierID int, 
--	@EmployeeID int, 
--	@Rating int, 
--	@Notes varChar(255), 
--	@originalRating int, 
--	@originalNotes varchar(255) )
--AS
--	UPDATE SupplierFeedbackRecord
--	SET
--		Rating = @Rating,
--		Notes = @Notes,
--	WHERE
--		RatingID = @RatingID
--		AND SupplierID = @SupplierID
--		AND EmployeeID = @EmployeeID
--		AND Rating = @originalRating
--		AND Nots = @originalNotes
--	RETURN @@ROWCOUNT



GO

SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
GO

SET NUMERIC_ROUNDABORT OFF;
GO

IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END
GO

USE [master];
GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END
GO

PRINT N'Creating $(DatabaseName)...'
GO

CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END
GO

IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END
GO

IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END
GO

USE [$(DatabaseName)];
GO

IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';
GO

PRINT N'Creating [dbo].[Booking]...';
GO

PRINT N'Creating [dbo].[Booking].[ix_GuestBookingID]...';
GO

PRINT N'Creating [dbo].[Booking].[ix_BookingDateBooked]...';
GO

PRINT N'Creating [dbo].[CityState]...';
GO

PRINT N'Creating [dbo].[CityState].[ix_CityStateZip]...';
GO

PRINT N'Creating [dbo].[CityState].[ix_CityStateCity]...';
GO

PRINT N'Creating [dbo].[CityState].[ix_CityStateState]...';
GO

PRINT N'Creating [dbo].[Employee]...';
GO

PRINT N'Creating [dbo].[EventItem]...';
GO

PRINT N'Creating [dbo].[EventItem].[EventItemIndex]...';
GO

PRINT N'Creating [dbo].[EventType]...';
GO

PRINT N'Creating [dbo].[EventType].[EventTypeIndex]...';
GO

PRINT N'Creating [dbo].[HotelGuest]...';
GO

PRINT N'Creating [dbo].[ItemListing]...';
GO

PRINT N'Creating [dbo].[ItemListing].[ItemListingIndex]...';
GO

PRINT N'Creating [dbo].[Lists]...';
GO

PRINT N'Creating [dbo].[Supplier]...';
GO

PRINT N'Creating [dbo].[Supplier].[SupplierIndex]...';
GO

PRINT N'Creating [dbo].[SupplierApplication]...';
GO

PRINT N'Creating [dbo].[SupplierApplication].[SupplierApplicationIndex]...';
GO

PRINT N'Creating [dbo].[SupplierApplication].[SupplierApplicationNameIndex]...';
GO

PRINT N'Creating [dbo].[SupplierFeedbackRecord]...';
GO

PRINT N'Creating [dbo].[SupplierFeedbackRecord].[SupplierFeedbackRecordIndex]...';
GO

PRINT N'Creating unnamed constraint on [dbo].[Employee]...';
GO

PRINT N'Creating unnamed constraint on [dbo].[HotelGuest]...';
GO

PRINT N'Creating unnamed constraint on [dbo].[ItemListing]...';
GO

PRINT N'Creating unnamed constraint on [dbo].[ItemListing]...';
GO

PRINT N'Creating unnamed constraint on [dbo].[ItemListing]...';
GO

PRINT N'Creating unnamed constraint on [dbo].[SupplierApplication]...';
GO

PRINT N'Creating [dbo].[fk_GuestIDBooking]...';
GO

PRINT N'Creating [dbo].[fk_EmployeeIDBooking]...';
GO

PRINT N'Creating [dbo].[fk_ItemListIDBooking]...';
GO

PRINT N'Creating [dbo].[fk_HotelGuest_CityState]...';
GO

PRINT N'Creating [dbo].[spAddBooking]...';
GO

PRINT N'Creating [dbo].[spAddEmployee]...';
GO

PRINT N'Creating [dbo].[spCityStateCreate]...';
GO

PRINT N'Creating [dbo].[spCityStateRead]...';
GO

PRINT N'Creating [dbo].[spCityStateReadAll]...';
GO

PRINT N'Creating [dbo].[spCityStateReadCity]...';
GO

PRINT N'Creating [dbo].[spCityStateReadCityState]...';
GO

PRINT N'Creating [dbo].[spCityStateReadState]...';
GO

PRINT N'Creating [dbo].[spCityStateUpdate]...';
GO

PRINT N'Creating [dbo].[spDeleteBooking]...';
GO

PRINT N'Creating [dbo].[spDeleteEventItem]...';
GO

PRINT N'Creating [dbo].[spDeleteItemListing]...';
GO

PRINT N'Creating [dbo].[spDeleteSupplier]...';
GO

PRINT N'Creating [dbo].[spDeleteSupplierFeedbackRecord]...';
GO

PRINT N'Creating [dbo].[spEmployeeList]...';
GO

PRINT N'Creating [dbo].[spHotelGuestAdd]...';
GO

PRINT N'Creating [dbo].[spHotelGuestGet]...';
GO

PRINT N'Creating [dbo].[spHotelGuestUpdate]...';
GO

PRINT N'Creating [dbo].[spInsertEventItem]...';
GO

PRINT N'Creating [dbo].[spInsertItemListing]...';
GO

PRINT N'Creating [dbo].[spInsertSupplier]...';
GO

PRINT N'Creating [dbo].[spInsertSupplierFeedbackRecord]...';
GO

PRINT N'Creating [dbo].[spSelectAllBookings]...';
GO

PRINT N'Creating [dbo].[spSelectAllEventItems]...';
GO

PRINT N'Creating [dbo].[spSelectAllEventTypes]...';
GO

PRINT N'Creating [dbo].[spSelectAllItemListings]...';
GO

PRINT N'Creating [dbo].[spSelectAllLists]...';
GO

PRINT N'Creating [dbo].[spSelectAllSupplierFeedbackRecords]...';
GO

PRINT N'Creating [dbo].[spSelectAllSuppliers]...';
GO

PRINT N'Creating [dbo].[spSelectBooking]...';
GO

PRINT N'Creating [dbo].[spSelectBookingFull]...';
GO

PRINT N'Creating [dbo].[spSelectEmployee]...';
GO

PRINT N'Creating [dbo].[spSelectEmployeeWithID]...';
GO

PRINT N'Creating [dbo].[spSelectEventItem]...';
GO

PRINT N'Creating [dbo].[spSelectEventType]...';
GO

PRINT N'Creating [dbo].[spSelectItemListing]...';
GO

PRINT N'Creating [dbo].[spSelectLists]...';
GO

PRINT N'Creating [dbo].[spSelectSupplier]...';
GO

PRINT N'Creating [dbo].[spSelectSupplierFeedbackRecord]...';
GO

PRINT N'Creating [dbo].[spUpdateBooking]...';
GO

PRINT N'Creating [dbo].[spUpdateEmployee]...';
GO

PRINT N'Creating [dbo].[spUpdateEventItem]...';
GO

PRINT N'Creating [dbo].[spUpdateEventType]...';
GO

PRINT N'Creating [dbo].[spUpdateItemListing]...';
GO

PRINT N'Creating [dbo].[spUpdateLists]...';
GO

PRINT N'Creating [dbo].[spUpdateSupplier]...';
GO

PRINT N'Creating [dbo].[spUpdateSupplierFeedbackRecord]...';
GO

/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [com.WanderingTurtle.EventDatabase];
GO

INSERT INTO  [dbo].[CityState] (Zip, City, State)
VALUES
('52641', 'Mt. Pleasant', 'IA'),
('52404', 'Cedar Rapids', 'IA'),
('10007', 'New York City', 'NY'),
('10001', 'New York City', 'NY'),
('52014', 'Chiliton', 'WI'),
('58214', 'Arvilla', 'ND'),
('50229', 'Pleasantville', 'IA'),
('12235', 'Albany', 'NY'),
('73102', 'Oklahoma City', 'OK'),
('73078', 'Piedmont', 'OK')
GO

INSERT INTO  [dbo].[Employee]  (firstName, lastName, empPassword, empLevel, active)
VALUES
('Kevin', 'Keene', 'Nin10doh', 1, DEFAULT),
('Ryan', 'Higa', 'F13mbl3m', 2, DEFAULT),
('Justin', 'Timberlake', 'S1ng3r', 3, DEFAULT),
('Megumi', 'Aino', 'Pr3Cur3', 4, DEFAULT),
('Natalie', 'Blackstone', 'M@xH3@rt', 4, DEFAULT),
('Hikari', 'Tomotachi', 'Lum1n0us', 3, DEFAULT),
('Hannah', 'Whitehouse', 'Bl@Wh1', 2, DEFAULT), 
('Saki', 'Mai', '$p1@$h', 1, DEFAULT),
('Chrom', 'Lowell', '@w@k3n1n', 3, DEFAULT),
('Robin', 'Lowell', '@uth0r3$', 4, DEFAULT)
GO

INSERT INTO [dbo].[HotelGuest] (FirstName, LastName, Zip, Address1, Address2, PhoneNumber, EmailAddress)
VALUES
('David', 'Tennant', '52404', '234 33rd Ave SW', ' ', '319-258-4566', 'comewithme@yahoo.com'),
('Edward', 'Elric', '52641', '2234 Benton Ave', ' ', ' ', ' '),
('Alphonse', 'Elric', '10001', '123 Wall St.', 'Apt. 114', ' ', 'fullmetal@gmail.com'),
('Ichigo', 'Kurasaki', '10007', '4567 Broadway Ave.', ' ', '123-145-9908',' '),
('Rose', 'Tyler', '73078', '6453 Benton Ave.', ' ','223-456-1234', ' '),
('Martha', 'Jones', '73078', '6453 Benton Ave.', ' ', ' ', ' '),
('Susan', 'Foreman', '73102', '3345 Main St.', 'Apt. 4432', ' ', ' '),
('Barbara', 'Wright', '73102', '5634 Main St.',' ', '456-223-1234', ' '),
('Peter', 'Capaldi', '52641', '2232 Benton Ave.', ' ', '319-217-4455', 'doctorwho@kirkwood.edu'),
('Clara', 'Oswald', '52641', '1455 Benton. Ave', ' ', '319-931-9983', ' ')
GO

INSERT INTO [dbo].[Supplier]  (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, UserID, Active)
VALUES
('Francisco''s Tours', 'Francisco', 'McHurdley', '255 East West St', ' ', '66685', '555-542-8796', 'franciscotours@gmail.com', 100, 101, 1),
('Harry''s Boat Rides', 'Harry', 'Bertleson', '19925 Wilmington Ave', 'Suite 206', '66686',  '555-874-9663', 'harrythehammer@gmail.com', 101, 102, 1),
('They''re Grape Tours, LLC', 'Gregory', 'Allensworth', '1644 East Central Way', ' ', '66685', '555-766-1124', 'info@theyregrapetours.com', 102, 103, 1)
GO

INSERT INTO [dbo].[SupplierApplication]  (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationDate, Approved, ApprovalDate)
VALUES
('Francisco'' Tours', 'Francisco', 'McHurdley', '255 East West St', ' ', '50229', '555-542-8796', 'franciscotours@gmail.com', '2014-12-22', 1, '2015-01-01'),
('Harry''s Boat Rides', 'Harry', 'Bertleson', '19925 Wilmington Ave', 'Suite 206', '50229',  '555-874-9663', 'harrythehammer@gmail.com', '2014-02-06', 1, '2014-06-12'),
('They''re Grape Tours, LLC', 'Gregory', 'Allensworth', '1644 East Central Way', ' ', '50229', '555-766-1124', 'info@theyregrapetours.com', '2015-01-22', 1, '2015-02-12')
GO

INSERT INTO [dbo].[EventItem] (EventItemName, EventTypeID, EventOnsite, Transportation, EventDescription, Active)
VALUES
('Non-Creepy Boat Ride', 100, 0, 0, 'A totally non creepy midnight boat ride down the river.', 1 ),
('Jungle Tour', 101, 0, 1, 'A Majestic Jungle Tour. Not safe for kids under 12.', 1 ),
('Market Excursion',102, 0, 1, 'Tour the local marketplace. Haggle your way to some great stuff!', 1 )
GO

INSERT INTO [dbo].[EventType] (EventName)
VALUES
('Boat Ride'),
('Tour'),
('Dinner'),
('Entertainment')
GO

INSERT INTO [dbo].[ItemListing] (StartDate, EndDate, EventItemID, Price, QuantityOffered, ProductSize, Active, SupplierID)
VALUES
('2015-03-01', '2015-03-01', 100, 15.00, 10, 'One Boat', 1, 100),
('2015-03-01', '2015-03-01', 101, 25.00, 30, 'One Excursion', 1, 100),
('2015-03-01', '2015-03-01', 102, 5.00, 50, 'One Trip', 1, 100)
GO

INSERT INTO [dbo].[Lists]  (SupplierID, ItemListID, DateListed)
VALUES
(100,100, GETDATE()),
(101,101, GETDATE()),
(102,102, GETDATE())
GO

INSERT INTO [dbo].[Booking] (GuestID, EmployeeID, ItemListID, Quantity, DateBooked)
VALUES
(1, 103, 100, 6, CURRENT_TIMESTAMP),
(2, 101, 101, 2, CURRENT_TIMESTAMP),
(3, 102, 102, 5, CURRENT_TIMESTAMP),
(4, 104, 102, 1, CURRENT_TIMESTAMP)
GO

PRINT N'Update complete.';
GO

/*
Deployment script for com.WanderingTurtle.EventDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/


GO

DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END



GO

:setvar DatabaseName "com.WanderingTurtle.EventDatabase"
:setvar DefaultFilePrefix "com.WanderingTurtle.EventDatabase"
:setvar DefaultDataPath "c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"


GO

--Syntax Error: Incorrect syntax near :.
--:setvar DatabaseName "com.WanderingTurtle.EventDatabase"
--:setvar DefaultFilePrefix "com.WanderingTurtle.EventDatabase"
--:setvar DefaultDataPath "c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"
--:setvar DefaultLogPath "c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\"
--

GO

:on error exit

GO

--Syntax Error: Incorrect syntax near :.
--:on error exit

GO

/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"

GO

--Syntax Error: Incorrect syntax near :.
--/*
--Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
--To re-enable the script after enabling SQLCMD mode, execute the following:
--SET NOEXEC OFF; 
--*/
--:setvar __IsSqlCmdEnabled "True"



GO
