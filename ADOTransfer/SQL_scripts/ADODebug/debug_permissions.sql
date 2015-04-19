USE [SignalDB];

UPDATE UserPasswords SET IsDisabled=0;


-- SELECT * FROM UserPasswords;

-- exec spGetUserRoles @UserName = 'account'

exec spUpdateUserRole @Id=24, @roleName='Guest', @isMember = true;

exec spUpdateUserRole @Id=24, @roleName='Admin', @isMember = true;
exec spUpdateUserRole @Id=24, @roleName='DesignEngineer', @isMember = true;
exec spUpdateUserRole @Id=24, @roleName='HeadEngineer', @isMember = true;


-- DELETE FROM UserRoles WHERE  UserId=24 AND RoleId =1
GO

--INSERT INTO UserRoles (UserId, RoleId) VALUES (24,1);
GO



exec spGetUserRoles @UserName = 'account'

go
