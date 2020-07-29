using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace muhasebe
{
    public partial class cariarsiv : System.Web.UI.Page
    {
        protected void btn_filter_Click(object sender, EventArgs e)
        {
            string url = "cariarsiv.aspx";
            if (this.ddlDurum.SelectedValue != "all")
            {
                url = (url == "cariarsiv.aspx") ? (url + "?md=" + this.ddlDurum.SelectedValue) : (url + "&md=" + this.ddlDurum.SelectedValue);
            }
            if (this.txt_tarih.Text.Length > 8)
            {
                url = (url == "cariarsiv.aspx") ? (url + "?d=" + this.txt_tarih.Text) : (url + "&d=" + this.txt_tarih.Text);
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
                string str3 = "<a href=\"printcari.aspx";
                if (base.Request.QueryString["md"] != null)
                {
                    str2 = str2 + " Where durum = @durum";
                    str3 = str3 + "?md=";
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
                        str2 = str2 + " and (tarih = @tarih or tarih = @dtarih)";
                        str3 = str3 + "&d=";
                    }
                    else
                    {
                        str2 = str2 + " where tarih = @tarih or tarih = @dtarih";
                        str3 = str3 + "?d=";
                    }
                    string str4 = base.Request.QueryString["d"];
                    this.txt_tarih.Text = str4;
                    string str5 = str4.Substring(0, 2);
                    string str6 = str4.Substring(6, 4);
                    string str7 = "";
                    string str8 = "";
                    switch (Convert.ToByte(str4.Substring(3, 2)))
                    {
                        case 1:
                            str7 = "Ocak";
                            str8 = "01";
                            break;

                        case 2:
                            str7 = "Şubat";
                            str8 = "01";
                            break;

                        case 3:
                            str7 = "Mart";
                            str8 = "03";
                            break;

                        case 4:
                            str7 = "Nisan";
                            str8 = "04";
                            break;

                        case 5:
                            str7 = "Mayıs";
                            str8 = "05";
                            break;

                        case 6:
                            str7 = "Haziran";
                            str8 = "06";
                            break;

                        case 7:
                            str7 = "Temmuz";
                            str8 = "07";
                            break;

                        case 8:
                            str7 = "Ağustos";
                            str8 = "08";
                            break;

                        case 9:
                            str7 = "Eyl\x00fcl";
                            str8 = "09";
                            break;

                        case 10:
                            str7 = "Ekim";
                            str8 = "10";
                            break;

                        case 11:
                            str7 = "Kasım";
                            str8 = "11";
                            break;

                        case 12:
                            str7 = "Aralık";
                            str8 = "12";
                            break;

                        default:
                            break;
                    }
                    string[] textArray1 = new string[] { str5, " ", str7, " ", str6 };
                    string[] textArray2 = new string[] { str5, ".", str8, ".",str6 };
                    parameters = new filterParameters
                    {
                        name = "tarih",
                        value = string.Concat(textArray1)
                    };

                    filterParameters parameters2 = new filterParameters
                    {
                        name = "dtarih",
                        value = string.Concat(textArray2)
                    };


                    list.Add(parameters);
                    list.Add(parameters2);
                    str3 = str3 + base.Request.QueryString["d"];
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
                str3 = str3 + "\" target=\"_blank\"><img src=\"img/print.png\" alt=\"Print\"><a/>";
                this.ltr_print.Text = str3;
            }
        }
    }
}
