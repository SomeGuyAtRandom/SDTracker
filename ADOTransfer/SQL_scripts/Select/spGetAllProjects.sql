USE [SignalDB];

IF OBJECT_ID('spGetAllProjects', 'P') IS NOT NULL
DROP PROCEDURE spGetAllProjects;

GO

CREATE PROC spGetAllProjects  
AS
BEGIN  
 SELECT TOP 100 * FROM Projects ORDER BY Id
 END;