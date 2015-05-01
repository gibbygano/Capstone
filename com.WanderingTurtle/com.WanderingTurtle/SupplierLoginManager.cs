using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;
using System;
using System.Data.SqlClient;

namespace com.WanderingTurtle.BusinessLogic
{
    public class SupplierLoginManager
    {
        private SupplierLoginAccessor access = new SupplierLoginAccessor();

        public SupplierLoginManager()
        { }

        public SupplierLogin retrieveSupplierLogin(string userPassword, string userName)
        {
            try
            {
                return access.RetrieveSupplierLogin(userPassword, userName);
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

        public string retrieveSupplierUserName(int supplierID)
        {
            try
            {
                return access.RetrieveSupplierUserNameByID(supplierID);
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

        public ResultsEdit UpdateSupplierLogin(string newPassword, SupplierLogin oldLogin)
        {
            try
            {
                //checks if user name already exits.. returns false if it does
                bool result1 = CheckSupplierUserName(oldLogin.UserName);

                if (!result1)
                {
                    int result = access.UpdateSupplierPassword(newPassword, oldLogin);

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

        public bool CheckSupplierUserName(string userName)
        {
            try
            {
                return access.CheckUserName(userName);
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