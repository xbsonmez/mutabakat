using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace muhasebe
{
    public partial class print : System.Web.UI.Page
    {
        private void checkLogin()
        {
            if (this.Session["Admin"] == null)
            {
                base.Response.Redirect("home.aspx");
            }
        }

        private void Mutabakatlar()
        {
            List<Mutabakat> list = new MutabakatBL().mutabakatlar();
            if ((list != null) && (list.Count > 0))
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }

        private void Mutabakatlar(int tip)
        {
            List<Mutabakat> list = new MutabakatBL().mutabakatlar(tip);
            if ((list != null) && (list.Count > 0))
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }

        private void MutabakatlarFilter(string query, List<filterParameters> list)
        {
            List<Mutabakat> list2 = new MutabakatBL().filter(query, list);
            if ((list2 != null) && (list2.Count > 0))
            {
                this.rptList.DataSource = list2;
                this.rptList.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            filterParameters parameters;
            this.checkLogin();
            List<filterParameters> list = new List<filterParameters>();
            string query = "Select * From tblMutabakatlar";
            string str2 = "";
            if (base.Request.QueryString["mt"] != null)
            {
                str2 = str2 + " Where mutabakatTipi = @tip";
                if (base.Request.QueryString["mt"] == "1")
                {
                    parameters = new filterParameters
                    {
                        name = "tip",
                        value = 1
                    };
                    list.Add(parameters);
                }
                else
                {
                    parameters = new filterParameters
                    {
                        name = "tip",
                        value = 2
                    };
                    list.Add(parameters);
                }
            }
            if (base.Request.QueryString["md"] != null)
            {
                str2 = (str2 == "") ? (str2 + " where durum = @durum") : (str2 + " and durum = @durum");
                if (base.Request.QueryString["md"] == "0")
                {
                    parameters = new filterParameters
                    {
                        name = "durum",
                        value = 0
                    };
                    list.Add(parameters);
                }
                if (base.Request.QueryString["md"] == "1")
                {
                    parameters = new filterParameters
                    {
                        name = "durum",
                        value = 1
                    };
                    list.Add(parameters);
                }
                if (base.Request.QueryString["md"] == "2")
                {
                    parameters = new filterParameters
                    {
                        name = "durum",
                        value = 2
                    };
                    list.Add(parameters);
                }
            }
            if (base.Request.QueryString["d"] != null)
            {
                str2 = (str2 == "") ? (str2 + " where donem = @donem") : (str2 + " and donem = @donem");
                parameters = new filterParameters
                {
                    name = "donem",
                    value = base.Request.QueryString["d"]
                };
                list.Add(parameters);
            }
            query = query + str2;
            if (str2 != "")
            {
                this.MutabakatlarFilter(query, list);
            }
            else
            {
                this.Mutabakatlar();
            }
        }
    }
}

