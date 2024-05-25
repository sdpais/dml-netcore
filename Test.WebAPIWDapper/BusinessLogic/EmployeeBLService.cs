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
        public EmployeeBLService(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<bool?> CreateEmployee(Employee employee)
        {
            IEmployeeService _employeeService = new EmployeeService(_dbService);
            CryptographicService encryptionDecryptionService = new CryptographicService(_dbService);
            CryptographicBLService encryptionBLService = new CryptographicBLService(_configuration);
            //find a login matching the email
            //if not found???

            //set Created date
            string encryptionKeyJson = await encryptionBLService.GetRandomEncryptionKeyValueFromCache();
            EncryptionKey? encryptionKey = JsonConvert.DeserializeObject<EncryptionKey>(encryptionKeyJson);
            if (encryptionKey is null || encryptionKey.KeyString is null || employee.Name is null)
            {
                return false;
            }
            employee.EncryptionKeyId = encryptionKey.Id;
            employee.DateOfBirthEncrypted = await encryptionDecryptionService.Encrypt(employee.DateOfBirth.ToString("yyyy-mm-dd"), encryptionKey.KeyString);
            employee.SSNEncrypted = await encryptionDecryptionService.Encrypt(employee.SSN, encryptionKey.KeyString);
            employee.CreatedDate = DateTime.Now;
            var result = await _employeeService.CreateEmployee(employee);
            return result;
        }

    }
}
