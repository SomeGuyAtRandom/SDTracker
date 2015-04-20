using System;
using BusinessLayer.DbImp;
using BusinessLayer.Models;
using System.Collections.Generic;
using BusinessLayer.DbInterfaces;

namespace BusinessLayer.MockDbs
{
    public class HomeDb : IHomeDb
    {
        private HomeDbImp context = new HomeDbImp();

        public String ValidateEmailAtServer(String Email)
        {
            return context.ValidateEmailAtServer(Email);
        }

        public String ValidateUserNameAtServer(String UserName)
        {
            return context.ValidateUserNameAtServer(UserName);
 
        }
        
        public Boolean AddNewUser(RegisterUser user)
        {
            return context.AddNewUser(user);
        }

        public UserDetail GetUserDetail(String UserName)
        {
            return context.GetUserDetail(UserName);
        }
        
        public Boolean DoAuthen()
        {
            return context.DoAuthen();
        }

        

        public String ValidatePasswordAtServer(String Password)
        {
            return context.ValidatePasswordAtServer(Password);
        }
        public Boolean UserIsValid(UserLogin model)
        {
            return context.UserIsValid(model);
        }
        
        public IEnumerable<Role> GetRoles(String UserName)
        {
            return context.GetRoles(UserName);
        }

        public Boolean IsServerSideValid(RegisterUser user)
        {
            return context.IsServerSideValid(user);
        }

        public Engineer getEngineerByUserName(String UserName)
        {
            return context.getEngineerByUserName(UserName);
 
        }
    }
}
