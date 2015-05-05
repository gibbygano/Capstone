using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for PrintableInvoice.xaml
    /// </summary>
    public partial class PrintableInvoice
    {
        private int GuestId { get; set; }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/17
        /// Sends the guest ID to the invoice report
        /// </summary>
        /// <param name="guestId"></param>
        /// <exception cref="WanderingTurtleException" />
        public PrintableInvoice(int guestId)
        {
            InitializeComponent();
            GuestId = guestId;
            ReportViewer.BuildInvoice(GuestId);
        }
    }
}