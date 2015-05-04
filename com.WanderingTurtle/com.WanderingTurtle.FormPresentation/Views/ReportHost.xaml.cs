using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for ReportHost.xaml
    /// </summary>
    public partial class ReportHost
    {
        public ReportHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/16 Binds the hosted report viewer to the PrintableInvoice with the given
        ///          guest with the guestID.
        /// </summary>
        /// <param name="guestId">guest id of the guest to use for the invoice</param>
        /// <returns>bool successful</returns>
        /// <remarks>Updated by Arik Chadima 2015/4/17</remarks>
        /// <exception cref="WanderingTurtleException" />
        public void BuildInvoice(int guestId)
        {
            //builds the managers required
            InvoiceManager invoiceManager = new InvoiceManager();
            HotelGuestManager guestManager = new HotelGuestManager();

            try
            {
                RvHostArea.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource1 = new ReportDataSource
                {
                    Name = "HotelGuestDataset"
                };
                //Name of the report dataset in our .RDLC file
                List<HotelGuest> guestSet = new List<HotelGuest>();
                var foundGuest = guestManager.GetHotelGuest(guestId);
                guestSet.Add(foundGuest);
                reportDataSource1.Value = guestSet;
                RvHostArea.LocalReport.DataSources.Add(reportDataSource1);

                ReportDataSource reportDataSource2 = new ReportDataSource
                {
                    Name = "InvoiceDetailsDataset"
                };
                //Name of the report dataset in our .RDLC file
                List<InvoiceDetails> invoiceSet = new List<InvoiceDetails>
                {
                    invoiceManager.RetrieveInvoiceByGuest(guestId)
                };
                reportDataSource2.Value = invoiceSet;
                RvHostArea.LocalReport.DataSources.Add(reportDataSource2);

                ReportDataSource reportDataSource3 = new ReportDataSource
                {
                    Name = "BookingDetailsSet",
                    Value = invoiceManager.RetrieveGuestBookingDetailsList(guestId)
                };
                //Name of the report dataset in our .RDLC file
                RvHostArea.LocalReport.DataSources.Add(reportDataSource3);

                ReportDataSource reportDataSource4 = new ReportDataSource
                {
                    Name = "ZipCodeDataset"
                };
                //Name of the report dataset in our .RDLC file
                List<CityState> zipSet = new List<CityState>
                {
                    foundGuest.CityState
                };
                reportDataSource4.Value = zipSet;
                RvHostArea.LocalReport.DataSources.Add(reportDataSource4);

                RvHostArea.LocalReport.ReportEmbeddedResource = "com.WanderingTurtle.FormPresentation.Views.PrintableInvoice.rdlc";
                RvHostArea.RefreshReport();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }
    }
}