using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using livingstone.Models;

namespace livingstone.Helper
{
    public class MainHelper
    {
        public static string Random32()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string CalculateMD5Hash(string input)
        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }

        public static User InitUser(int id)
        {
            Livingstone db = new Livingstone();
            return db.Users.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}