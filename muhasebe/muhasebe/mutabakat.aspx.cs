using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace muhasebe
{
    public partial class mutabakat : System.Web.UI.Page
    {
        int mutabakatIDForFile;
        int status;
        string returnMail;
      

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txt_description.Attributes.Add("maxlength", "254");
            }
            if (((base.Request.QueryString["r"] == null) || (base.Request.QueryString["rm"] == null)) || (base.Request.QueryString["mid"] == null))
            {
                base.Response.Redirect("404.aspx");
            }
            else
             {
                this.status = int.Parse(base.Request.QueryString["r"]);
                this.returnMail = base.Request.QueryString["rm"];
                string mutabakatID = base.Request.QueryString["mid"];
                string str3 = "MUTABIKIZ";
                this.saveFile.Visible = false;
                this.fileUpload.Visible = false;
                this.txt_description.Visible = false;
                this.why_txt.Visible = false;
                this.maxTextLimit.Visible = false;
                if (this.status == 2)
                {
                    str3 = "MUTABIK DEĞİLİZ";
                  
                }
                MutabakatBL tbl = new MutabakatBL();
                List<Mutabakat> list = tbl.mutabakat(mutabakatID);
                if ((list == null) || (list.Count <= 0))
                {
                    base.Response.Redirect("404.aspx");
                }
                else
                {
                    int num2 = int.Parse(list[0].ID.ToString());
                    this.mutabakatIDForFile = num2;
                    int num3 = int.Parse(list[0].durum.ToString());
                    string gonderen = list[0].gonderen;
                    string str5 = "BS";
                    if (list[0].mutabakatTipi == 2)
                    {
                        str5 = "BA";
                    }
                    if(this.status == 2 && num3==0)
                    {
                        this.saveFile.Visible = true;
                        this.fileUpload.Visible = true;
                        this.txt_description.Visible = true;
                        this.why_txt.Visible = true;
                        this.maxTextLimit.Visible = true;

                    }
                    this.ltr_ip.Text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() + "IP adresi ile sistemde işlem yapmaktasınız";
                    switch (num3)
                    {
                        case 0:
                          
                            if (this.status == 1 && tbl.UpdateStatus(num2, this.returnMail, this.status) <= 0)
                            {
                                base.Response.Redirect("404.aspx");
                            }
                          
                            else
                            {
                                string[] textArray1 = new string[14];
                                textArray1[0] = "<strong>";
                                textArray1[1] = list[0].vkno.ToString();
                                textArray1[2] = " - ";
                                textArray1[3] = list[0].unvan.ToString();
                                textArray1[4] = "</strong>\x00a0\x00fcnvanlı firmanız i\x00e7in\x00a0 <br /><strong>";
                                textArray1[5] = list[0].donem.ToString();
                                textArray1[6] = "</strong> d\x00f6nemine ait\x00a0<strong>Form ";
                                textArray1[7] = str5;
                                textArray1[8] = "</strong>\x00a0mutabakat bilgilerini <strong>\"";
                                textArray1[9] = str3;
                                textArray1[10] = "\"</strong> olarak yanıtladınız. <br /> Belge Adedi : ";
                                textArray1[11] = list[0].belgeSayisi.ToString();
                                textArray1[12] = " <br /> Fatura Tutarı : ";
                                textArray1[13] = list[0].malHizmetBedeli;
                                this.ltr_durum.Text = string.Concat(textArray1);
                                string[] textArray2 = new string[] { list[0].donem.ToString(), " D\x00d6NEMLİ ", list[0].unvan.ToString(), " FORM ", str5, " MUTABAKAT YANITI" };
                                string subject = string.Concat(textArray2);
                                string mailBody = "<div style=\"font-family:arial; font-size:12px;\">";
                                string[] textArray3 = new string[0x16];
                                textArray3[0] = mailBody;
                                textArray3[1] = "<strong>";
                                textArray3[2] = list[0].gonderilmeTarihi.ToString();
                                textArray3[3] = "</strong> tarihinde <strong><a href=\"mailto:";
                                textArray3[4] = list[0].gonderen;
                                textArray3[5] = "\">";
                                textArray3[6] = list[0].gonderen;
                                textArray3[7] = "</a></strong> tarafından g\x00f6nderilen <br /> <strong>";
                                textArray3[8] = list[0].vkno;
                                textArray3[9] = " - ";
                                textArray3[10] = list[0].unvan;
                                textArray3[11] = "</strong> \x00fcnvanlı, <br /><strong>";
                                textArray3[12] = list[0].donem;
                                textArray3[13] = "</strong> d\x00f6nemine ait <strong>Form ";
                                textArray3[14] = str5;
                                textArray3[15] = " </strong> mutabakatı <strong><a href=\"mailto:";
                                textArray3[0x10] = this.returnMail;
                                textArray3[0x11] = "\">";
                                textArray3[0x12] = this.returnMail;
                                textArray3[0x13] = "</a></strong> tarafından <strong>";
                                textArray3[20] = str3;
                                textArray3[0x15] = "</strong> olarak yanıtlanmıştır.";
                                mailBody = string.Concat(textArray3) + "</div>";
                                eMailBL lbl = new eMailBL();
                                lbl.informationSend("muharrem@adalicam.com.tr", subject, mailBody);
                                lbl.informationSend("onurturan@adalicam.com.tr", subject, mailBody);
                                if (list[0].gonderen == "adem@adalicam.com.tr")
                                {
                                    lbl.informationSend("adem@adalicam.com.tr", subject, mailBody);
                                }
                            }
                            break;

                        case 1:
                            {
                                string[] textArray4 = new string[12];
                                textArray4[0] = "<strong>";
                                textArray4[1] = list[0].vkno.ToString();
                                textArray4[2] = " - ";
                                textArray4[3] = list[0].unvan.ToString();
                                textArray4[4] = "</strong>\x00a0\x00fcnvanlı firmanız i\x00e7in\x00a0 <br /><strong>";
                                textArray4[5] = list[0].donem.ToString();
                                textArray4[6] = "</strong> d\x00f6nemine ait\x00a0<strong>Form ";
                                textArray4[7] = str5;
                                textArray4[8] = "</strong>\x00a0mutabakat bilgilerini  daha \x00f6nce <strong>\"MUTABIKIZ\"</strong> olarak yanıtladınız. <br /> Belge Adedi : ";
                                textArray4[9] = list[0].belgeSayisi.ToString();
                                textArray4[10] = " <br /> Fatura Tutarı : ";
                                textArray4[11] = list[0].malHizmetBedeli;
                                this.ltr_durum.Text = string.Concat(textArray4);
                                string[] textArray5 = new string[] { "Bu mutabakat ile ilgili bir sorun olduğunu d\x00fcş\x00fcn\x00fcyorsanız l\x00fctfen <a href=\"mailto:", gonderen, "\">", gonderen, "</a> mail adresi ile iletişime ge\x00e7iniz." };
                                this.ltr_uyari.Text = string.Concat(textArray5);
                                break;
                            }
                        case 2:
                            {
                                string[] textArray6 = new string[12];
                                textArray6[0] = "<strong>";
                                textArray6[1] = list[0].vkno.ToString();
                                textArray6[2] = " - ";
                                textArray6[3] = list[0].unvan.ToString();
                                textArray6[4] = "</strong>\x00a0\x00fcnvanlı firmanız i\x00e7in\x00a0 <br /> <strong>";
                                textArray6[5] = list[0].donem.ToString();
                                textArray6[6] = "</strong> d\x00f6nemine ait\x00a0<strong>Form ";
                                textArray6[7] = str5;
                                textArray6[8] = "</strong>\x00a0mutabakat bilgilerini daha \x00f6nce <strong>\"MUTABIK DEĞİLİZ\"</strong> olarak yanıtladınız. <br /> Belge Adedi : ";
                                textArray6[9] = list[0].belgeSayisi.ToString();
                                textArray6[10] = " <br /> Fatura Tutarı : ";
                                textArray6[11] = list[0].malHizmetBedeli;
                                this.ltr_durum.Text = string.Concat(textArray6);
                                string[] textArray7 = new string[] { "Bu mutabakat ile ilgili bir sorun olduğunu d\x00fcş\x00fcn\x00fcyorsanız l\x00fctfen <a href=\"mailto:", gonderen, "\">", gonderen, "</a> mail adresi ile iletişime ge\x00e7iniz." };
                                this.ltr_uyari.Text = string.Concat(textArray7);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            MutabakatBL tbl = new MutabakatBL();
            if ( tbl.UpdateStatus(this.mutabakatIDForFile, this.returnMail, this.status) <= 0)
            {
                base.Response.Redirect("404.aspx");
            }
            if (this.fileUpload.HasFile)
            {
                string description;
                description=this.txt_description.Text.ToString();

                FileInfo fi = new FileInfo(fileUpload.FileName);
                byte[] documentContent = fileUpload.FileBytes;
                string name = fi.Name;
                string extn = fi.Extension;
                int id=151;
                if (this.mutabakatIDForFile > 0)
                {
                    id = this.mutabakatIDForFile;
                }

                tbl.updateRejectedMutabakat(name, extn, documentContent, id, description);
       

            }
            this.saveFile.Text = "Gönderildi";
            this.saveFile.Attributes.Add("style", "background:url('../img/btn_mailok.png') no-repeat;");

        }
        protected void btnGet_Click(object sender, EventArgs e)
        {
            MutabakatBL tbl = new MutabakatBL();
  
            int id;
            if (this.mutabakatIDForFile > 0)
            {
                id = this.mutabakatIDForFile;
            }
            id = 151;
            DataTable dt = new DataTable();
            dt.Load(tbl.getDocument(id));
            int lastItem = dt.Rows.Count;
            string name = dt.Rows[lastItem-1]["Name"].ToString();
            byte[] docByte = (byte[])dt.Rows[lastItem-1]["DocumentContent"];
            Response.ContentType = "application/octetstream";
            Response.AddHeader("Content-Disposition",string.Format("attachment; filename={0}", name));
            Response.AddHeader("Content-Length", docByte.Length.ToString());
            Response.BinaryWrite(docByte);
            Response.Flush();
            Response.Close();


        }
        
    }
}

