using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Data.SqlClient;

namespace com.WanderingTurtle.BusinessLogic
{
    public class SupplierLoginManager
    {
        private SupplierLoginAccessor _access = new SupplierLoginAccessor();


        /// <summary>
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// Added ability for a supplier to login
        /// </summary>
        public SupplierLoginManager()
        { }

        /// <summary>
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// Retrieves supplier login by username and password
        /// </summary>
        /// <param name="userPassword">The supplier's password to check against</param>
        /// <param name="userName">The supplier's username to check against</param>
        /// <returns>SupplierLogin object whose username and password match those of the parameters passed</returns>
        public SupplierLogin RetrieveSupplierLogin(string userPassword, string userName)
        {
            try
            {
                return _access.RetrieveSupplierLogin(userPassword, userName);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// Retrieves a suppler userName based on the supplier ID
        /// </summary>
        /// <param name="supplierID">SupplierID matching a Supplier object</param>
        /// <returns>A string representing the username of the Supplier whose ID matches the passed Parameter</returns>

        public string RetrieveSupplierUserName(int supplierID)
        {
            try
            {
                return _access.RetrieveSupplierUserNameByID(supplierID);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Rose Steffensmeier
        /// Created:  2015/04/03
        /// Updates a supplierLogin information with a new password
        /// </summary>
        /// <param name="newPassword">The string representation of the new password requested</param>
        /// <param name="oldLogin">The SupplierLogin object to have the password updated for</param>
        /// <returns>An enumerated result depicting pass or fail</returns>
        public ResultsEdit UpdateSupplierLogin(string newPassword, SupplierLogin oldLogin)
        {
            try
            {
                //checks if user name already exits.. returns false if it does
                bool result1 = CheckSupplierUserName(oldLogin.UserName);

                if (!result1)
                {
                    int result = _access.UpdateSupplierPassword(newPassword, oldLogin);

                    return result == 1 ? ResultsEdit.Success : ResultsEdit.ChangedByOtherUser;
                }
                return ResultsEdit.DatabaseError;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/04/13
        /// Checks if a supplier username is in use
        /// </summary>
        /// <param name="userName">The string representation of the username to check against</param>
        /// <returns>Returns true or false; true if the username is valid to use, false if it is not</returns>
        public bool CheckSupplierUserName(string userName)
        {
            try
            {
                return _access.CheckUserName(userName);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}