using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using BL;
using Entity;

namespace muhasebe
{
    public partial class master : System.Web.UI.MasterPage
    {

        private void adminLogin(string user, string pass)
        {
            
            List<Admin> list = new AdminBL().login(this.txt_user.Text, this.txt_pass.Text);
            if ((list == null) || (list.Count <= 0))
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "onload", "Alert('Hatalı kullanıcı adı veya şifre')", true);
            }
            else
            {
                if (this.chk_remember.Checked)
                {
                    HttpCookie cookie = new HttpCookie("AdminInfo");
                    cookie.Values.Add("USERNAME", user);
                    cookie.Values.Add("PASSWORD", pass);
                    cookie.Expires = DateTime.Now.Date.AddDays(365.0);
                    base.Response.AppendCookie(cookie);
                }
                this.Session["Admin"] = list;
                foreach (Admin admin in list)
                {
                    this.Session["NameSurname"] = admin.NameSurname;
                }
                base.Response.Redirect("home.aspx");
            }


        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('"+ this.txt_user.Text.Trim()+ this.txt_pass.Text + "');</script>");
            this.adminLogin(this.txt_user.Text, this.txt_pass.Text);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (this.Session["Admin"] != null)
                {
                    base.Response.Redirect("home.aspx");
                }
                if (base.Request.Cookies["AdminInfo"] != null)
                {
                    this.txt_user.Text = base.Request.Cookies["AdminInfo"]["USERNAME"].ToString();
                    this.txt_pass.Text = base.Request.Cookies["AdminInfo"]["PASSWORD"].ToString();
                }
            }
        }
    }
}
