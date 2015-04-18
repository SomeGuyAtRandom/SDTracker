USE [SignalDB];
GO

IF OBJECT_ID('spUpdateUserDetail', 'P') IS NOT NULL
DROP PROCEDURE spUpdateUserDetail;

GO
	
CREATE PROC spUpdateUserDetail

@Id int,
@UserName nvarchar(20),
@PassWord nvarchar(20),

@FirstName nvarchar(30)= null,
@LastName nvarchar(30)= null,
@Email nvarchar(100)= null,
@Initials nvarchar(5)= null,
@Phone nvarchar(20)

AS
BEGIN  

	UPDATE UserPasswords
	SET 
		UserName= @UserName,
		PassWord= @PassWord,
		DateUpdated= GETDATE()
	WHERE
		Id = @Id;

	UPDATE Engineers
	SET 
		FirstName= @FirstName,
		LastName= @LastName,
		Email= @Email,
		Initials= @Initials,
		UserName= @UserName,
		Phone= @Phone,
		DateUpdated= GETDATE()
	WHERE
		Id = @Id;
 END;

GO
