namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for PrintableInvoice.xaml
    /// </summary>
    public partial class PrintableInvoice
    {
        private int GuestId { get; set; }

        public PrintableInvoice(int guestId)
        {
            InitializeComponent();
            GuestId = guestId;
            ReportViewer.BuildInvoice(GuestId);
        }
    }
}