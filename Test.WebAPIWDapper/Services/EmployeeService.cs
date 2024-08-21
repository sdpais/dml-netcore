using WebAPIWDapper.Models;


namespace WebAPIWDapper.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IDbService _dbService;

    public EmployeeService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task<int> CreateEmployee(Employee employee)
    {
        var result =
            await _dbService.EditData(
                "INSERT INTO public.employees (name, email, dateofbirth, mobilenumber, encryptionkeyid, ssn, createddate, updateddate) VALUES (@Name, @email, @DateOfBirth, @MobileNumber, @encryptionkeyid, @ssn, @createddate, @updateddate) ; SELECT currval(pg_get_serial_sequence('employees','id'));",
                employee);
        return result;
    }

    public async Task<List<Employee>> GetEmployeeList()
    {
        var employeeList = await _dbService.GetAll<Employee>("SELECT * FROM public.employees", new { });
        return employeeList;
    }


    public async Task<Employee> GetEmployee(int id)
    {
        var employee = await _dbService.GetAsync<Employee>("SELECT * FROM public.employees where id=@id", new {id});
        return employee;
    }
    public async Task<Employee> GetEmployee(string email)
    {
        var employee = await _dbService.GetAsync<Employee>("SELECT * FROM public.employees where email=@email", new { email});
        return employee;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        var updateEmployee =
            await _dbService.EditData(
                "Update public.employees SET name=@Name, email=@email, dateofbirth=@dateofbirth, mobile_number=@MobileNumber, encryptionkeyid=@encryptionkeyid, ssn=@ssn, updateddate=@updateddate WHERE id=@Id",
                employee);
        return employee;
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var deleteEmployee = await _dbService.EditData("DELETE FROM public.employees WHERE id=@Id", new {id});
        return true;
    }
}