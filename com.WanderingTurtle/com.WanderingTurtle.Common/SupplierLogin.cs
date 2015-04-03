using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    public class SupplierLogin
    {
        public int UserID{ get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }

        public SupplierLogin()
        { }

        public SupplierLogin(string UserPassword, string UserName)
        {
            this.UserPassword = UserPassword;
            this.UserName = UserName;
        }
    }
}
