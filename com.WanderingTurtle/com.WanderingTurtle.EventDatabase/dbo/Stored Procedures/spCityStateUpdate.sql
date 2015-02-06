CREATE PROCEDURE [dbo].[spCityStateUpdate] 
	(@Zip	varchar(10),
	 @City	varchar(50),
	 @State	varchar(50),
	 @original_City		varchar(50),
	 @original_State	varchar(50))
AS
	UPDATE 	CityState
	   SET 	City 	= @City, 
			State	= @State
	 WHERE	Zip 	= @Zip 
	   AND	City	= @original_City 
	   AND	State 	= @original_State