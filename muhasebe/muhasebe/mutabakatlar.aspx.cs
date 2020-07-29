using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace muhasebe
{
    public partial class mutabakatlar : System.Web.UI.Page
    {
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            string url = "mutabakatlar.aspx";
            if (this.ddl_tip.SelectedValue != "0")
            {
                url = url + "?mt=" + this.ddl_tip.SelectedValue;
            }
            if (this.ddlDurum.SelectedValue != "all")
            {
                url = (url == "mutabakatlar.aspx") ? (url + "?md=" + this.ddlDurum.SelectedValue) : (url + "&md=" + this.ddlDurum.SelectedValue);
            }
            if (this.ddlDonemAy.SelectedValue != "0")
            {
                string str2 = this.ddlYil.SelectedValue + " - " + this.ddlDonemAy.SelectedItem.Text;
                url = (url == "mutabakatlar.aspx") ? (url + "?d=" + str2) : (url + "&d=" + str2);
            }
            if (this.ddlDonemAy.SelectedValue == "0" && this.ddlYil.SelectedValue!="")
            {
                string str2 = this.ddlYil.SelectedValue + " - " +"all";
                url = (url == "mutabakatlar.aspx") ? (url + "?y=" + str2) : (url + "&y=" + str2);
            }

            base.Response.Redirect(url);
        }

        private void checkLogin()
        {
            if (this.Session["Admin"] == null)
            {
                base.Response.Redirect("home.aspx");
            }
        }

        private void donemYillar()
        {
            int num = int.Parse(DateTime.Now.Year.ToString());
            int num3 = 0;
            while (true)
            {
                if (num3 >= 10)
                {
                    this.ddlYil.SelectedIndex = 0;
                    return;
                }
                this.ddlYil.Items.Add(Convert.ToString((int)(num - num3)));
                num3++;
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
            this.checkLogin();
            if (base.Request.QueryString["ID"] != null)
            {
                new MutabakatBL().delete(int.Parse(base.Request.QueryString["ID"]));
                base.Response.Redirect("mutabakatlar.aspx");
            }
            if (!base.IsPostBack)
            {
                filterParameters parameters;
                this.donemYillar();
                List<filterParameters> list = new List<filterParameters>();
                string query = "Select * From tblMutabakatlar";
                string str2 = "";
                string str3 = "<a href=\"print.aspx";
                if (base.Request.QueryString["mt"] != null)
                {
                    str2 = str2 + " Where mutabakatTipi = @tip";
                    if (base.Request.QueryString["mt"] == "1")
                    {
                        this.ddl_tip.SelectedIndex = 1;
                        parameters = new filterParameters
                        {
                            name = "tip",
                            value = 1
                        };
                        list.Add(parameters);
                        str3 = str3 + "?mt=1";
                    }
                    else
                    {
                        this.ddl_tip.SelectedIndex = 2;
                        parameters = new filterParameters
                        {
                            name = "tip",
                            value = 2
                        };
                        list.Add(parameters);
                        str3 = str3 + "?mt=2";
                    }
                }
                if (base.Request.QueryString["md"] != null)
                {
                    if (str2 != "")
                    {
                        str2 = str2 + " and durum = @durum";
                        str3 = str3 + "&md=";
                    }
                    else
                    {
                        str2 = str2 + " where durum = @durum";
                        str3 = str3 + "?md=";
                    }
                    if (base.Request.QueryString["md"] == "0")
                    {
                        this.ddlDurum.SelectedIndex = 1;
                        parameters = new filterParameters
                        {
                            name = "durum",
                            value = 0
                        };
                        list.Add(parameters);
                        str3 = str3 + "0";
                    }
                    if (base.Request.QueryString["md"] == "1")
                    {
                        this.ddlDurum.SelectedIndex = 2;
                        parameters = new filterParameters
                        {
                            name = "durum",
                            value = 1
                        };
                        list.Add(parameters);
                        str3 = str3 + "1";
                    }
                    if (base.Request.QueryString["md"] == "2")
                    {
                        this.ddlDurum.SelectedIndex = 3;
                        parameters = new filterParameters
                        {
                            name = "durum",
                            value = 2
                        };
                        list.Add(parameters);
                        str3 = str3 + "2";
                    }
                }
                if (base.Request.QueryString["d"] != null)
                {
                    if (str2 != "")
                    {
                        str2 = str2 + " and donem = @donem";
                        str3 = str3 + "&d=";
                    }
                    else
                    {
                        str2 = str2 + " where donem = @donem";
                        str3 = str3 + "?d=";
                    }
                    string str4 = base.Request.QueryString["d"];
                    this.ddlDonemAy.SelectedValue = str4.Substring(7, str4.Length - 7);
                    this.ddlYil.SelectedValue = str4.Substring(0, 4);
                    parameters = new filterParameters
                    {
                        name = "donem",
                        value = str4
                    };
                    list.Add(parameters);
                    str3 = str3 + str4;
                }
                if (base.Request.QueryString["y"] != null)
                {
                    if (str2 != "")
                    {
                        str2 = str2 + " and  donem LIKE @donem";
                        str3 = str3 + "&d=";
                    }
                    else
                    {
                        str2 = str2 + " where donem LIKE @donem";
                        str3 = str3 + "?d=";
                    }
                    string str4 = base.Request.QueryString["y"];
                    this.ddlYil.SelectedValue = str4.Substring(0, 4);
                    parameters = new filterParameters
                    {
                        name = "donem",
                        value = "%" +this.ddlYil.SelectedValue + "%"
                    };
                    list.Add(parameters);
                    str3 = str3 + str4;
                }
                query = query + str2;
                if (str2 != "")
                {
                    this.MutabakatlarFilter(query, list);
                }
                else
                {
                    string thisYear = DateTime.Now.Year.ToString() ;
                    parameters = new filterParameters
                    {
                        name = "donem",
                        value = '%' + this.ddlYil.SelectedValue + '%'
                    };
                    list.Add(parameters);
                    query = query + " where donem LIKE  @donem ";
                     this.MutabakatlarFilter(query, list);
                     
                 // this.Mutabakatlar();
                }
                str3 = str3 + "\" target=\"_blank\"><img src=\"img/print.png\" alt=\"Print\"><a/>";
                this.ltr_print.Text = str3;
            }
        }
    }
}

