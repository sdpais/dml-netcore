using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using WebAPIWDapper.Models;
using WebAPIWDapper.Services;
using Newtonsoft.Json;
namespace WebAPIWDapper.BusinessLogic
{
    public class EmployeeBLService : BusinessLogicBLBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeBLService(IConfiguration configuration) : base(configuration)
        {
            _employeeService = new EmployeeService(_dbService);
        }
        public async Task<int?> CreateEmployee(Employee employee)
        {
            
            CryptographicService encryptionDecryptionService = new CryptographicService(_dbService);
            CryptographicBLService cryptographicBLService = new CryptographicBLService(_configuration);
            //find a login matching the email to associate with the employee
            ILoginService _loginService = new LoginService(_dbService);
            EncryptionKey? encryptionKey = null;
            Login login = await _loginService.GetLogin(employee.Email);
            if (login != null)
            {
                employee.EncryptionKeyId = login.EncryptionKeyId;
                //get the Encryptionkey from the cache
                string? encryptionKeyJson = await cryptographicBLService.GetEncryptionKeyValueFromCache(login.EncryptionKeyId);
                if (!string.IsNullOrEmpty(encryptionKeyJson))
                {
                    encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
                }
            }
            else
            {
                string encryptionKeyJson = await cryptographicBLService.GetRandomEncryptionKeyValueFromCache();
                encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            }
            if (encryptionKey is null || encryptionKey.KeyString is null || employee.Name is null)
            {
                //TODO 
                return 0;
            }
            employee.EncryptionKeyId = encryptionKey.Id;
            employee.DateOfBirthEncrypted = await encryptionDecryptionService.Encrypt(employee.DateOfBirth.ToString("yyyy-MM-dd"), encryptionKey.KeyString);
            employee.SSNEncrypted = await encryptionDecryptionService.Encrypt(employee.SSN, encryptionKey.KeyString);
            employee.CreatedDate = DateTime.Now;
            var result = await _employeeService.CreateEmployee(employee);
            if (login != null)
            {
                employee = await GetEmployeeMatching(employee.Email);
                login.EmployeeId = employee.Id;
                await _loginService.UpdateLogin(login);

            }
            return result;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {

            CryptographicService encryptionDecryptionService = new CryptographicService(_dbService);
            CryptographicBLService cryptographicBLService = new CryptographicBLService(_configuration);
            //find a login matching the email to associate with the employee
            ILoginService _loginService = new LoginService(_dbService);
            EncryptionKey? encryptionKey = null;
            string? encryptionKeyJson = await cryptographicBLService.GetEncryptionKeyValueFromCache(employee.EncryptionKeyId);
            if (!string.IsNullOrEmpty(encryptionKeyJson))
            {
                encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            }
            //if (login != null)
            //{
            //    employee.EncryptionKeyId = login.EncryptionKeyId;
            //    get the Encryptionkey from the cache
            //    string? encryptionKeyJson = await cryptographicBLService.GetEncryptionKeyValueFromCache(login.EncryptionKeyId);
            //    if (!string.IsNullOrEmpty(encryptionKeyJson))
            //    {
            //        encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            //    }
            //}
            //else
            //{
            //    string encryptionKeyJson = await cryptographicBLService.GetRandomEncryptionKeyValueFromCache();
            //    encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            //}
            //if (encryptionKey is null || encryptionKey.KeyString is null || employee.Name is null)
            //{
            //    TODO
            //    return 0;
            //}
            //employee.EncryptionKeyId = encryptionKey.Id;
            employee.DateOfBirthEncrypted = await encryptionDecryptionService.Encrypt(employee.DateOfBirth.ToString("yyyy-MM-dd"), encryptionKey.KeyString);
            employee.SSNEncrypted = await encryptionDecryptionService.Encrypt(employee.SSN, encryptionKey.KeyString);
            employee.UpdatedDate = DateTime.Now;
            Login login = await _loginService.GetLogin(employee.Email);
            var result = await _employeeService.UpdateEmployee(employee);
            if (login != null && login.EmployeeId == null)
            {
                login.EmployeeId = employee.Id;
                await _loginService.UpdateLogin(login);

            }
            return true;
        }


        public async Task<Employee>? GetEmployeeMatching(string email)
        {
            var result = await _employeeService.GetEmployee(email);
            return result;
        }

    }
}
