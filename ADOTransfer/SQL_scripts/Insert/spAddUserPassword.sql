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
DECLARE @Id int
DECLARE @table table (id int)

 INSERT INTO UserPasswords 
 (UserName,Password,IsDisabled,DateCreated,DateUpdated,DateAccessed )  
 OUTPUT inserted.id into @table
 VALUES (@UserName,@Password,0,GETDATE(), GETDATE(), GETDATE())  ;

 SELECT @Id = id from @table
 
  INSERT INTO Engineers 
 (Id,FirstName,LastName,Email,Initials,UserName,Phone,
 DateCreated,DateUpdated )  
 VALUES ( @Id, @FirstName,@LastName,@Email,@Initials,@UserName,@Phone,GETDATE(), GETDATE());

 -- RoleId=2 is the Guest Role
 INSERT INTO UserRoles(UserId,RoleId) VALUES (@Id,2);

 END

GO
