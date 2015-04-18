using System;
using BusinessLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.DbInterfaces
{
    public interface IAdminDb
    {
        AdminUser getAdminUserById(int Id);
        IEnumerable<AdminUser> GetUserWithSearch(string searchTerm);
        Boolean EnableDisableUser(int Id, bool isDisabled);
        Boolean AddRemoveRole(int iId, string roleName, bool bIsMember);
        Boolean SaveField(int id, string fieldName, string txtValue);
    }
}
