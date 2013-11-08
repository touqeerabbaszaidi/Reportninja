using System;
using System.Security.Cryptography;
using System.Text;

public class CustomEncryption
{
    public static string key = "sfdjf48mdfdf3054";

    public static string Decrypt(string encryptedString)
    {
        string str = null;
        byte[] inputBuffer = null;
        try
        {
            inputBuffer = Convert.FromBase64String(encryptedString);
            byte[] buffer2 = null;
            MD5CryptoServiceProvider provider = null;
            buffer2 = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(key));
            provider = null;
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider {
                Key = buffer2,
                Mode = CipherMode.ECB
            };
            str = Encoding.ASCII.GetString(provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        }
        catch (Exception exception)
        {
            string message = exception.Message;
            throw;
        }
        return str;
    }

    public static string Encrypt(string plainText)
    {
        string str = null;
        try
        {
            byte[] bytes = Encoding.ASCII.GetBytes(plainText);
            byte[] buffer2 = null;
            MD5CryptoServiceProvider provider = null;
            buffer2 = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(key));
            provider = null;
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider {
                Key = buffer2,
                Mode = CipherMode.ECB
            };
            str = Convert.ToBase64String(provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        }
        catch (Exception exception)
        {
            string message = exception.Message;
            throw;
        }
        return str;
    }
}

