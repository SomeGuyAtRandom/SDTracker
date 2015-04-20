using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.DbInterfaces;
using BusinessLayer.DbImp;


namespace BusinessLayer.MockDbs
{
    public class UserDb : IUserDb
    {
        private UserDbImp context = new UserDbImp();

        public Boolean InitialsExixts(string Initials)
        {
            return context.InitialsExixts(Initials);
        }
        public Boolean UserExists(String UserName)
        {
            return context.UserExists(UserName);
        }

        public bool SaveField(int id, string FieldName, string Value)
        {
            return context.SaveField( id,  FieldName,  Value);
        }

    }
}
