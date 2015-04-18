USE [SignalDB];

IF OBJECT_ID('spGetAllJobTypes', 'P') IS NOT NULL
DROP PROCEDURE spGetAllJobTypes;

GO

CREATE PROC spGetAllJobTypes  
AS
BEGIN  
 SELECT * FROM JobTypes   
END;