using BusinessLayer.Models;
using BusinessLayer.DbImp;
using BusinessLayer.DbInterfaces;

namespace BusinessLayer.MockDbs
{
    public class EngineerDb : IEngineerDb
    {
        private EngineerDbImp context = new EngineerDbImp();
        public Engineer getEngineerUserId(int Id) 
        {
            return context.getEngineerUserId(Id);
        }
        public bool SaveField(int id, string fieldName, string txtValue)
        {
            return context.SaveField(id, fieldName, txtValue);
        }

    }
}
