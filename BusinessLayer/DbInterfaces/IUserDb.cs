using System;
using BusinessLayer.Models;

namespace BusinessLayer.DbInterfaces
{
    public interface IUserDb
    {
        Boolean InitialsExixts(string Initials);
        Boolean UserExists(String UserName);
        bool SaveField(int id, string FieldName, string Value);
        bool ChangePassword(UserPassword user);
        Engineer GetEngineerByUserName(String UserName);
    }
}
