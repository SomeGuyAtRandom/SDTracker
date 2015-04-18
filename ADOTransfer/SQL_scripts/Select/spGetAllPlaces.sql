USE [SignalDB];

IF OBJECT_ID('spGetAllPlaces', 'P') IS NOT NULL
DROP PROCEDURE spGetAllPlaces;

GO

CREATE PROC spGetAllPlaces  
AS
BEGIN  
 SELECT TOP 100 * FROM Places  ORDER BY PlaceLocation  
 END;