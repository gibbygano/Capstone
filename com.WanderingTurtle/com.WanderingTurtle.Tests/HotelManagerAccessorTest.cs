using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.WanderingTurtle.DataAccess;
using com.WanderingTurtle.Common;
using System.Data.SqlClient;

namespace com.WanderingTurtle.Tests
{
    [TestClass]
    public class HotelManagerAccessorTest
    {
        [TestInitialize]
        public void initialize()
        {
            HotelGuestAccessor.HotelGuestAdd(new HotelGuest("Fake", "Guest", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", 000, true));
        }

        [TestMethod]
        public void HotelAccessorAdd()
        {
            int changed = HotelGuestAccessor.HotelGuestAdd(new HotelGuest("Fake", "Person", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", 234234234, true));
            Assert.AreEqual(2, changed);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void HotelAccessorAddFail()
        {
            HotelGuestAccessor.HotelGuestAdd(new HotelGuest("Fake", "Guest", "1111 Fake St.", "", new CityState("52641", "Mt. Pleasant", "IA"), "5556667777", "fake@gmail.com", 000, true));
        }

        [TestMethod]
        public void HotelAccessorGet()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            Assert.AreEqual("Fake", guest[guest.Count - 1].FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void HotelAccessorGetFail()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet(-1);
        }

        [TestMethod]
        public void HotelAccessorUpdate()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            guest.Add(new HotelGuest((int)guest[guest.Count - 1].HotelGuestID, guest[guest.Count - 1].FirstName, guest[guest.Count - 1].LastName, guest[guest.Count - 1].Address1, guest[guest.Count - 1].Address2, guest[guest.Count - 1].CityState, guest[guest.Count - 1].PhoneNumber, guest[guest.Count - 1].EmailAddress, guest[guest.Count - 1].Room, false));
            int changed = HotelGuestAccessor.HotelGuestUpdate(guest[guest.Count - 2], guest[guest.Count - 1]);
            Assert.AreEqual(1, changed);
        }

        [TestMethod]
        public void HotelAccessorArchive()
        {
            List<HotelGuest> guest = HotelGuestAccessor.HotelGuestGet();
            int changed = HotelGuestAccessor.HotelGuestArchive(guest[guest.Count - 1], false);
            Assert.AreEqual(1, changed);
        }

        [TestCleanup]
        public void cleanup()
        {
            var conn = DatabaseConnection.GetDatabaseConnection();
            string commandText = @"DELETE FROM Invoice WHERE HotelGuestID >= 10";
            //string commandText2 = @"DELETE FROM Invoice WHERE HotelGuestID = 11";
            string commandText3 = @"DELETE FROM [dbo].[HotelGuest] WHERE FirstName = 'Fake'";

            var cmd = new SqlCommand(commandText, conn);
            //var cmd2 = new SqlCommand(commandText2, conn);
            var cmd3 = new SqlCommand(commandText3, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                //cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                Console.Write("Fail!");
            }
            finally { conn.Close();  }
        }
    }
}
