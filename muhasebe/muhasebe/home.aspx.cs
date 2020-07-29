using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;


namespace muhasebe
{
    public partial class home : System.Web.UI.Page
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
                oItem.Attributes.Add("id", counter.ToString());
                counter++;
            }
            this.GridView1.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CariMutabakatBL tbl = new CariMutabakatBL();           
            int id = 99999;
            DataTable dt = new DataTable();
            dt.Load(tbl.getDocument(id));
            int lastItem = dt.Rows.Count;
            string name = dt.Rows[lastItem - 1]["Name"].ToString();
            byte[] docByte = (byte[])dt.Rows[lastItem - 1]["DocumentContent"];
            Response.ContentType = "application/octetstream";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", name));
            Response.AddHeader("Content-Length", docByte.Length.ToString());
            Response.BinaryWrite(docByte);
            Response.Flush();
            Response.Close();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
         
            if (this.fileUploadXML.HasFile)
            {
                try
                {
                    if (this.fileUploadXML.PostedFile.ContentLength >= 0x300000)
                    {
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "onload", "Alert('L\x00fctfen 3 MB dan k\x00fc\x00e7\x00fck bir dosya se\x00e7iniz')", true);
                    }
                    else
                    {
                        string str = Guid.NewGuid().ToString();
                        string fileName = Path.GetFileName(this.fileUploadXML.FileName);
                        int startIndex = this.fileUploadXML.FileName.LastIndexOf(".") + 1;
                        string str3 = this.fileUploadXML.FileName.Substring(startIndex, this.fileUploadXML.FileName.Length - startIndex);
                        this.fileUploadXML.SaveAs(base.Server.MapPath("~/files/") + str + "." + str3);
                        if (str3.ToLower() == "xml")
                        {
                            base.Response.Redirect("home.aspx?f=" + str);
                        }
                        else if ((str3.ToLower() == "xls") || (str3.ToLower() == "xlsx"))
                        {
                            base.Response.Redirect("cari.aspx?f=" + str);
                        }
                        else
                        {
                            base.Response.Redirect("home.aspx");
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "onload", "Alert('HATA :" + exception.Message + "')", true);
                }
            }
        }

        private void checkLogin()
        {
            if (this.Session["Admin"] == null)
            {
                base.Response.Redirect("/");
            }
        }

        private bool controlFile(string fileName) =>
            File.Exists(base.Server.MapPath("~/files/") + fileName + ".xml");

        private void getXMLDetails(string fileName)
        {
            XmlNodeList list;
            XmlTextReader reader = new XmlTextReader(base.Server.MapPath("~/files/" + fileName + ".xml"));
            XmlDocument document = new XmlDocument();
            document.Load(reader);
            XmlNode root = document.DocumentElement;
            int mutabakatTipi = 1;

            if (root.SelectNodes("/beyanname/ozel/satisBildirimi/bs").Count == 0)
            {
                list = root.SelectNodes("/beyanname/ozel/alisBildirimi/ba");
                mutabakatTipi = 2;
            }
            else
            {
                list = root.SelectNodes("/beyanname/ozel/satisBildirimi/bs");
            }
            XmlNodeList list2 = root.SelectNodes("/beyanname/genel/idari/donem/yil");
            XmlNodeList list3 = root.SelectNodes("/beyanname/genel/idari/donem/ay");
            string donem = list2.Item(0).InnerText.ToString() + " - ";
            string s = list3.Item(0).InnerText.ToString();
            uint num3 = (uint)Convert.ToInt32(s);

            switch (s)
            {
                case "1":
                    donem = donem + "Ocak";
                    break;
                case "2":
                    donem = donem + "Şubat";
                    break;
                case "3":
                    donem = donem + "Mart";
                    break;
                case "4":
                    donem = donem + "Nisan";
                    break;
                case "5":
                    donem = donem + "Mayıs";
                    break;
                case "6":
                    donem = donem + "Haziran";
                    break;
                case "7":
                    donem = donem + "Temmuz";
                    break;
                case "8":
                    donem = donem + "Ağustos";
                    break;
                case "9":
                    donem = donem + "Eylül";
                    break;
                case "10":
                    donem = donem + "Ekim";
                    break;
                case "11":
                    donem = donem + "Kasım";
                    break;
                case "12":
                    donem = donem + "Aralık";
                    break;
                default:
                    donem = donem;
                    break;
            }

            this.Session["donem"] = donem;
            List<Company> list4 = (this.Session["Companys"] == null) ? new List<Company>() : ((List<Company>)this.Session["Companys"]);
            List<Mutabakat> list5 = new MutabakatBL().doneminTumMutabakatlari(donem, mutabakatTipi);
            List<Company> list6 = new CompanyBL().allCompanys();
            int num2 = 0;

            foreach (XmlNode xn in list)
            {
                string vkno = "";
                try
                {
                    vkno = xn["vkno"].InnerText.ToString();
                }
                catch
                {
                    vkno = xn["tckimlikno"].InnerText.ToString();
                }
                vkno = vkno.Replace(" ", "");
                bool flag3 = false;
                if (((list5 != null) && (list5.Count > 0)) && ((from o in list5
                                                                where (((o.vkno == vkno) && (o.unvan == xn["unvan"].InnerText.ToString())) && (o.malHizmetBedeli == (Convert.ToDouble(xn["malHizmetBedeli"].InnerText.ToString()).ToString("#,#.00") + " ₺"))) && (o.belgeSayisi == int.Parse(xn["belgeSayisi"].InnerText.ToString()))
                                                                select o).ToList<Mutabakat>().Count > 0))
                {
                    flag3 = true;
                }
                string email = "";
                if ((list6 != null) && (list6.Count > 0))
                {
                    List<Company> list8 = (from o in list6
                                           where (o.vkno == vkno) && (o.unvan == xn["unvan"].InnerText.ToString())
                                           select o).ToList<Company>();
                    if (list8.Count > 0)
                    {
                        email = list8[0].email;
                    }
                }
                ListItem item = new ListItem(xn["unvan"].InnerText.ToString(), num2.ToString());

                this.companyList.Items.Add(item);
                this.firmalarList.Add(xn["unvan"].InnerText.ToString());
                Company company1 = new Company();
                company1.ID = num2;
                company1.unvan = xn["unvan"].InnerText.ToString();
                company1.vkno = vkno;
                company1.belgeSayisi = int.Parse(xn["belgeSayisi"].InnerText.ToString());
                company1.malHizmetBedeli = Convert.ToDouble(xn["malHizmetBedeli"].InnerText.ToString()).ToString("#,#.00") + " ₺";
                company1.email = email;
                company1.mailGonderim = flag3;
                company1.donemAy = short.Parse(list3.Item(0).InnerText.ToString());
                company1.donemYil = int.Parse(list2.Item(0).InnerText.ToString());
                company1.mutabakatTipi = mutabakatTipi;
                list4.Add(company1);
                num2++;
            }
            this.fill();
            this.Session["Companys"] = list4;
            reader.Close();
        }
        protected void SelectCheckBox_OnCheckedChanged(object sender, EventArgs e)
        {

        }
       

        protected void Page_Load(object sender, EventArgs e)
        {

            this.checkLogin();
            if (this.Session["Caris"] != null && this.Session["excel"] != null)
            {
                base.Response.Redirect("cari.aspx");
            }
            if (!this.IsPostBack) { 
                string fileName = "";
                if (base.Request.QueryString["f"] != null)
                {
                    fileName = base.Request.QueryString["f"];
                    if (!this.controlFile(fileName))
                    {
                        this.pnl_fileShow.Visible = true;
                    }
                    else
                    {
                        this.pnl_fileUpload.Visible = false;
                        this.pnl_proccess.Visible = true;
                        this.ltr_fChange.Text = "\x00c7alışılan dosyayı değiştirmek i\x00e7in l\x00fctfen <a href=\"home.aspx?d=" + fileName + "\">buraya</a> tıklayınız.";
                        this.getXMLDetails(fileName);
                        this.Session["xml"] = fileName;
                      
                        this.lbl_donem.Text = this.Session["donem"].ToString();
                    }
                }
                else if (this.Session["xml"] == null)
                {
                    this.pnl_fileShow.Visible = false;
                }
                else
                {
                    this.pnl_fileUpload.Visible = false;
                    this.pnl_proccess.Visible = true;
                    fileName = this.Session["xml"].ToString();
                    this.ltr_fChange.Text = "\x00c7alışılan dosyayı değiştirmek i\x00e7in l\x00fctfen <a href=\"home.aspx?d=" + fileName + "\">buraya</a> tıklayınız.";
                    this.getXMLDetails(fileName);
                    this.Session["xml"] = fileName;
                  
                    this.lbl_donem.Text = this.Session["donem"].ToString();
                }
                if (base.Request.QueryString["d"] != null)
                {
                    if (File.Exists(base.Server.MapPath("~/files/") + base.Request.QueryString["d"] + ".xml"))
                    {
                        File.Delete(base.Server.MapPath("~/files/") + base.Request.QueryString["d"] + ".xml");
                    }
                    this.Session["Companys"] = null;
                    this.Session["xml"] = null;
                    base.Response.Redirect("home.aspx");
                }
            
        }
        }



    }
}
