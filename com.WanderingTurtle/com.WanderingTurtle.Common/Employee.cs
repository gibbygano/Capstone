using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    // Ryan Blake
    // February 2015 || Updated: February 16, 2015 -- Removed inheritance.
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public int Level { get; set; }
    }
}
