using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace muhasebe
{
    public partial class cari : System.Web.UI.Page
    {
        List<string> firmalarList = new List<string>();
        
        
        protected void fill()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("FİRMALAR", typeof(string));
            
            
            foreach (var item in this.companyList.Items)
            {
                dr = dt.NewRow();
                dr["FİRMALAR"] = item;
                dt.Rows.Add(dr);

            }
            this.GridView1.DataSource = dt;
          
            int counter = 0;
            foreach (GridViewRow oItem in GridView1.Rows)
            {
                oItem.Attributes.Add("id", counter.ToString());//here you can set row id ie(<tr>)
                counter++;
            }
           
            this.GridView1.DataBind();
           
        }
        private int controlFile(string fileName)
        {
            int num = 0;
            if (File.Exists(base.Server.MapPath("~/files/") + fileName + ".xls"))
            {
                num = 1;
            }
            else if (File.Exists(base.Server.MapPath("~/files/") + fileName + ".xlsx"))
            {
                num = 2;
            }
            return num;
        }

        private void getFileDetails(string fileName)
        {
            int startIndex = fileName.LastIndexOf(".") + 1;
            OleDbConnection selectConnection = (fileName.Substring(startIndex, fileName.Length - startIndex) != "xlsx") ? new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + base.Server.MapPath("~/files/") + fileName + ";Extended Properties='Excel 8.0;HDR=No'") : new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + base.Server.MapPath("~/files/") + fileName + "; Extended Properties='Excel 12.0 Xml;HDR=No'");
            DataTable dt = new DataTable();
            new OleDbDataAdapter("SELECT * FROM [Sheet$]", selectConnection).Fill(dt);
            List<Cari> list = (this.Session["Caris"] == null) ? new List<Cari>() : ((List<Cari>)this.Session["Caris"]);
            DataTable dataTable = new DataTable();
            new OleDbDataAdapter("SELECT * FROM [Sheet$H1:I1]", selectConnection).Fill(dataTable);
            string date = dataTable.Rows[0][0].ToString();
            List<Company> list2 = new CompanyBL().allCompanys();
            List<CariMutabakat> list3 = new CariMutabakatBL().mutabakatlar(date);
            int num4 = 0;
            if (dt.Rows.Count <= 0)
            {
                base.Response.Redirect("home.aspx");
            }
            else
            {
                num4 = (dt.Rows[dt.Rows.Count - 1][0].ToString() != "Genel Toplam") ? dt.Rows.Count : (dt.Rows.Count - 1);
            }
            int i = 3;
            while (true)
            {
                if (i >= num4)
                {
                    this.Session["Caris"] = list;
                    this.lbl_tarih.Text = date;
                    this.fill();
                    return;
                }
                ListItem item = new ListItem(dt.Rows[i][3].ToString(), Convert.ToString((int)(i - 3)));
                this.companyList.Items.Add(item);
                this.firmalarList.Add(dt.Rows[i][3].ToString());
                string email = "";
                if ((list2 != null) && (list2.Count > 0))
                {
                    List<Company> list4 = (from o in list2
                                           where o.unvan == dt.Rows[i][3].ToString()
                                           select o).ToList<Company>();
                    if (list4.Count > 0)
                    {
                        email = list4[0].email;
                    }
                }
                string borcTxt = "0 TL";
                if (dt.Rows[i][4].ToString() != "")
                {
                    borcTxt = Convert.ToDouble(dt.Rows[i][4].ToString()).ToString("#,#.00") + " ₺";
                    
                }
                string borcBakiyeTxt = "0 TL";
                if (dt.Rows[i][6].ToString() != "")
                {
                    borcBakiyeTxt = Convert.ToDouble(dt.Rows[i][6].ToString()).ToString("#,#.00") + " ₺";
                }
                string alacakTxt = "0 TL";
                if (dt.Rows[i][5].ToString() != "")
                {
                    alacakTxt = Convert.ToDouble(dt.Rows[i][5].ToString()).ToString("#,#.00") + " ₺";
                }
                string alacakBakiyeTxt = "0 TL";
                if (dt.Rows[i][7].ToString() != "")
                {
                    alacakBakiyeTxt = Convert.ToDouble(dt.Rows[i][7].ToString()).ToString("#,#.00") + " ₺";
                }
                bool flag5 = false;
                if (((list3 != null) && (list3.Count > 0)) && ((from o in list3
                                                                where ((o.vkno == dt.Rows[i][1].ToString().Replace(" ", "")) && ((o.unvan == dt.Rows[i][3].ToString()) && ((o.borc == borcTxt) && ((o.alacak == alacakTxt) && (o.borcBakiye == borcBakiyeTxt))))) && (o.alacakBakiye == alacakBakiyeTxt)
                                                                select o).ToList<CariMutabakat>().Count > 0))
                {
                    flag5 = true;
                }
                Cari cari1 = new Cari();
                cari1.ID = i - 3;
                cari1.vkno = dt.Rows[i][1].ToString().Replace(" ", "");
                cari1.muhasebeKodu = dt.Rows[i][0].ToString().Replace(" ", "");
                cari1.hesapKodu = dt.Rows[i][2].ToString().Replace(" ", "");
                cari1.unvan = dt.Rows[i][3].ToString();
                cari1.borc = borcTxt;
                cari1.alacak = alacakTxt;
                cari1.borcBakiye = borcBakiyeTxt;
                cari1.alacakBakiye = alacakBakiyeTxt;
                cari1.email = email;
                cari1.mailGonderim = flag5;
                cari1.tarih = date;
                list.Add(cari1);
                i++;
            }
            
            
        }
        protected void SelectCheckBox_OnCheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = "";
            string str2 = "";
            if (base.Request.QueryString["f"] == null)
            {
                if (this.Session["excel"] == null)
                {
                    base.Response.Redirect("home.aspx");
                }
                else
                {
                    fileName = this.Session["excel"].ToString();
                    this.ltr_fChange.Text = "\x00c7alışılan dosyayı değiştirmek i\x00e7in l\x00fctfen <a href=\"cari.aspx?d=" + fileName + "\">buraya</a> tıklayınız.";
                    this.getFileDetails(fileName);
                    this.Session["excel"] = fileName;
                    //this.companyList.Attributes.Add("onclick", "getDetails();");
                }
            }
            else
            {
                fileName = base.Request.QueryString["f"];
                if (this.controlFile(fileName) <= 0)
                {
                    base.Response.Redirect("home.aspx");
                }
                else
                {
                    str2 = (this.controlFile(fileName) != 1) ? "xlsx" : "xls";
                    fileName = fileName + "." + str2;
                    this.ltr_fChange.Text = "\x00c7alışılan dosyayı değiştirmek i\x00e7in l\x00fctfen <a href=\"cari.aspx?d=" + fileName + "\">buraya</a> tıklayınız.";
                    this.getFileDetails(fileName);
                    this.Session["excel"] = fileName;
                    //this.companyList.Attributes.Add("onclick", "getDetails();");
                }
            }
            if (base.Request.QueryString["d"] != null)
            {
                if (File.Exists(base.Server.MapPath("~/files/") + base.Request.QueryString["d"]))
                {
                    File.Delete(base.Server.MapPath("~/files/") + base.Request.QueryString["d"]);
                }
                this.Session["Caris"] = null;
                this.Session["excel"] = null;
                base.Response.Redirect("home.aspx");
            }
        }
    }
}
