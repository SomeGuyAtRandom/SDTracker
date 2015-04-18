USE [SignalDB];

IF OBJECT_ID('spGetUserPasswordByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetUserPasswordByUserName;

GO

CREATE PROC spGetUserPasswordByUserName  
@UserName nvarchar(15)
AS
BEGIN  
 SELECT Id FROM UserPasswords   /*Only return the Id - UserName is given and password protected*/
 WHERE UserName=@UserName
END;




