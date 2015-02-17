using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    // Ryan Blake
    // February 2015
    public class Employee : UserLogin
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public int Level { get; set; }
    }
}
