USE [SignalDB];

IF OBJECT_ID('spGetAllEngineers', 'P') IS NOT NULL
DROP PROCEDURE spGetAllEngineers;

GO

CREATE PROC spGetAllEngineers  
AS
BEGIN  
 SELECT * FROM Engineers   
END;
