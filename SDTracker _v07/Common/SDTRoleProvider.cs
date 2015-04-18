using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BusinessLayer.DbImp;
using System.Web.Mvc;

namespace SDTracker.Common
{
    public class SDTRoleProvider : RoleProvider
    {

        public static bool UserIsInRole(ViewContext view, string roleName)
        {
            string userName = view.HttpContext.User.Identity.Name;
            UserRoleDbContext db = new UserRoleDbContext();
            string[] roles = roleName.Split(',');

            foreach (string s in roles)
            {
                if (db.IsUserInRole(userName, s))
                {
                    return true;
                }
            }

            return false;


        }

        public static bool UserIsInRole(string userName, string roleName) 
        {
            UserRoleDbContext db = new UserRoleDbContext();
            string[] roles = roleName.Split(',');

            foreach (string s in roles)
            {
                if (db.IsUserInRole(userName, s))
                {
                    return true;
                }
            }

            return false;


        }
        private UserRoleDbContext dbDbContext;
        public override string[] GetRolesForUser(string username)
        {
            dbDbContext = new UserRoleDbContext();
            return dbDbContext.GetUserRoles(username);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            dbDbContext = new UserRoleDbContext();
            string[] roles = roleName.Split(',');

            foreach (string s in roles)
            {
                if (dbDbContext.IsUserInRole(username, s))
                {
                    return true;
                }
            }

            return false;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

    }
}