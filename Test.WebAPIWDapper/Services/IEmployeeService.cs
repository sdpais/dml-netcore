using WebAPIWDapper.Models;

namespace WebAPIWDapper.Services;

public interface IEmployeeService
{
    Task<int> CreateEmployee(Employee employee);
    Task<Employee> GetEmployee(int id);
    Task<Employee> GetEmployee(string email);
    Task<List<Employee>> GetEmployeeList();
    Task<Employee> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(int key);
    
}