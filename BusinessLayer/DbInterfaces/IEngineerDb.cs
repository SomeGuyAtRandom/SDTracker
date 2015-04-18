using BusinessLayer.Models;

namespace BusinessLayer.DbInterfaces
{
    public interface IEngineerDb 
    {
        Engineer getEngineerUserId(int Id);
        
        bool SaveField(int id, string fieldName, string txtValue);
    }
}
