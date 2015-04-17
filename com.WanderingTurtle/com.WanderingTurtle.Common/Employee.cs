namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Ryan Blake
    /// Created: 2015/02/16
    ///
    /// Class for the creation of Employee Objects with set data fields
    /// </summary>
    /// <remarks>
    /// Miguel Santana
    /// Updated: 2015/26/22
    ///
    /// Created non-default constructors to be used in assigning values.
    /// </remarks>
    public class Employee
    {
        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/26
        ///
        /// Use for creating an employee object with an ID
        /// </summary>
        /// <param name="EmployeeID">Employee ID</param>
        /// <param name="FirstName">Employee First Name</param>
        /// <param name="LastName">Employee Last Name</param>
        /// <param name="Level">Employee User Level</param>
        /// <param name="Active">Employee Active</param>
        public Employee(int EmployeeID, string FirstName, string LastName, int Level, bool Active = true)
        {
            SetValues(EmployeeID, FirstName, LastName, null, Level, Active);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/26
        ///
        /// Use for creating a new employee.
        /// </summary>
        /// <remarks>Does not have an Employee ID</remarks>
        /// <param name="FirstName">Employee First Name</param>
        /// <param name="LastName">Employee Last Name</param>
        /// <param name="Password">Employee Password</param>
        /// <param name="Level">Employee User Level</param>
        /// <param name="Active">Employee Active</param>
        public Employee(string FirstName, string LastName, string Password, int Level, bool Active = true)
        {
            SetValues(null, FirstName, LastName, Password, Level, Active);
        }

        public bool Active { get; private set; }

        public int? EmployeeID { get; private set; }

        public string FirstName { get; private set; }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/26
        /// </summary>
        public string GetFullName { get { return string.Format("{0} {1}", this.FirstName, this.LastName); } }

        public string LastName { get; private set; }

        public RoleData Level { get; private set; }

        public string Password { get; private set; }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/26
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Password"></param>
        /// <param name="Level"></param>
        /// <param name="Active"></param>
        private void SetValues(int? EmployeeID, string FirstName, string LastName, string Password, int Level, bool Active)
        {
            this.EmployeeID = EmployeeID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Password = Password;
            this.Level = (RoleData)Level;
            this.Active = Active;
        }
    }
}