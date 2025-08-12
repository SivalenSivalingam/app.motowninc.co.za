using Dapper;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class Session
{
    public DataTable GetAccess(string emailAddress, string password)
    {
        DataTable dataTable = new Repository().Query("SELECT EmployeeId, EmailAddress, AccountType FROM Employees WHERE EmailAddress = @EmailAddress AND Password = SHA2(@Password,512) AND Active = 1;", new List<MySqlParameter>
        {
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@EmailAddress", Value = emailAddress.Trim()},
            new MySqlParameter() { MySqlDbType = MySqlDbType.VarChar, ParameterName="@Password", Value = password.Trim()},
        });

        return dataTable;
    }

    public bool IsEmailUnique(string emailAddress)
    {
        using (IDbConnection db = new MySqlConnection(new Repository().GetMySqlConnection()))
        {
            string sql = "SELECT COUNT(*) FROM Employees WHERE EmailAddress = @EmailAddress AND Active = 1;";

            int count = db.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress.Trim() });

            return count == 0;
        }
    }

    public string CreateSessionTokenClaims(List<Claim> claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["IssuerSigningKey"]));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSettings["ValidIssuer"],
                audience: ConfigurationManager.AppSettings["ValidAudience"],
                expires: DateTime.UtcNow.AddDays(5),
                claims: claims,
                signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    public string EncryptString(string plainText)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("ed9f935a6536428481ee043b14333c37");
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public string DecryptString(string cipherText)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes("ed9f935a6536428481ee043b14333c37");
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}