using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using Entity;

namespace muhasebe
{
    public partial class MutabakatDetails : System.Web.UI.Page
    {

        MutabakatBL tbl = new MutabakatBL();
        int mutabakatID;
        protected void Page_Load(object sender, EventArgs e)
        {
            
           
            this.checkLogin();
            if (base.Request.QueryString["ID"] != null)
            {
                this.mutabakatID = int.Parse(base.Request.QueryString["ID"]);
                
                List<MutabakatDoc> customerAnswer= new MutabakatBL().getDocumentContext(this.mutabakatID);

               
                if (customerAnswer.Count>0) { 
                this.descDetail.Text = customerAnswer[0].Description.ToString() ;
                if (customerAnswer[0].Name.ToString().Length>0 && customerAnswer[0].Extn.ToString().Length>0) { 
                this.fileName.Text = customerAnswer[0].Name.ToString();
                }
                else
                {
                    this.fileName.Text = "Müşteri tarafından eklenen dosya bulunmamaktadır.";
                    this.saveFile.Visible = false;
                }
                }
                else { 
                this.descDetail.Text ="Müşterinin Açıklaması bulunmamaktadır.";
                this.fileName.Text = "Müşteri tarafından eklenen dosya bulunmamaktadır.";
                this.saveFile.Visible = false;
                }

               List< Mutabakat> mutabakatList = new MutabakatBL().mutabakatWithID(this.mutabakatID);
                if (mutabakatList != null)
                {
                    this.unvanCompany.Text = mutabakatList[0].unvan.ToString();
                    this.vkno.Text = mutabakatList[0].vkno.ToString();
                    this.donem.Text = mutabakatList[0].donem.ToString();
                    this.yanitlayanMail.Text = mutabakatList[0].yanitlayanMail.ToString();
                    this.gonderilmeTarihi.Text = mutabakatList[0].gonderilmeTarihi.ToString();

                    this.MalHizmetBedeli.Text = mutabakatList[0].malHizmetBedeli.ToString().Replace("TL", "₺");
               
                    this.Mailler.Text = mutabakatList[0].email.ToString();
                    if (mutabakatList[0].durum.ToString() == "2")
                    {
                        this.durum.Text = "Mutabık Değil";
                    }
                    else if (mutabakatList[0].durum.ToString() == "1")
                    {
                        this.durum.Text = "Mutabık";
                    }
                    if (mutabakatList[0].durum.ToString() == "0")
                    {
                        this.durum.Text = "Bekliyor";
                    }


                    this.belgeSayisi.Text = mutabakatList[0].belgeSayisi.ToString();

                }


            }
            else
            {
                base.Response.Redirect("mutabakatlar.aspx");
            }
        }
        private void checkLogin()
        {
            if (this.Session["Admin"] == null)
            {
                base.Response.Redirect("home.aspx");
            }
        }
        protected void btnGet_Click(object sender, EventArgs e)
        {


          
            DataTable dt = new DataTable();
            dt.Load(this.tbl.getDocument(this.mutabakatID));
            int lastItem = dt.Rows.Count;
            string name = dt.Rows[0]["Name"].ToString();
            byte[] docByte = (byte[])dt.Rows[0]["DocumentContent"];
         
            Response.ContentType = "application/octetstream";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", name));
            Response.AddHeader("Content-Length", docByte.Length.ToString());
            Response.BinaryWrite(docByte);
            Response.Flush();
            Response.Close();


        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.belgeSayisi.ReadOnly)
            {
                this.belgeSayisi.ReadOnly = false;
                this.MalHizmetBedeli.ReadOnly = false;
                this.Mailler.ReadOnly = false;
                this.belgeSayisi.Attributes.Add("style", "background-color:yellow");
                this.MalHizmetBedeli.Attributes.Add("style", "background-color:yellow");
                this.Mailler.Attributes.Add("style", "background-color:yellow");
                this.BtnEdit.Text = "Vazgeç";
                this.Button1.Visible = true;

            }
            else
            {
                this.belgeSayisi.Attributes.Add("style", "background-color:white");
                this.MalHizmetBedeli.Attributes.Add("style", "background-color:white");
                this.Mailler.Attributes.Add("style", "background-color:white");

                this.belgeSayisi.ReadOnly = true;
                this.MalHizmetBedeli.ReadOnly = true;
                this.Mailler.ReadOnly = true;
                this.BtnEdit.Text = "Düzenle";
                this.Button1.Visible = false ;


            }

        }
        protected void reSendMailAndSaveDetail(object sender, EventArgs e)
        {
            int belgeNumber = Int32.Parse( Request.Form["belgeSayisi"]);
            string hizmetBedeli= Request.Form["MalHizmetBedeli"];
            string mailler = Request.Form["Mailler"];



            List<Admin> list2 = (List<Admin>)base.Session["Admin"];
            string mail = list2[0].mail;
            string mailPass = list2[0].mailPass;

            if (this.tbl.updateMutabakatToSendAgain(this.mutabakatID, belgeNumber, hizmetBedeli, mailler, 0, mail) <= 0)
            {
                base.Response.Redirect("404.aspx");
            }
            if (this.SendMailAgain(belgeNumber, hizmetBedeli, mailler, 0, mail, mailPass) > 0)
            {
                base.Response.Redirect("mutabakatlar.aspx");
            }

        }
        protected int SendMailAgain(int belgeNumber, string hizmetBedeli,string mailler, int durum,string  mail, string mailPass)
        {
           eMailBL eMailProcc = new eMailBL();
        List < Mutabakat > mutabakatList = new MutabakatBL().mutabakatWithID(this.mutabakatID);


            string unvan = mutabakatList[0].unvan.ToString();
            string vkno = mutabakatList[0].vkno.ToString() ;
            string email = mailler;
            string newValue = belgeNumber.ToString();
            string malHizmetBedeli =hizmetBedeli.ToString().Replace("TL", "₺");
         
            string donemYil =mutabakatList[0].donem.ToString();
            int mutabakatTipi = mutabakatList[0].mutabakatTipi;
            char[] separator = new char[] { ';' };
            string[] strArray = email.Split(separator);
            int num5 = 0;
            string str7 = mutabakatList[0].mutabakatID.ToString();
            List<Admin> list2 = (List<Admin>)base.Session["Admin"];
  
            string str10 = "imza_" + list2[0].ID.ToString();
            if (strArray.Length != 0)
            {
                string str11 = Convert.ToString(ReadHtmlFile(base.Server.MapPath("~/mail_files/MAIL_FORMAT.html"))).Replace("[VKNO]", vkno).Replace("[UNVAN]", unvan).Replace("[DONEM]", donemYil).Replace("[BELGESAYISI]", newValue).Replace("[MALHIZMETBEDELI]", malHizmetBedeli).Replace("[MUTABAKATID]", str7).Replace("[IMZA]", str10);
                str11 = (mutabakatTipi != 1) ? str11.Replace("[TIP]", "BA") : str11.Replace("[TIP]", "BS");
                string str12 = donemYil;
                str12 = donemYil + " " + str12.ToUpper();
                string subject = "ADALICAM " + str12 + " BS MUTABAKATI‏";
                if (mutabakatTipi == 2)
                {
                    subject = "ADALICAM " + str12 + " BA MUTABAKATI‏";
                }
                int index = 0;
                while (true)
                {
                    if (index >= strArray.Length)
                    {
                        break;
                    }
                    if (eMailProcc.send(strArray[index], subject, str11.Replace("[MAIL]", strArray[index]), mail, mail, mailPass))
                    {
                        num5++;
                    }
                    index++;
                }
            }



            return 1;
        }
        public static StringBuilder ReadHtmlFile(string htmlFileNameWithPath)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                using (StreamReader reader = new StreamReader(htmlFileNameWithPath))
                {
                    while (true)
                    {
                        string str = reader.ReadLine();
                        if (str == null)
                        {
                            break;
                        }
                        builder.Append(str);
                    }
                }
            }
            catch (Exception exception1)
            {
                throw exception1;
            }
            return builder;
        }

    }
}