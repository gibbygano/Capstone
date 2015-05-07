using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using System.Windows.Forms;

namespace com.WanderingTurtle.FormPresentation.Views.Admin
{
    /// <summary>
    /// Interaction logic for GenerateReport.xaml
    /// </summary>
    public partial class GenerateReport
    {
        public GenerateReport()
        {
            InitializeComponent();
        }

        private async void BtnRunXML_Click(object sender, RoutedEventArgs e)
        {
            var accountingManager = new AccountingManager();

            if (DateStart.SelectedDate == null) throw new InputValidationException(DateStart, "Please select a Start Date");
            var formStartDate = (DateTime)DateStart.SelectedDate;

            if (DateEnd.SelectedDate == null) throw new InputValidationException(DateStart, "Please select an End Date");
            var formEndDate = (DateTime)DateEnd.SelectedDate;

            if (formStartDate > formEndDate)
            {
                throw new InputValidationException(DateStart, "Start Date must be before the end date.");
            }

            var report = accountingManager.GetAccountingDetails(formStartDate, formEndDate);

            var dlg = new SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                FileName = "Report - " +
                            DateTime.Now.ToShortDateString().Replace("/", "-")
                            + " at " +
                            DateTime.Now.ToShortTimeString().Replace(":", "-"),
                DefaultExt = ".xml",
                Filter = @"Text documents (.xml)|*.xml"
            };
            // Default file extension
            // Filter files by extension

            // Show save file dialog box
            if (!dlg.ShowDialog().Equals(DialogResult.OK)) return;

            DateStart.Text = null;
            DateEnd.Text = null;

            if (report.XmlFile<AccountingDetails>(dlg.FileName))
            {
                await this.ShowMessageDialog("Your Report has been saved successfully.", "Success");
            }
            else
            {
                throw new WanderingTurtleException(this, "There was a problem.  File not written.", "Error");
            }
        }
    }
}