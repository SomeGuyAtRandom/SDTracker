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
        Boolean UserExists(String UserName);
        Boolean EmailExists(String Email);

        Boolean UserIsValid(UserLogin model);
        IEnumerable<Role> GetRoles(String UserName);

        String ValidateUserNameSelection(String UserName);
        String ValidateEmailSelection(String Email);

    }
}
