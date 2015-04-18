using System;
using System.Collections.Generic;
using BusinessLayer.Models;
using BusinessLayer.DbImp;
using BusinessLayer.DbInterfaces;


namespace BusinessLayer.MockDbs
{
    public class AdminDb : IAdminDb
    {
        private AdminDbImp context = new AdminDbImp();

        public AdminUser getAdminUserById(int Id)
        {
            return context.getAdminUserById(Id);
        }

        public IEnumerable<AdminUser> GetUserWithSearch(string s)
        {
            return context.GetUserWithSearch(s);

        }

        public Boolean EnableDisableUser(int id, bool isDisabled)
        {
            return context.EnableDisableUser(id, isDisabled);
        }

        public Boolean AddRemoveRole(int id, string roleName, bool isMember)
        {
            return context.AddRemoveRole(id, roleName, isMember);
        }

        public Boolean SaveField(int id, string fieldName, string txtValue)
        {
            return context.SaveField(id, fieldName, txtValue);
        }
    }
}
