using System;

namespace BusinessLayer.DbInterfaces
{
    public interface IUserDb
    {
        Boolean InitialsExixts(string Initials);
        Boolean UserExists(String UserName);

    }
}
