using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.WanderingTurtle.Common
{
    public class AccountingDetails : IXMLAble
    {
        public List<AccountingInvoiceDetails> Invoices { get; private set; }
        public List<AccountingSupplierListingDetails> SupplierListings { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// Arik Chadima
        /// Created: 2015/4/30
        /// 
        /// Class combines data from Invoices and Suppliers for XML
        /// </summary>
        public AccountingDetails()
        {
            Invoices = new List<AccountingInvoiceDetails>();
            SupplierListings = new List<AccountingSupplierListingDetails>();
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// 
        /// Serializes object into an xml string.
        /// 
        /// Adapted from code originally by Jim Glasgow
        /// </summary>
        /// <returns>string xml representation of object</returns>
        public string ToXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(AccountingDetails));

            MemoryStream memStream = new MemoryStream();

            serializer.Serialize(memStream, this);

            //we need a streamReader to read a memory stream, as it is just a buffer.

            StreamReader reader = new StreamReader(memStream);

            // read stream to a string.
            memStream.Position = 0;
            string text = reader.ReadToEnd();

            reader.Close();
            memStream.Close();

            return text;
        }

        /// <summary>
        /// Arik Chadima
        /// Created: 2015/4/30
        /// 
        /// Saves a seriealized xml reperesentation of this object.
        /// 
        /// Adapted from code originally by Jim Glasgow
        /// </summary>
        /// <param name="filename">string name of the file and path to save the xml</param>
        /// <returns>bool successful</returns>
        public bool XMLFile(string filename)
        {
            try
            {
                //first, new up an xml serializer of the current type.
                XmlSerializer serializer = new XmlSerializer(typeof(AccountingDetails));

                //to write to a file, build a streamWriter
                StreamWriter writer = new StreamWriter(filename);

                serializer.Serialize(writer, this);

                writer.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Arik Chadima
    /// Created: 2015/4/30
    /// </summary>
    public class AccountingInvoiceDetails
    {
        public InvoiceDetails InvoiceInformation { get; set; }
        public List<BookingDetails> Bookings { get; set; }
        public AccountingInvoiceDetails()
        {
            Bookings = new List<BookingDetails>();
        }
    }

    /// <summary>
    /// Arik Chadima
    /// Created: 2015/4/30
    /// </summary>
    public class AccountingSupplierListingDetails
    {
        public List<ItemListingDetails> Items { get; set; }
        public List<BookingDetails> Bookings { get; set; }
        public Supplier Vendor { get; set; }

        public AccountingSupplierListingDetails()
        {
            Items = new List<ItemListingDetails>();
            Bookings = new List<BookingDetails>();
        }
    }
}
