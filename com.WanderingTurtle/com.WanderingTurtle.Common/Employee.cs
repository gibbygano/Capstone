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
		public int? EmployeeId { get; private set; }

		public string FirstName { get; private set; }

		public string LastName { get; private set; }

		public string Password { get; private set; }

		public RoleData Level { get; private set; }

		public bool Active { get; private set; }

		/// <summary>
		/// Use for creating an employee object with an ID
		/// </summary>
		/// <param name="employeeId">Employee ID</param>
		/// <param name="firstName">Employee First Name</param>
		/// <param name="lastName">Employee Last Name</param>
		/// <param name="password">Employee Password</param>
		/// <param name="level">Employee User Level</param>
		/// <param name="active">Employee Active</param>
		public Employee(int employeeId, string firstName, string lastName, string password, int level, bool active = true)
		{
			SetValues(employeeId, firstName, lastName, password, level, active);
		}

		/// <summary>
		/// Use for creating a new employee.
		/// </summary>
		/// <remarks>Does not have an Employee ID</remarks>
		/// <param name="firstName">Employee First Name</param>
		/// <param name="lastName">Employee Last Name</param>
		/// <param name="password">Employee Password</param>
		/// <param name="level">Employee User Level</param>
		/// <param name="active">Employee Active</param>
		public Employee(string firstName, string lastName, string password, int level, bool active = true)
		{
			SetValues(null, firstName, lastName, password, level, active);
		}

		private void SetValues(int? employeeId, string firstName, string lastName, string password, int level, bool active)
		{
			EmployeeId = employeeId;
			FirstName = firstName;
			LastName = lastName;
			Password = password;
			Level = (RoleData)level;
			Active = active;
		}

		public string GetFullName => string.Format("{0} {1}", FirstName, LastName);
	}
}