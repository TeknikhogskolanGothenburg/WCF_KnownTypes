using System.ServiceModel;


namespace EmployeeService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        Employee GetEmployee(int id);

        [OperationContract]
        void SaveEmployee(Employee employee);
    }
}
