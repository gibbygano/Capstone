using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Web
{
    public partial class SupplierViewEvents : System.Web.UI.Page
    {
        EventManager _myManager = new EventManager();
        List<Event> _listedEvents;
        public List<EventType> _eventTypes;
        public string current;
        public string transport;
        public string onsite;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                try
                {
                    _eventTypes = _myManager.RetrieveEventTypeList();
                }
                catch (Exception)
                {
                    Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving Event Types\")</SCRIPT>");
                }
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
            }
        }
        private void bindData()
        {
            lvEvents.DataSource = _listedEvents;
            lvEvents.DataBind();
        }
        public IEnumerable<Event> GetEvents()
        {
            try
            {
                _listedEvents = _myManager.RetrieveEventList();
                foreach (Event x in _listedEvents)
                {
                    x.setFields();
                }
                return _listedEvents;
            }
            catch (Exception)
            {

                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List\")</SCRIPT>");
                return null;
            }
        }
        public void UpdateEvent(int eventItemID)
        {
            try
            {
                Event myEvent = _listedEvents
                .Where(e => e.EventItemID == eventItemID).FirstOrDefault();

                Event newEvent = new Event(myEvent.EventItemID, myEvent.EventItemName, myEvent.Transportation, myEvent.EventTypeID, myEvent.OnSite, myEvent.ProductID, myEvent.Description, myEvent.Active);
                
                newEvent.EventItemName = String.Format("{0}", Request.Form["name"]);
                newEvent.EventTypeID = int.Parse(Request.Form["type"]);
                newEvent.Transportation = bool.Parse(Request.Form["transport"]);
                newEvent.OnSite = bool.Parse(Request.Form["onsite"]);
                int result = 0;
                if (myEvent != null)
                {
                    result = _myManager.EditEvent(myEvent, newEvent);

                }

            }
            catch (Exception)
            {
                Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Updating Event\")</SCRIPT>");
            }


        }
        public void DeleteEvent(int productID)
        {

        }
        public void InsertEvent()
        {

        }

        protected void lvEvents_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //this.dpEvents.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData();
        }

    }
}