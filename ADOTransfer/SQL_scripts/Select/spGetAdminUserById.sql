USE [SignalDB];

IF OBJECT_ID('spGetAdminUserById', 'P') IS NOT NULL
DROP PROCEDURE spGetAdminUserById;

GO

CREATE PROC spGetAdminUserById  
@Id int
AS
BEGIN  
 
  
 SELECT UserPasswords.Id, 
Engineers.FirstName, Engineers.LastName, Engineers.Email, Engineers.Initials, Engineers.Phone,
UserPasswords.UserName, UserPasswords.IsDisabled, UserPasswords.DateCreated, UserPasswords.DateUpdated 
FROM UserPasswords   
	LEFT JOIN Engineers 
	ON Engineers.Id = UserPasswords.Id
WHERE  UserPasswords.Id=@Id;


END
GO

