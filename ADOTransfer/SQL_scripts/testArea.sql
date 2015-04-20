USE [SignalDB];

-- Test with :
-- UserName : account
-- Password : Account@1
-- Email    : aa@ab.com

-- EXEC testRemoveUserAccount @UserName='account'
GO

-- EXEC spAddUserPassword  @UserName='account', @Password='BettyDavis', @Email='xxx1'
GO

-- EXEC testUserRegistration  @UserName='account'
GO

SELECT * FROM UserRoles where UserId >22 OR UserId IS NULL;
DELETE FROM UserRoles WHERE UserId IS NULL;

