using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    public class UserLogin
    {
        public int UserID { get; set; }
        public string UserPassword { get; set; }
        public char UserLevel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Summary: makes a UserLogin object that is empty
        //
        //Parameters: none
        //
        //Exceptions: none
        public UserLogin()
        { }

        //Summary: creates a new UserLogin object that
        //has parameters assigned to it
        //
        //Parameters: int userID - the ID of the user
        //            string userPassword - user's password
        //            char userLevel - the level of access
        //
        //Exceptions: none
        public UserLogin(int userID, string userPassword, char userLevel)
        {
            UserID = userID;
            UserPassword = userPassword;
            UserLevel = userLevel;
        }
    }
}
