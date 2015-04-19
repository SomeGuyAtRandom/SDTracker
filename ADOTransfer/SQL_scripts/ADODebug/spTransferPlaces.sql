USE [SignalDB];

IF OBJECT_ID('spTransferPlaces', 'P') IS NOT NULL
DROP PROCEDURE spTransferPlaces;

GO

CREATE PROC spTransferPlaces  
	@FiveDigit nvarchar(10),
	@DistrictId int,
	@PlaceLocation nvarchar(80)
AS
BEGIN  
 INSERT INTO Places 
 (FiveDigit,DistrictId,PlaceLocation,DateCreated,DateUpdated)
 VALUES (@FiveDigit,@DistrictId, @PlaceLocation,GetDate(),GetDate());

END
GO