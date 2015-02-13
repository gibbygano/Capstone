using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle
{
    class UserLoginAccessor
    {
        /// <summary>  Create a user login
        /// 
        /// </summary>
        /// <param name="userLogin">userName</param>
        /// <returns>integer of the loginID</returns>

        public int AddAUserLogin(string userLogin)
        {


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        public UserLogin RetrieveUserLogin(string userName)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public List<UserLogin> RetrieveUserLoginList()
        {

        }

        //Update
        public int EditUserLogin(string oldUserLogin, string newUserLogin)
        {

        }


        //Need Archive user??

        //Create 
        public int AddAnEmployee(Employee employeeToAdd)
        {

        }

        //Read
        public string RetrieveEmployee(string UserName)
        {

        }

        public List<Employee> RetrieveEmployeeList()
        {
            try
            {
                return EmployeeAccessor.getEmployeeList();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                var error = new ApplicationException("There was a problem accessing the server.\nPlease contact your system administrator.");
                throw error;
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Update
        public int EditEmployee(Employee employeeToEdit)
        {

        }




    }
}
