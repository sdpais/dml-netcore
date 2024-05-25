using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IDbService _dbService;

    public EmployeeService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<bool> CreateEmployee(Employee employee)
    {
        var result =
            await _dbService.EditData(
                "INSERT INTO public.employees (id,name, age, mobile_number, encryptionkeyid, ssn, createddate) VALUES (@Id, @Name, @Age, @MobileNumber, @encryptionkeyid, @ssn, @createddate)",
                employee);
        return true;
    }

    public async Task<List<Employee>> GetEmployeeList()
    {
        var employeeList = await _dbService.GetAll<Employee>("SELECT * FROM public.employees", new { });
        return employeeList;
    }


    public async Task<Employee> GetEmployee(int id)
    {
        var employeeList = await _dbService.GetAsync<Employee>("SELECT * FROM public.employees where id=@id", new {id});
        return employeeList;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        var updateEmployee =
            await _dbService.EditData(
                "Update public.employees SET name=@Name, age=@Age, mobile_number=@MobileNumber, encryptionkeyid=@encryptionkeyid, ssn=@ssn, createddate=@createddate WHERE id=@Id",
                employee);
        return employee;
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var deleteEmployee = await _dbService.EditData("DELETE FROM public.employees WHERE id=@Id", new {id});
        return true;
    }
}