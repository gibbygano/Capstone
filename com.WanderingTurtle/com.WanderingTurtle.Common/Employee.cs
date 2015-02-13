using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    public class Employee : UserLogin
    {
        public int EmployeeID { get; set; }

        public bool Active { get; set; }

        public Employee() : base() {}   

    }
}
