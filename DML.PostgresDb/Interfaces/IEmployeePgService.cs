using DML.Domain.Entities;

namespace DML.PostgresDb.Interfaces;

public interface IEmployeePgService
{
    Task<int> CreateEmployee(Employee employee);
    Task<Employee> GetEmployee(int id);
    Task<Employee> GetEmployee(string email);
    Task<List<Employee>> GetEmployeeList();
    Task<Employee> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(int key);

}