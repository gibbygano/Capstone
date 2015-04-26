CREATE PROCEDURE [dbo].[spSelectCityStateList] 
AS
	SELECT Zip, City, State 
	FROM CityState