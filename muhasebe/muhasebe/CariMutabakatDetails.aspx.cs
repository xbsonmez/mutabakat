using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace muhasebe
{
    public partial class CariMutabakatDetails : System.Web.UI.Page
    {
        int mutabakatID;
        CariMutabakatBL tbl = new CariMutabakatBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.checkLogin();
            if (base.Request.QueryString["ID"] != null)
            {
                this.mutabakatID = int.Parse(base.Request.QueryString["ID"]);
                List<CariMutabakat> list = new CariMutabakatBL().getCariMutabakatDetail(this.mutabakatID);
                List<CariMutabakatDoc> customerAnswer = new CariMutabakatBL().getDocumentContext(this.mutabakatID);
              


                if (customerAnswer.Count > 0)
                {
                    this.descDetail.Text = customerAnswer[0].Description.ToString();
                    if (customerAnswer[0].Name.ToString().Length > 0 && customerAnswer[0].Extn.ToString().Length > 0)
                    {
                        this.fileName.Text = customerAnswer[0].Name.ToString();
                    }
                    else
                    {
                        this.fileName.Text = "Müşteri tarafından eklenen dosya bulunmamaktadır.";
                        this.saveFile.Visible = false;
                    }

                }
                else
                {
                    this.descDetail.Text = "Müşterinin Açıklaması bulunmamaktadır.";
                    this.fileName.Text = "Müşteri tarafından eklenen dosya bulunmamaktadır.";
                    this.saveFile.Visible = false;
                }
                if (list != null)
                {
                    this.unvanCompany.Text = list[0].unvan.ToString();
                    this.vkno.Text = list[0].vkno.ToString();
                    this.tarih.Text = list[0].tarih.ToString();
                    this.yanitlayanMail.Text = list[0].yanitlayanMail.ToString();
                    this.gonderilmeTarihi.Text = list[0].gonderilmeTarihi.ToString();

                    this.alacakbakiyesi.Text = list[0].alacakBakiye.ToString().Replace("TL", "₺");
                    this.alacak.Text= list[0].alacak.ToString().Replace("TL", "₺");
                    this.borcbakiye.Text = list[0].borcBakiye.ToString().Replace("TL", "₺");
                    this.borc.Text = list[0].borc.ToString().Replace("TL", "₺");
                    this.hesapKodu.Text = list[0].hesapKodu.ToString();
                    this.muhasebeKodu.Text = list[0].muhasebeKodu.ToString();



                    this.Mailler.Text = list[0].email.ToString();
                    if (list[0].durum.ToString() == "2")
                    {
                        this.durum.Text = "Mutabık Değil";
                    }
                    else if (list[0].durum.ToString() == "1")
                    {
                        this.durum.Text = "Mutabık";
                    }
                    if (list[0].durum.ToString() == "0")
                    {
                        this.durum.Text = "Bekliyor";
                    }                 

                }
            }
            else
            {
                base.Response.Redirect("cariarsiv.aspx");
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
            if (this.Mailler.ReadOnly)
            {
                this.alacakbakiyesi.ReadOnly = false;
                this.alacak.ReadOnly = false;
                this.borcbakiye.ReadOnly = false;
                this.borc.ReadOnly = false;
                this.Mailler.ReadOnly = false;
                this.borcbakiye.Attributes.Add("style", "background-color:yellow");
                this.borc.Attributes.Add("style", "background-color:yellow");
                this.alacakbakiyesi.Attributes.Add("style", "background-color:yellow");
                this.alacak.Attributes.Add("style", "background-color:yellow");
                this.Mailler.Attributes.Add("style", "background-color:yellow");
                this.BtnEdit.Text = "Vazgeç";
                this.Button1.Visible = true;

            }
            else
            {
                this.alacakbakiyesi.Attributes.Add("style", "background-color:white");
                this.alacak.Attributes.Add("style", "background-color:white");
                this.borcbakiye.Attributes.Add("style", "background-color:white");
                this.borc.Attributes.Add("style", "background-color:white");
                this.Mailler.Attributes.Add("style", "background-color:white");

                this.alacakbakiyesi.ReadOnly = true;
                this.alacak.ReadOnly = true;
                this.borcbakiye.ReadOnly = true;
                this.borc.ReadOnly = true;
                this.Mailler.ReadOnly = true;
                this.BtnEdit.Text = "Düzenle";
                this.Button1.Visible = false;


            }

        }
        protected void reSendMailAndSaveDetail(object sender, EventArgs e)
        {
            string borc = Request.Form["borc"];
            string borcBakiye = Request.Form["borcBakiye"];
            string alacakBakiyesi = Request.Form["alacakBakiyesi"];
            string alacak = Request.Form["alacak"];
            string mailler = Request.Form["Mailler"];



            List<Admin> list2 = (List<Admin>)base.Session["Admin"];
            string mail = list2[0].mail;
            string mailPass = list2[0].mailPass;

            if (this.tbl.updateMutabakatToSendAgain(this.mutabakatID, borc, borcBakiye, alacakBakiyesi, alacak, mailler, 0, mail) <= 0)
            {
               
                base.Response.Redirect("404.aspx");
            }
            if (this.SendMailAgain(borc, borcBakiye, alacakBakiyesi, alacak, mailler, mail, mailPass) > 0)
            {
                base.Response.Redirect("cariarsiv.aspx");
            }

        }
        protected int SendMailAgain(string borcTxt, string borcBakiyeTxt, string alacakBakiyesiTxt, string alacakTxt, string maillerTxt, string mailTxt, string mailPass)
        {
            eMailBL eMailProcc = new eMailBL();
     

            List<CariMutabakat> list = new CariMutabakatBL().getCariMutabakatDetail(this.mutabakatID);
            string unvan = list[0].unvan;
            string hesapKodu = list[0].hesapKodu;
            string muhasebeKodu = list[0].muhasebeKodu;
            string email = maillerTxt;
            string alacak = alacakTxt.ToString().Replace("TL", "₺");
            string alacakBakiye = alacakBakiyesiTxt.ToString().Replace("TL", "₺");
            string borc = borcTxt.ToString().Replace("TL", "₺");
            string borcBakiye = borcBakiyeTxt.ToString().Replace("TL", "₺");
            string tarih = list[0].tarih;
            string vkno = list[0].vkno;
            char[] separator = new char[] { ';' };
            string[] strArray = email.Split(separator);
            bool flag2 = false;
            int num2 = 0;
            string newValue = Guid.NewGuid().ToString().Replace("-", "");
            List<Admin> list2 = (List<Admin>)base.Session["Admin"];
            string str14 = "imza_" + list2[0].ID.ToString();
            if (strArray.Length != 0)
            {
                string str15 = Convert.ToString(ReadHtmlFile(base.Server.MapPath("~/mail_files/CARI_MAIL_FORMAT.html"))).Replace("[VKNO]", vkno).Replace("[UNVAN]", unvan).Replace("[TARIH]", tarih).Replace("[MUTABAKATID]", newValue).Replace("[IMZA]", str14);
                str15 = ((alacakBakiye == "") || (alacakBakiye == "0 TL")) ? ((borcBakiye != "0 TL") ? str15.Replace("[TUTAR]", borcBakiye).Replace("[DURUM]", "BORCUNUZ") : str15.Replace("[TUTAR]", borcBakiye).Replace("[DURUM]", "")) : str15.Replace("[TUTAR]", alacakBakiye).Replace("[DURUM]", "ALACAĞINIZ");
                string subject = "ADALICAM " + tarih.ToUpper() + " CARİ MUTABAKATI‏";
                int index = 0;
                while (true)
                {
                    if (index >= strArray.Length)
                    {
                        break;
                    }
                    flag2 = false;
                    if (eMailProcc.send(strArray[index], subject, str15.Replace("[MAIL]", strArray[index]), mailTxt, mailTxt, mailPass))
                    {
                        num2++;
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