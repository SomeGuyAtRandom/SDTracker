USE [SignalDB];

IF OBJECT_ID('spGetAllDistricts', 'P') IS NOT NULL
DROP PROCEDURE spGetAllDistricts;

GO

CREATE PROC spGetAllDistricts  
AS
BEGIN  
 SELECT * FROM Districts WHERE Id > 0  
END;