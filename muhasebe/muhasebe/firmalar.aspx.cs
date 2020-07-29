using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace muhasebe
{
    public partial class firmalar : System.Web.UI.Page
    {
        private List<Company> lst;
        private CompanyBL compBL = new CompanyBL();
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            int num = int.Parse(base.Request.QueryString["id"]);
            this.compBL.edit(num, this.txt_mail.Text.Replace(Environment.NewLine, ";"));
            base.Response.Redirect("firmalar.aspx");
        }

        private void checkLogin()
        {
            if (this.Session["Admin"] == null)
            {
                base.Response.Redirect("home.aspx");
            }
        }

        private void getCompanys()
        {
            this.lst = this.compBL.allCompanys();
            if ((this.lst != null) && (this.lst.Count > 0))
            {
                this.rptList.DataSource = this.lst;
                this.rptList.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.checkLogin();
            if (!base.IsPostBack)
            {
                this.getCompanys();
                if (base.Request.QueryString["cmd"] != null)
                {
                    byte num = Convert.ToByte(base.Request.QueryString["cmd"]);
                    int id = int.Parse(base.Request.QueryString["id"]);
                    byte num2 = num;
                    if (num2 != 1)
                    {
                        if (num2 == 2)
                        {
                            this.compBL.delete(id);
                            base.Response.Redirect("firmalar.aspx");
                        }
                    }
                    else
                    {
                        List<Company> list = (from o in this.lst
                                              where o.ID == id
                                              select o).ToList<Company>();
                        if (list.Count > 0)
                        {
                            this.txt_firma.Text = list[0].unvan.ToString();
                            this.txt_vkno.Text = list[0].vkno.ToString();
                            this.txt_mail.Text = list[0].email.ToString().Replace(";", Environment.NewLine);
                        }
                        this.pnl_edit.Visible = true;
                    }
                }
            }
        }
    }
}

