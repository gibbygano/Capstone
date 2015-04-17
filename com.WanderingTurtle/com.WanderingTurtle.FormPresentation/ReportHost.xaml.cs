﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using Microsoft.Reporting.WinForms;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ReportHost.xaml
    /// </summary>
    public partial class ReportHost : UserControl
    {
        public ReportHost()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/16
        /// 
        /// Binds the hosted report viewer to the PrintableInvoice with the given guest with the guestID.
        /// </summary>
        /// <param name="guestID">guest id of the guest to use for the invoice</param>
        /// <returns>bool successful</returns>
        /// <remarks>Updated by Arik Chadima 2015/4/17</remarks>
        public bool BuildInvoice(int guestID)
        {
            //builds the managers required
            InvoiceManager invoiceManager = new InvoiceManager();
            HotelGuestManager guestManager = new HotelGuestManager();

            try
            {
                ReportDataSource reportDataSource1 = new ReportDataSource();
                reportDataSource1.Name = "HotelGuestDataset"; //Name of the report dataset in our .RDLC file
                List<HotelGuest> guestSet = new List<HotelGuest>();
                guestSet.Add(guestManager.GetHotelGuest(guestID));
                reportDataSource1.Value = guestSet;
                rvHostArea.LocalReport.DataSources.Add(reportDataSource1);
                ReportDataSource reportDataSource2 = new ReportDataSource();
                reportDataSource2.Name = "InvoiceDetailsDataset"; //Name of the report dataset in our .RDLC file
                List<InvoiceDetails> invoiceSet = new List<InvoiceDetails>();
                invoiceSet.Add(invoiceManager.RetrieveInvoiceByGuest(guestID));
                reportDataSource2.Value = invoiceSet;
                rvHostArea.LocalReport.DataSources.Add(reportDataSource2);
                ReportDataSource reportDataSource3 = new ReportDataSource();
                reportDataSource3.Name = "BookingDetailsSet"; //Name of the report dataset in our .RDLC file
                reportDataSource3.Value = invoiceManager.RetrieveGuestBookingDetailsList(guestID);
                rvHostArea.LocalReport.DataSources.Add(reportDataSource3);
                rvHostArea.LocalReport.ReportEmbeddedResource = "com.WanderingTurtle.FormPresentation.PrintableInvoice.rdlc";

                rvHostArea.RefreshReport();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false;
            }
        }
    }
}
