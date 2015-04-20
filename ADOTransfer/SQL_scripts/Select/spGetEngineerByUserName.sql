USE [SignalDB];

IF OBJECT_ID('spGetEngineerByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetEngineerByUserName;

GO

CREATE PROC spGetEngineerByUserName  
@UserName nvarchar(30)
AS
BEGIN  
 SELECT * FROM Engineers   
 WHERE UserName =@UserName 
END
GO
