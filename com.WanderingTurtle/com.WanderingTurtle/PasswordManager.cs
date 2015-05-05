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

            //byte[] bytes = (input + salt).Split();
            //hash = mySHA256.ComputeHash(bytes);

            return hash;
        }
    }
}
