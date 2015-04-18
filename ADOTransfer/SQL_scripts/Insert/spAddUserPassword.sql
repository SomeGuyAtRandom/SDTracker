USE [SignalDB];
GO

IF OBJECT_ID('spAddUserPassword', 'P') IS NOT NULL
DROP PROCEDURE spAddUserPassword;

GO

CREATE PROC spAddUserPassword

@UserName nvarchar(20),
@Password nvarchar(20),

@FirstName nvarchar(30)= null,
@LastName nvarchar(30)= null,
@Email nvarchar(100)= null,
@Initials nvarchar(5)= null,
@Phone nvarchar(20)= null

AS
BEGIN  

DECLARE @ReturnValue int = 0

 INSERT INTO UserPasswords 
 (UserName,Password,DateCreated,DateUpdated,DateAccessed )  
 VALUES (@UserName,@Password,GETDATE(), GETDATE(), GETDATE())  ;

  INSERT INTO Engineers 
 (Id,FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated )  
 VALUES ( @@IDENTITY, @FirstName,@LastName,@Email,@Initials,@UserName,@Phone,GETDATE(), GETDATE())  ;

 END;

GO
