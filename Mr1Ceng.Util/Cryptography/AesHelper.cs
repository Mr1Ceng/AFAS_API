using System.Security.Cryptography;
using System.Text;

namespace Mr1Ceng.Util;

/// <summary>
/// AES加密工具类(ECB,PKCS7)
/// </summary>
public class AesHelper
{
    #region 变量

    /// <summary>
    /// 密钥
    /// </summary>
    private readonly string m_CstrKey = "Yu1n3XG6Tb4iwaP8KwfSoiTts9kMjXbT";

    /// <summary>
    /// 编码方式
    /// </summary>
    private Encoding m_Encoding = Encoding.UTF8;

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public AesHelper()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public AesHelper(Encoding encoding)
    {
        m_Encoding = encoding;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">加密的密钥，必须为32位字符或数字</param>
    public AesHelper(string key)
    {
        if (key.Length == 32)
        {
            m_CstrKey = key;
        }
        else if (key.Length > 0)
        {
            throw new Exception("key必须为32位字符或数字");
        }
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="key">加密的密钥，必须为32位字符或数字</param>
    /// <param name="encoding">编码方式</param>
    public AesHelper(string key, Encoding encoding)
    {
        if (key.Length == 32)
        {
            m_CstrKey = key;
        }
        else if (key.Length > 0)
        {
            throw new Exception("key必须为32位字符或数字");
        }

        m_Encoding = encoding;
    }

    #endregion

    #region Encrypt

    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="encryptValue">要加密的字符串</param>
    /// <returns>加密后的Base64格式字符串</returns>
    public string Encrypt(string encryptValue) => Encrypt(encryptValue, m_Encoding);

    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="encryptValue">要加密的字符串</param>
    /// <param name="encoding">编码方式</param>
    /// <returns>加密后的Base64格式字符串</returns>
    public string Encrypt(string encryptValue, Encoding encoding)
    {
        m_Encoding = encoding;

        //数据检查
        if (GetString.FromObject(encryptValue) == "")
        {
            return "";
        }

        //数据加密
        using var aesAlg = Aes.Create();
        aesAlg.Key = m_Encoding.GetBytes(m_CstrKey);
        aesAlg.Mode = CipherMode.ECB;
        aesAlg.Padding = PaddingMode.PKCS7;

        //数据输出
        using MemoryStream msEncrypt = new();
        var encryptor = aesAlg.CreateEncryptor();
        using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (StreamWriter swEncrypt = new(csEncrypt))
        {
            swEncrypt.Write(encryptValue);
        }
        var bytes = msEncrypt.ToArray();
        return GetBase64.FromBytes(bytes);
    }

    #endregion

    #region Decrypt

    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="decryptValue">要解密的Base64格式字符串</param>
    /// <returns>解密的字符串</returns>
    public string Decrypt(string decryptValue) => Decrypt(decryptValue, m_Encoding);

    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="decryptValue">要解密的Base64格式字符串</param>
    /// <param name="encoding">编码方式</param>
    /// <returns>解密的字符串</returns>
    public string Decrypt(string decryptValue, Encoding encoding)
    {
        m_Encoding = encoding;

        //数据检查
        if (GetString.FromObject(decryptValue) == "")
        {
            return "";
        }

        //数据解密
        using var aesAlg = Aes.Create();
        aesAlg.Key = m_Encoding.GetBytes(m_CstrKey);
        aesAlg.Mode = CipherMode.ECB;
        aesAlg.Padding = PaddingMode.PKCS7;

        var inputBytes = GetBuffer.FromBase64(decryptValue);
        using MemoryStream msEncrypt = new(inputBytes);
        var decryptor = aesAlg.CreateDecryptor();
        using CryptoStream csEncrypt = new(msEncrypt, decryptor, CryptoStreamMode.Read);

        using StreamReader srEncrypt = new(csEncrypt);
        return srEncrypt.ReadToEnd();
    }

    #endregion
}
