namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for PrintableInvoice.xaml
    /// </summary>
    public partial class PrintableInvoice
    {

        private int _guestId { get; set; }

        /// <summary>
        /// Pat Banks
        /// Created 2015/04/17
        /// 
        /// Sends the guest ID to the invoice report
        /// </summary>
        /// <param name="guestId"></param>
        public PrintableInvoice(int guestId)
        {
            InitializeComponent();
            _guestId = guestId;
            ReportViewer.BuildInvoice(_guestId);
        }
    }
}