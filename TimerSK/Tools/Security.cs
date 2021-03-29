using System;
using System.Security.Cryptography;
using System.Text;

namespace TimerSK.Tools
{
    class Security
    {

        private readonly SHA256 sHA256 = SHA256.Create();
        

        public string GetHashString(string str)
        {
            byte[] hash = sHA256.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append(hash[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}
