USE [SignalDB];

IF OBJECT_ID('spGetAllUserDetails', 'P') IS NOT NULL
DROP PROCEDURE spGetAllUserDetails;

GO

CREATE PROC spGetAllUserDetails  
AS
BEGIN  
 SELECT UserPasswords.Id, UserPasswords.UserName, Password,
 FirstName,LastName,Email,Initials,Phone, 
 UserPasswords.DateCreated,
 UserPasswords.DateUpdated,
 UserPasswords.DateAccessed

 FROM UserPasswords   
 Left JOIN Engineers
 ON UserPasswords.Id = Engineers.Id
END;







