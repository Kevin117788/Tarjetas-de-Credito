using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tarjetas_de_Credito
{
    public class C_RC2 // Cambiado a 'public' en lugar de 'internal'
    {
        
        public static byte[] Encriptar(string plainText, byte[] key, byte[] iv)
        {
            using (RC2 rc2 = RC2.Create())
            {
                rc2.Key = key;
                rc2.IV = iv;

                ICryptoTransform encryptor = rc2.CreateEncryptor(rc2.Key, rc2.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string Desencriptar(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (RC2 rc2 = RC2.Create())
            {
                rc2.Key = key;
                rc2.IV = iv;

                ICryptoTransform decryptor = rc2.CreateDecryptor(rc2.Key, rc2.IV);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }

            }
        }
    }
}
