using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.WanderingTurtle.Common;
using System.Security.Cryptography;

namespace com.WanderingTurtle.BusinessLogic
{
    public class PasswordManager
    {

        public string employeeHash(Employee emp, string input)
        {
            string hash = "";

            string salt = ((int)emp.EmployeeID * emp.Level.GetHashCode()).ToString();
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            var temphash = mySHA256.ComputeHash(bytes);
            foreach (byte x in temphash)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        public string supplierHash(string username, string input)
        {
            string hash = "";

            string salt = username.Substring(1).ToLower() + ((username.Length * input.Length)*((int)(input.Length/username.Length))).ToString();
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            var temphash = mySHA256.ComputeHash(bytes);
            foreach (byte x in temphash)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }
    }
}
