using System;
using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.DbInterfaces
{
    public interface IHomeDb
    {
        Boolean AddNewUser(RegisterUser user);
        UserDetail GetUserDetail(String UserName);
        Boolean DoAuthen();

        Boolean UserIsValid(UserLogin model);
        IEnumerable<Role> GetRoles(String UserName);

        String ValidateUserNameAtServer(String UserName);
        String ValidateEmailAtServer(String Email);
        String ValidatePasswordAtServer(String Password);

        Boolean IsServerSideValid(RegisterUser user);


        Engineer getEngineerByUserName(String UserName);
    }
}
