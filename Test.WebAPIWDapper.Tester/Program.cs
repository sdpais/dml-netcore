using Microsoft.Extensions.Configuration;
using WebAPIWDapper.Services;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using static WebAPIWDapper.Tester.Program;

namespace WebAPIWDapper.Tester
{

    internal class Program
    {
        static void Main(string[] args)
        {
            runALoop runALoop = new runALoop();
            string plainTextPassword = "sanjayPais";
            string passHash = runALoop.CreateSHA512HashString(plainTextPassword);
            bool isEqual = runALoop.CompareHash(plainTextPassword, passHash);
            Console.WriteLine(isEqual);
            Console.ReadKey();
        }
        
        static void oldMain(string[] args)
        {
            Console.WriteLine("Hello, World!");
            runALoop runALoop = new runALoop();
            runALoop.runAFewTimes();
            Console.WriteLine("Hello, World All Loops are done!");
            Console.ReadLine();
        }
        
    }
    
    public class runALoop
    {
        public static bool CompareHash(string plainTextPassword, string PasswordHash)
        {
            byte[] saltyHashByteArray = CreateSHA512HashBytes(plainTextPassword);
            string saltyHash = Convert.ToBase64String(saltyHashByteArray);
            return saltyHash.Equals(PasswordHash);
        }

        public static string CreateSHA512HashString(string plainTextPassword)
        {
            byte[] saltyHashByteArray = CreateSHA512HashBytes(plainTextPassword);
            string saltyHash = Convert.ToBase64String(saltyHashByteArray);
            return saltyHash;
        }
        public static byte[] CreateSHA512HashBytes(string plainTextPassword)
        {
            string salty = "Thekeyisakeythatisusedtodowhat";
            string saltyPassword = plainTextPassword + salty;
            byte[] saltyByteArray = Encoding.UTF8.GetBytes(saltyPassword);
            

            byte[] saltyHashByteArray = SHA512.HashData(saltyByteArray);

            return saltyHashByteArray;
        }
        public void runAFewTimes()
        {
            string randomStringofWords = "This is a test of the emergency broadcast system";
            TestEncryption(randomStringofWords);
            randomStringofWords = "just kidding, this is a test of the emergency broadcast system";
            TestEncryption(randomStringofWords);
            randomStringofWords = "this is only a test";
            TestEncryption(randomStringofWords);
            randomStringofWords = "if this were a real emergency, you would be instructed to do something";
            TestEncryption(randomStringofWords);
        }
        public void TestEncryption(string randomStringofWords)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            //EncryptionBLService encryptionBLService = new EncryptionBLService(configuration);
            DbService dbService = new DbService(configuration);
            CryptographicService encryptionDecryptionService = new CryptographicService(dbService);
            //This error happens because the key size provided for the AES encryption algorithm is not valid. AES encryption requires a key of either 128, 192, or 256 bits.
            //string passwordForKey = Guid.NewGuid().ToString().Replace("-", "");
            //string saltForKey = Guid.NewGuid().ToString().Replace("-", "");
            string encryptionKey = Generate256BitKey(); // GenerateKeyString(passwordForKey, saltForKey, 256); //"E546C8DF278CD5931069B522E695D4F2";//
            //byte[] encryptionKeyArray = GenerateKeyBytes(passwordForKey, saltForKey, 256);
            string unencryptedText = randomStringofWords;
            string encryptedText = encryptionDecryptionService.EncryptV2(unencryptedText, encryptionKey).Result;
            Console.WriteLine($"Encrypted Text using string: {encryptedText}");
            string decryptedText = encryptionDecryptionService.DecryptV2(encryptedText, encryptionKey).Result;
            Console.WriteLine($"Decrypted Text: {decryptedText}");

            //encryptedText = encryptionDecryptionService.Encrypt(unencryptedText, encryptionKeyArray).Result;
            //Console.WriteLine($"Encrypted Text using bytes: {encryptedText}");
            //decryptedText = encryptionDecryptionService.Decrypt(encryptedText, encryptionKeyArray).Result;
            //Console.WriteLine($"Decrypted Text: {decryptedText}");
            if (encryptedText != decryptedText)
            {
                Console.WriteLine("Encryption/Decryption failed");
            }
            else
            {
                Console.WriteLine("Encryption/Decryption successful");
            }   
            Console.WriteLine("hello world! All Done!");
    

        }
        public string Generate256BitKey()
        {
            string key = "";
            int keyLength = 32;
            while (key.Length < keyLength)
            {
                key += Guid.NewGuid().ToString().Replace("-", "");
                Console.WriteLine($"Key Length: {key.Length}");
            }
            if(key.Length > keyLength)
            {
                key = key.Substring(0, keyLength);
            }
            return key;
        }
        public string GenerateKeyString(string password, string salt, int keySize = 256)
        {
            byte[] keyArray = GenerateKeyBytes(password, salt, keySize);
            return Encoding.UTF8.GetString(keyArray); 
        }
        public byte[] GenerateKeyBytes(string password, string salt, int keySize = 256)
        {
            Rfc2898DeriveBytes derivedBytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 1000);
            byte[] keyarray = derivedBytes.GetBytes(keySize / 8);
            return keyarray;
        }
        public string GenerateA256bitKey()
        {
            Console.WriteLine("Creating Aes Encryption 256 bit key");
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.KeySize = 256;
                aesAlgorithm.GenerateKey();
                string keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
                Console.WriteLine("Here is the Aes key in Base64:");
                Console.WriteLine(keyBase64);
                return keyBase64;
            }
            
        }
    }
}
