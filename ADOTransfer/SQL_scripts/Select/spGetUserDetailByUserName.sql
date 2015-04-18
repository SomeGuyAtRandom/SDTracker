USE [SignalDB];

IF OBJECT_ID('spGetUserDetailByUserName', 'P') IS NOT NULL
DROP PROCEDURE spGetUserDetailByUserName;

GO

CREATE PROC spGetUserDetailByUserName  
@UserName nvarchar(30)
AS
BEGIN  
 SELECT 
	 UserPasswords.Id,
	 UserPasswords.UserName,
	 UserPasswords.Password,
	 Engineers.FirstName,
	 Engineers.LastName,
	 Engineers.Initials,
	 Engineers.Email,
	 Engineers.Phone,
	 Engineers.DateCreated,
	 UserPasswords.DateUpdated,
	 UserPasswords.DateAccessed
 FROM Engineers 
 INNER JOIN UserPasswords
 ON UserPasswords.Id = Engineers.Id
 WHERE UserPasswords.UserName=@UserName
END

GO

