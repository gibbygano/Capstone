namespace com.WanderingTurtle.Common
{
    public class SupplierLogin
    {
        public int UserID{ get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string SupplierID { get; set; }
        public bool Active { get; set; }

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
    }
}
