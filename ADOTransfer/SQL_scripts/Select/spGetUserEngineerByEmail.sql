USE [SignalDB];

IF OBJECT_ID('spGetUserEngineerByEmail', 'P') IS NOT NULL
DROP PROCEDURE spGetUserEngineerByEmail;

GO

CREATE PROC spGetUserEngineerByEmail  
@Email nvarchar(100)
AS
BEGIN  
 SELECT Id FROM Engineers   
 WHERE Email=@Email
END
GO



