USE [SignalDB];

-- Test with :
-- UserName : account
-- Email    : aa@ab.com
-- Password : Account@1

SELECT * FROM Engineers

-- SELECT * FROM Roles
-- EXEC testRemoveUserAccount @UserName='account'
GO

--EXEC spAddUserPassword  @UserName='account', @Password='BettyDavis', @Email='xxx1'
GO

-- Old password QWNjb3VudEAx
-- Password#1 UGFzc3dvcmQjMQ==
-- 
-- UPDATE UserPasswords SET Password='UGFzc3dvcmQjMQ==' WHERE Id=15;
EXEC testUserRegistration  @UserName='jvarghese'


GO

