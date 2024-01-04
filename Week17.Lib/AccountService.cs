using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Week17.Lib
{
    public struct Account
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public struct RegisterResult
    {
        public bool IsValid { get; set; }
        public string? Error { get; set; }
    }

    public class AccountService
    {
        private List<Account> accounts = [];

        public RegisterResult RegisterAccount(string username, string password)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(username, "username");
            ArgumentNullException.ThrowIfNullOrWhiteSpace(password, "password");

            var usernameExist = accounts.Any(a => a.Email == username); 
            if (usernameExist)
            {
                return new RegisterResult { Error = "Username already exists" };
            }

            // check strong password
            // ...

            accounts.Add(new Account
            {
                Email = username,
                Password = HashString(password), // todo: encrypt the password
            });

            return new RegisterResult { IsValid = true };
        }

        public bool ValidateAccount(string username, string password)
        {
            // if incorrect username
            // -> username does not exist
            // else if incorrect password
            // -> password is incorrect
            // username or password is incorrect
            ArgumentNullException.ThrowIfNullOrWhiteSpace(username, "username");    
            ArgumentNullException.ThrowIfNullOrWhiteSpace(password, "password");

            var hashedPassword = HashString(password); // 2cf24dba5fb0a30e26e83b2ac5b9e29e1b161e5c1fa7425e73043362938b9824
            var accountExist = accounts.Where(x => x.Email == username && x.Password == hashedPassword)
                .Any();

            return accountExist;
        }

        static string HashString(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    stringBuilder.Append(hashedBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
