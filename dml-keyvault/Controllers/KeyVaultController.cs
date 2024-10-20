using Microsoft.AspNetCore.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace dml_keyvault.Controllers
{
    public class KeyVaultController : Controller
    {
        [HttpGet()]
        [Route("api/v1/GetVaultDefaultValue/{id}")]
        /// <summary>Uset this to add employees</summary>
        public IActionResult GetVaultDefaultValue(string id)
        {
            #region Option 1
            /*
            
            SecretKeyPair secretKeyPair = new SecretKeyPair();
            secretKeyPair.Key = id;
            secretKeyPair.Value = "value of the key " + id;
            string jsonResult = JsonConvert.SerializeObject(secretKeyPair);
            return Ok(jsonResult);
            */
            #endregion
            #region Option 2
            /*

            var result = string.Format("value of the key {id}", id);
            SecretKeyPair secretKeyPair = new SecretKeyPair
            {
                Key = id,
                Value = "value of the key " + id
            };
            string jsonResult = JsonConvert.SerializeObject(secretKeyPair);
            return Ok(jsonResult);
            */
            #endregion
            SecretKeyPair secretKeyPair = new()
            {
                Key = id,
                Value = "value of the key " + id
            };
            string jsonResult = JsonConvert.SerializeObject(secretKeyPair);
            return Ok(jsonResult);
        }
        [HttpGet()]
        [Route("api/v1/GetVaultValue/{id}")]
        /// <summary>Uset this to add employees</summary>
        public IActionResult GetVaultValue(string id)
        {
            
            SecretKeyPair secretKeyPair = new();
            secretKeyPair.Key = id;
            secretKeyPair.Value = "value of the key " + id;
            string jsonResult = JsonConvert.SerializeObject(secretKeyPair);
            return Ok(jsonResult);
        }


    }
    public class SecretKeyPair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class KVHandler
    {
        public string GetKVValue(string id)
        {
            //testkey = testvalue

            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential();
            //defaultAzureCredential.
            //var client = new SecretClient(new Uri("https://<your-unique-key-vault-name>.vault.azure.net/"), new DefaultAzureCredential(), options);
            var client = new SecretClient(new Uri("https://dml-keyvault-store.vault.azure.net/"), new DefaultAzureCredential(), options);
            KeyVaultSecret secret = client.GetSecret(id);

            string secretValue = secret.Value;
            return secretValue;
        }

    }
}
