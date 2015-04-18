--spDeleteEngineerByUserName

USE [SignalDB];

IF OBJECT_ID('spDeleteEngineerByUserName', 'P') IS NOT NULL
DROP PROCEDURE spDeleteEngineerByUserName;

GO

CREATE PROC spDeleteEngineerByUserName
@UserName nvarchar(30)
AS
BEGIN  
 DELETE FROM Engineers
 WHERE UserName=@UserName;

 DELETE FROM UserPasswords
 WHERE UserName=@UserName;
    
END

GO
