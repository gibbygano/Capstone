using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Object to hold employee role data for the employee forms
    /// Pat Banks 2/18/15
    /// </summary>
    /// <remarks>
    /// Miguel Santana
    /// : 2015/02/22
    /// Changed from object to enum
    /// </remarks>
    public enum RoleData
    {
        Admin = 1,
        Concierge,
        DeskClerk,
        Valet
    }

    ////List<RoleData> ListData = new List<RoleData>();
    ////ListData.Add(new RoleData { id = "Admin", value = 1 });
    ////ListData.Add(new RoleData { id = "Concierge", value = 2 });
    ////ListData.Add(new RoleData { id = "DeskClerk", value = 3 });
    ////ListData.Add(new RoleData { id = "Valet", value = 4 });
}