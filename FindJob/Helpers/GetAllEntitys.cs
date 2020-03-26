using FindJob.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FindJob.Models
{
    public class GetEntities
    {
        public static IList<T> GetAll<T>()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var experience = session.Query<T>().ToList<T>();
                return experience;
            }
        }

        public static int GetId(string email)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var user = session.Query<User>().FirstOrDefault(u => u.Email == email);
                if (user == null) return 0;
                else
                    return user.Id;
            }
        }

        public static string CreateMD5Salt(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input+"salt000");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}