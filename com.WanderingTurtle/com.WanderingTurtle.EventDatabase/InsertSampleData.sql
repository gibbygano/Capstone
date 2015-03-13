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

INSERT INTO  [dbo].[CityState] (Zip, City, State)
VALUES
('52641', 'Mt. Pleasant', 'IA'),
('52404', 'Cedar Rapids', 'IA'),
('10007', 'New York City', 'NY'),
('10001', 'New York City', 'NY'),
('52014', 'Chilton', 'WI'),
('58214', 'Arvilla', 'ND'),
('50229', 'Pleasantville', 'IA'),
('12235', 'Albany', 'NY'),
('73102', 'Oklahoma City', 'OK'),
('73078', 'Piedmont', 'OK')
GO

INSERT INTO  [dbo].[Employee]  (firstName, lastName, empPassword, empLevel, active)
VALUES
('Admin', 'Test', 'test', 1, DEFAULT),
('Concierge', 'Test', 'test', 2, DEFAULT),
('DeskClerk', 'Test', 'test', 3, DEFAULT),
('Valet', 'Test', 'test', 4, DEFAULT),
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

INSERT INTO [dbo].[HotelGuest] (FirstName, LastName, Zip, Address1, Address2, PhoneNumber, EmailAddress, Room)
VALUES
('David', 'Tennant', '52404', '234 33rd Ave SW', ' ', '(319) 258-4566', 'comewithme@yahoo.com', 101),
('Edward', 'Elric', '52641', '2234 Benton Ave', ' ', ' ', ' ', 102),
('Alphonse', 'Elric', '10001', '123 Wall St', 'Apt 114', ' ', 'fullmetal@gmail.com', 103),
('Ichigo', 'Kurasaki', '10007', '4567 Broadway Ave', ' ', '(223) 145-9908',' ', 104),
('Rose', 'Tyler', '73078', '6453 Benton Ave', ' ','(223) 456-1234', ' ', 105),
('Martha', 'Jones', '73078', '6453 Benton Ave', ' ', ' ', ' ', 201),
('Susan', 'Foreman', '73102', '3345 Main St', 'Apt. 4432', ' ', ' ', 202),
('Barbara', 'Wright', '73102', '5634 Main St',' ', '(456) 223-1234', ' ', 203),
('Peter', 'Capaldi', '52641', '2232 Benton Ave', ' ', '(319) 217-4455', 'doctorwho@kirkwood.edu', 204),
('Clara', 'Oswald', '52641', '1455 Benton Ave', ' ', '(319) 931-9983', ' ', 205)
GO
INSERT INTO [dbo].[Supplier]  (CompanyName, FirstName, LastName, Address1, Address2, Zip, PhoneNumber, EmailAddress, ApplicationID, UserID, Active)
VALUES
('Francisco''s Tours', 'Francisco', 'McHurdley', '255 East West St', ' ', '50229', '555-542-8796', 'franciscotours@gmail.com', 100, 101, 1),
('Harry''s Boat Rides', 'Harry', 'Bertleson', '19925 Wilmington Ave', 'Suite 206', '50229',  '555-874-9663', 'harrythehammer@gmail.com', 101, 102, 1),
('They''re Grape Tours, LLC', 'Gregory', 'Allensworth', '1644 East Central Way', ' ', '50229', '555-766-1124', 'info@theyregrapetours.com', 102, 103, 1)
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
('Market Excursion', 102, 0, 1, 'Tour the local marketplace. Haggle your way to some great stuff!', 1 )
GO

INSERT INTO [dbo].[EventType] (EventName)
VALUES
('Boat Ride'),
('Tour'),
('Dinner'),
('Entertainment')
GO

INSERT INTO [dbo].[ItemListing] (StartDate, EndDate, EventItemID, Price, Active, SupplierID, CurrentNumberOfGuests, MaxNumberOfGuests)
VALUES
('20150313 11:30:00 AM', '20150313 05:30:00 PM', 100, 15.00, 1, 100, 30, 50),
('20150314 01:30:00 PM', '20150314 07:00:00 PM', 101, 25.00, 1, 101, 10, 15),
('20150315 10:34:00 AM', '20150315 10:00:00 PM', 102, 5.00, 1, 102, 80, 90),
('20150316 10:00:00 AM', '20150316 10:15:00 PM', 101, 20.00, 1, 100, 2, 5),
('20150328 08:30:00 AM', '20150328 10:30:00 AM', 102, 10.00, 1, 102, 10,11),
('20150321 09:30:00 AM', '20150321 11:45:00 AM', 102, 10.00, 1, 102, 2,11),
('20150324 08:30:00 AM', '20150324 09:30:00 AM', 100, 30.00, 1, 100, 30, 50),
('20150401 10:30:00 AM', '20150401 12:30:00 PM', 101, 35.00, 1, 101, 10, 15),
('20150401 10:00:00 AM', '20150402 10:00:00 AM', 102, 5.00, 1, 102, 12, 20)
GO

INSERT INTO [dbo].[Lists]  (SupplierID, ItemListID, DateListed)
VALUES
(100,100, GETDATE()),
(101,101, GETDATE()),
(102,102, GETDATE())
GO

INSERT INTO [dbo].[Booking] (GuestID, EmployeeID, ItemListID, Quantity, DateBooked, Discount, Active, TicketPrice, ExtendedPrice, TotalCharge)
VALUES
(1, 103, 100, 6, CURRENT_TIMESTAMP, DEFAULT, DEFAULT, 15.00, 90.00, 90.00),
(2, 101, 101, 2, CURRENT_TIMESTAMP, DEFAULT, DEFAULT, 25.00, 50.00, 50.00) ,
(3, 102, 102, 5, CURRENT_TIMESTAMP, DEFAULT, DEFAULT, 5.00, 25.00, 25.00 ),
(4, 104, 102, 1,'2015-03-04', DEFAULT, DEFAULT, 5.00, 5.00, 5.00),
(4, 104, 101, 4, CURRENT_TIMESTAMP, DEFAULT, DEFAULT, 25.00, 100.00, 100.00),
(6, 104, 103, 4, '2015-03-04', DEFAULT, DEFAULT, 20.00, 80.00, 80.00),
(7, 104, 104, 10, '2015-03-04', .10, DEFAULT, 9.00, 90.00, 90.00)
GO
INSERT INTO [dbo].[Invoice] (HotelGuestID, Active, DateOpened)
VALUES
(0, DEFAULT, CURRENT_TIMESTAMP),
(1, DEFAULT, CURRENT_TIMESTAMP),
(2, DEFAULT, CURRENT_TIMESTAMP),
(3, DEFAULT, CURRENT_TIMESTAMP),
(4, DEFAULT, CURRENT_TIMESTAMP),
(5, DEFAULT, CURRENT_TIMESTAMP),
(6, DEFAULT, CURRENT_TIMESTAMP),
(7, DEFAULT, CURRENT_TIMESTAMP),
(8, DEFAULT, CURRENT_TIMESTAMP),
(9, DEFAULT, CURRENT_TIMESTAMP)
GO
