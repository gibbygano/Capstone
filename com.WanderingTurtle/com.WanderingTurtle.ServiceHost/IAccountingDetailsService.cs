using com.WanderingTurtle.Common;
using System;
using System.ServiceModel;

namespace com.WanderingTurtle.Service
{
    [ServiceContract]
    public interface IAccountingDetailsService
    {
        [OperationContract]
        AccountingDetails GetAccountingDetails(DateTime start, DateTime end);
    }
}