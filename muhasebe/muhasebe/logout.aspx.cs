using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace muhasebe
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["xml"] != null)
            {
                if (File.Exists(base.Server.MapPath("~/files/") + this.Session["xml"] + ".xml"))
                {
                    File.Delete(base.Server.MapPath("~/files/") + this.Session["xml"] + ".xml");
                }
                this.Session["Companys"] = null;
                this.Session["xml"] = null;
                if (File.Exists(base.Server.MapPath("~/files/") + this.Session["excel"]))
                {
                    File.Delete(base.Server.MapPath("~/files/") + this.Session["excel"]);
                }
                this.Session["excel"] = null;
                this.Session["Caris"] = null;
                this.Session["Admin"] = null;
            }
            this.Session.RemoveAll();
            this.Session.Clear();
            this.Session.Abandon();
            base.Response.Redirect("home.aspx");
        }
    }
}