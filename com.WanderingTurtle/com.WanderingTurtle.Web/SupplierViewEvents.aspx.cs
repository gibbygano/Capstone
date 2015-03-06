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
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                try
                {

                    _listedEvents = _myManager.RetrieveEventList();
                    foreach (Event x in _listedEvents)
                    {
                        x.setFields();
                    }

                    bindData();

                }
                catch (Exception)
                {
                    Response.Write("<SCRIPT LANGUAGE=\"JavaScript\">alert(\"Error Retrieving List\")</SCRIPT>");
                    return;
                }
            }
        }

        private void bindData()
        {
            lvEvents.DataSource = _listedEvents;
            lvEvents.DataBind();
        }

        protected void lvEvents_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //this.dpEvents.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            bindData();
        }

    }
}