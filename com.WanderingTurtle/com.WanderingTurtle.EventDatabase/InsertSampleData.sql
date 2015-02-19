﻿/*
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

INSERT INTO [dbo].[EventItem] (EventItemName, EventStartTime, EventEndTime, CurrentNumberOfGuests, MaxNumberOfGuests, MinNumberOfGuests, EventTypeID, PricePerPerson, EventOnsite, Transportation, EventDescription, Active)
VALUES
('Non-Creepy Boat Ride', '00:00:00', '01:00:00', 0, 10, 1, 100, 15.00, 0, 0, 'A totally non creepy midnight boat ride down the river.', 1 ),
('Jungle Tour', '13:00:00', '15:00:00', 2, 30, 5, 101, 25.00, 0, 1, 'A Majestic Jungle Tour. Not safe for kids under 12.', 1 ),
('Market Excursion', '10:00:00', '12:00:00', 5, 50, 2, 101, 5.00, 0, 1, 'Tour the local marketplace. Haggle your way to some great stuff!', 1 )
GO

INSERT INTO [dbo].[EventType] (EventName)
VALUES
('Boat Ride'),
('Tour'),
('Dinner'),
('Entertainment')
GO

INSERT INTO [dbo].[ItemListing] (StartDate, EndDate, EventItemID, Price, QuantityOffered, ProductSize, Active)
VALUES
('2015-03-01', '2015-03-01', 100, 15.00, 10, 'One Boat', 1),
('2015-03-01', '2015-03-01', 101, 25.00, 30, 'One Excursion', 1),
('2015-03-01', '2015-03-01', 102, 5.00, 50, 'One Trip', 1)
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

INSERT INTO [dbo].[Employee] (firstName, lastName, userID, Active)
VALUES
('Phil', 'Robinson', 100, 1),
('Benny', 'Albertson', 104, 1),
('Suzanne', 'Colletes', 105, 1)
GO