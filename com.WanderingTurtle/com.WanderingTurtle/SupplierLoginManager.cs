using System;
using System.Data.SqlClient;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.DataAccess;

namespace com.WanderingTurtle.BusinessLogic
{
    public class SupplierLoginManager
    {
        SupplierLoginAccessor access = new SupplierLoginAccessor();

        public SupplierLoginManager()
        { }

        public SupplierLogin retrieveSupplierLogin(string userPassword, string userName)
        {
            try
            {
                return access.retrieveSupplierLogin(userPassword, userName);
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
                return access.retrieveSupplierUserNameByID(supplierID);
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



        public int addSupplierLogin(string userName, int supplierID)
        {
            try
            {
                return access.addSupplierLogin(userName, supplierID);
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

        public ResultsEdit UpdateSupplierLogin(string newUserName, string oldUserName, int oldSupplierID)
        {
            try
            {
                bool result1 = CheckSupplierUserName(newUserName);

                if (result1)
                {
                    int result = access.UpdateSupplierLogin(newUserName, oldUserName, oldSupplierID);

                    if (result == 1)
                    {
                        return ResultsEdit.Success;
                    }
                    else
                    {
                        return ResultsEdit.ChangedByOtherUser;
                    }
                }
                else
                {
                    return ResultsEdit.DatabaseError;
                }
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
                return access.checkUserName(userName);
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

        public int archiveSupplierLogin(SupplierLogin oldSupplier, bool archive)
        {
            try
            {
                return access.archiveSupplierLogin(oldSupplier, archive);
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