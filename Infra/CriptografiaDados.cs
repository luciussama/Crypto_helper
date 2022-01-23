using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LT.Dominio.Infra
{
    public static class CriptografiaDados
    {
        public static string Criptografar(string chave, string textoLimpo)
        {
            string chaveCriptografia = chave;
            byte[] bytesLimpos = Encoding.Unicode.GetBytes(textoLimpo);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(chaveCriptografia, 
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                
                using (MemoryStream streamMemoria = new MemoryStream())
                {
                    using (CryptoStream streamCriptografia = new CryptoStream(streamMemoria, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        streamCriptografia.Write(bytesLimpos, 0, bytesLimpos.Length);
                        streamCriptografia.Close();
                    }
                    textoLimpo = Convert.ToBase64String(streamMemoria.ToArray());
                }
            }
            return textoLimpo;
        }
        public static string Descriptografar(string chave, string textoCriptografado)
        {
            string chaveCriptografia = chave;
            textoCriptografado = textoCriptografado.Replace(" ", "+");
            byte[] criptografiaBytes = Convert.FromBase64String(textoCriptografado);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(chaveCriptografia, 
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream streamMemoria = new MemoryStream())
                {
                    using (CryptoStream streamCriptografia = new CryptoStream(streamMemoria, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        streamCriptografia.Write(criptografiaBytes, 0, criptografiaBytes.Length);
                        streamCriptografia.Close();
                    }
                    textoCriptografado = Encoding.Unicode.GetString(streamMemoria.ToArray());
                }
            }
            return textoCriptografado;
        }
    }
}
