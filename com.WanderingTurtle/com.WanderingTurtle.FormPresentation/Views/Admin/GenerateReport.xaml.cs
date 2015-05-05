using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows;
using com.WanderingTurtle.Common;

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

            var filename = "report - " +
                              DateTime.Now.ToShortDateString().Replace("/", "-")
                              + " at " +
                              DateTime.Now.ToShortTimeString().Replace(":", "-") + ".xml";

            DateStart.Text = null;
            DateEnd.Text = null;

            

            if (report.XMLFile(filename))
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