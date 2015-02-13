CREATE PROCEDURE [dbo].[spCityStateReadState] 
	(@State	varchar(50))
AS
	SELECT Zip, City, State 
	FROM CityState 
	WHERE State = @State