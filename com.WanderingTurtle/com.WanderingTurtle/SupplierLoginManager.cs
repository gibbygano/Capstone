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
        /// <param name="userPassword"></param>
        /// <param name="userName"></param>
        /// <returns>SupplierLogin object</returns>
        public SupplierLogin RetrieveSupplierLogin(string userPassword, string userName)
        {
            try
            {
                PasswordManager myPass = new PasswordManager();
                userPassword = myPass.supplierHash(userName, userPassword);
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
        /// <param name="newPassword"></param>
        /// <param name="oldLogin"></param>
        /// <returns></returns>
        public ResultsEdit UpdateSupplierLogin(string newPassword, SupplierLogin oldLogin)
        {
            try
            {
                //checks if user name already exits.. returns false if it does
                bool result1 = CheckSupplierUserName(oldLogin.UserName);

                if (!result1)
                {
                    PasswordManager myPass = new PasswordManager();
                    //oldLogin.UserPassword = myPass.supplierHash(oldLogin.UserName, oldLogin.UserPassword);
                    newPassword= myPass.supplierHash(oldLogin.UserName, newPassword);
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
        /// <param name="userName"></param>
        /// <returns></returns>
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