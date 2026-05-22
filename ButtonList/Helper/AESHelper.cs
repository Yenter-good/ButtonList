using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class AesHelper
{
    // ====================== 我为你随机生成的密钥和向量 ======================
    // 密钥 (32字节 = 256位 AES)
    public static readonly string AesKey = "xQ9L$kF7sG2pR5tY8vB0nC4mZ6aD1fH3";
    // 向量 (16字节 = 128位 固定要求)
    public static readonly string AesIV = "9sK2pG5dF8gH1jK4";
    // ======================================================================

    /// <summary>
    /// AES 加密 (CBC模式, PKCS7填充)
    /// </summary>
    public static string Encrypt(string plainText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(AesKey);
        byte[] ivBytes = Encoding.UTF8.GetBytes(AesIV);
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    /// <summary>
    /// AES 解密
    /// </summary>
    public static string Decrypt(string cipherText)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(AesKey);
        byte[] ivBytes = Encoding.UTF8.GetBytes(AesIV);
        byte[] cipherBytes = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}