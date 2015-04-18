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

        public String ValidateEmailSelection(String Email)
        {
            return context.ValidateEmailSelection(Email);
        }
        public String ValidateUserNameSelection(String UserName)
        {
            return context.ValidateUserNameSelection(UserName);
 
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

        public Boolean UserExists(String UserName)
        {
            return context.UserExists(UserName);
        }

        public Boolean EmailExists(String Email)
        {
            return context.EmailExists(Email);
        }
        
        public Boolean UserIsValid(UserLogin model)
        {
            return context.UserIsValid(model);
        }
        
        public IEnumerable<Role> GetRoles(String UserName)
        {
            return context.GetRoles(UserName);
        }

    }
}
