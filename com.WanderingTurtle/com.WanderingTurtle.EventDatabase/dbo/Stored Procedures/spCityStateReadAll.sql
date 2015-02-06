CREATE PROCEDURE [dbo].[spCityStateReadAll] 
AS
	SELECT Zip, City, State 
	FROM CityState