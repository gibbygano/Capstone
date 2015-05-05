namespace com.WanderingTurtle.Common
{
    /// <summary>
    /// Rose Steffensmeier
    /// Created: 2015/04/03 
    ///
    /// Object to hold information pertaining to a Supplier Login
    /// </summary>
    public class SupplierLogin
    {
        public SupplierLogin()
        { }

        public SupplierLogin(string UserPassword, string UserName)
        {
            this.UserPassword = UserPassword;
            this.UserName = UserName;
        }

        public SupplierLogin(int UserID, string UserPassword, string UserName, bool Active)
        {
            this.UserID = UserID;
            this.UserPassword = UserPassword;
            this.UserName = UserName;
            this.Active = Active;
        }

        public bool Active { get; set; }

        public string SupplierID { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }
    }
}