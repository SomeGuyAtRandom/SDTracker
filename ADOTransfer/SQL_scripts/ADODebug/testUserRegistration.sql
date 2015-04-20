USE [SignalDB];
-- use in conjunction with testRemoveUserAccount

-- Test with :
-- UserName : account
-- Email    : aa@bb.com
-- Password : Account@1

IF OBJECT_ID('testUserRegistration', 'P') IS NOT NULL
DROP PROCEDURE testUserRegistration;

GO
	
EXEC testRemoveUserAccount @UserName='account'
GO


CREATE PROC testUserRegistration(@UserName nvarchar(30))
AS
BEGIN
DECLARE @Id int
BEGIN
	SELECT * FROM Engineers WHERE UserName=@UserName;
	SELECT * FROM UserPasswords WHERE UserName=@UserName;
	SELECT @Id =Id FROM UserPasswords WHERE UserName=@UserName;
	SELECT * FROM UserRoles WHERE UserId= @Id;
END
END
GO

EXEC testUserRegistration  @UserName='account'
GO

-- DROP PROCEDURE testUserRegistration;
GO
