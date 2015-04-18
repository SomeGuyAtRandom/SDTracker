USE [SignalDB];

IF OBJECT_ID('spGetUserPasswordByInitials', 'P') IS NOT NULL
DROP PROCEDURE spGetUserPasswordByInitials;

GO

CREATE PROC spGetUserPasswordByInitials  
@Initials nvarchar(5)
AS
BEGIN  
 SELECT * FROM Engineers   
 WHERE Initials=@Initials
END

GO
