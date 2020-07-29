using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace muhasebe
{
    public partial class printcari : System.Web.UI.Page
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
            List<CariMutabakat> list = new CariMutabakatBL().mutabakatlar();
            if ((list != null) && (list.Count > 0))
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }

        private void MutabakatlarFilter(string query, List<filterParameters> list)
        {
            List<CariMutabakat> list2 = new CariMutabakatBL().filter(query, list);
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
                new CariMutabakatBL().delete(int.Parse(base.Request.QueryString["ID"]));
                base.Response.Redirect("cariarsiv.aspx");
            }
            if (!base.IsPostBack)
            {
                filterParameters parameters;
                List<filterParameters> list = new List<filterParameters>();
                string query = "Select * From tblCariMutabakatlar";
                string str2 = "";
                if (base.Request.QueryString["md"] != null)
                {
                    str2 = str2 + " Where durum = @durum";
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
                    str2 = (str2 == "") ? (str2 + " where tarih = @tarih") : (str2 + " and tarih = @tarih");
                    string str3 = base.Request.QueryString["d"];
                    string str4 = str3.Substring(0, 2);
                    string str5 = str3.Substring(6, 4);
                    string str6 = "";
                    switch (Convert.ToByte(str3.Substring(3, 2)))
                    {
                        case 1:
                            str6 = "Ocak";
                            break;

                        case 2:
                            str6 = "Şubat";
                            break;

                        case 3:
                            str6 = "Mart";
                            break;

                        case 4:
                            str6 = "Nisan";
                            break;

                        case 5:
                            str6 = "Mayıs";
                            break;

                        case 6:
                            str6 = "Haziran";
                            break;

                        case 7:
                            str6 = "Temmuz";
                            break;

                        case 8:
                            str6 = "Ağustos";
                            break;

                        case 9:
                            str6 = "Eyl\x00fcl";
                            break;

                        case 10:
                            str6 = "Ekim";
                            break;

                        case 11:
                            str6 = "Kasım";
                            break;

                        case 12:
                            str6 = "Aralık";
                            break;

                        default:
                            break;
                    }
                    string[] textArray1 = new string[] { str4, " ", str6, " ", str5 };
                    parameters = new filterParameters
                    {
                        name = "tarih",
                        value = string.Concat(textArray1)
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
}

