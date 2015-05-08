using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;

namespace com.WanderingTurtle.Service
{
    public class AccountingDetailsService : IAccountingDetailsService
    {
        public AccountingDetails GetAccountingDetails(DateTime start, DateTime end)
        {
            return new AccountingManager().GetAccountingDetails(start, end);
        }
    }
}