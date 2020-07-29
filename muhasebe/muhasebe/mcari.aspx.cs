using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace muhasebe
{

    public partial class mcari : System.Web.UI.Page
    {
        int status;
        string returnMail;
        int mutabakatIDForFile;

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
                if (status == 2)
                {
                    str3 = "MUTABIK DEĞİLİZ";
                }
            
                this.ltr_ip.Text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() + "IP adresi ile sistemde işlem yapmaktasınız";
                CariMutabakatBL tbl = new CariMutabakatBL();
                List<CariMutabakat> list = tbl.mutabakat(mutabakatID);
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
                    string alacakBakiye = "";
                    string str6 = "";
                    if (this.status == 2 && num3 == 0)
                    {
                        this.saveFile.Visible = true;
                        this.fileUpload.Visible = true;
                        this.txt_description.Visible = true;
                        this.why_txt.Visible = true;
                        this.maxTextLimit.Visible = true;

                    }
                    if ((list[0].alacakBakiye != "") && (list[0].alacakBakiye != "0 TL"))
                    {
                        alacakBakiye = list[0].alacakBakiye;
                        str6 = "ALACAKLI";
                    }
                    else
                    {
                        alacakBakiye = list[0].borcBakiye;
                        str6 = "BORCLU";
                    }
                    switch (num3)
                    {
                        case 0:
                            if (this.status == 1 && tbl.UpdateStatus(this.mutabakatIDForFile, this.returnMail, this.status) <= 0)
                            {
                                base.Response.Redirect("404.aspx");
                            }
                            else
                            {
                                if ((list[0].alacakBakiye == "0 TL") && (list[0].borcBakiye == "0 TL"))
                                {
                                    string[] textArray1 = new string[11];
                                    textArray1[0] = "<center><strong>";
                                    textArray1[1] = list[0].vkno.ToString();
                                    textArray1[2] = " - ";
                                    textArray1[3] = list[0].unvan.ToString();
                                    textArray1[4] = "</strong><br /><br />\x00a0Şirketimizdeki Cari Hesabınızı\x00a0<br /><br /><strong>";
                                    textArray1[5] = list[0].tarih.ToString();
                                    textArray1[6] = "</strong> tarihi itibari ile <strong>";
                                    textArray1[7] = alacakBakiye;
                                    textArray1[8] = " </strong> olduğunu <br /><br /><strong>\"";
                                    textArray1[9] = str3;
                                    textArray1[10] = "\"</strong> olarak onayladınız. </center>";
                                    this.ltr_durum.Text = string.Concat(textArray1);
                                }
                                else
                                {
                                    string[] textArray2 = new string[13];
                                    textArray2[0] = "<center><strong>";
                                    textArray2[1] = list[0].vkno.ToString();
                                    textArray2[2] = " - ";
                                    textArray2[3] = list[0].unvan.ToString();
                                    textArray2[4] = "</strong><br /><br />\x00a0Şirketimizdeki Cari Hesabınızı\x00a0<br /><br /><strong>";
                                    textArray2[5] = list[0].tarih.ToString();
                                    textArray2[6] = "</strong> tarihi itibari ile <strong>";
                                    textArray2[7] = alacakBakiye;
                                    textArray2[8] = " ";
                                    textArray2[9] = str6;
                                    textArray2[10] = "</strong> olduğunuzu <br /><br /><strong>\"";
                                    textArray2[11] = str3;
                                    textArray2[12] = "\"</strong> olarak onayladınız.</center>";
                                    this.ltr_durum.Text = string.Concat(textArray2);
                                }
                                string subject = list[0].tarih.ToUpper() + " TARİHLİ " + list[0].unvan.ToString() + " CARİ MUTABAKAT YANITI";
                                string mailBody = "<div style=\"font-family:arial; font-size:12px;\">";
                                string[] textArray3 = new string[20];
                                textArray3[0] = mailBody;
                                textArray3[1] = "<strong>";
                                textArray3[2] = list[0].gonderilmeTarihi.ToString();
                                textArray3[3] = "</strong> tarihinde <strong><a href=\"mailto:";
                                textArray3[4] = list[0].gonderen;
                                textArray3[5] = "\">";
                                textArray3[6] = list[0].gonderen;
                                textArray3[7] = "</a></strong> tarafından g\x00f6nderilen <br /><br /> <strong>";
                                textArray3[8] = list[0].vkno;
                                textArray3[9] = " - ";
                                textArray3[10] = list[0].unvan;
                                textArray3[11] = "</strong> \x00fcnvanlı, <strong>";
                                textArray3[12] = list[0].tarih;
                                textArray3[13] = "</strong> tarihine ait <strong>Cari </strong> mutabakatı <strong><a href=\"mailto:";
                                textArray3[14] = returnMail;
                                textArray3[15] = "\">";
                                textArray3[0x10] = returnMail;
                                textArray3[0x11] = "</a></strong> tarafından <strong>";
                                textArray3[0x12] = str3;
                                textArray3[0x13] = "</strong> olarak yanıtlanmıştır.";
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
                                if ((list[0].alacakBakiye == "0 TL") && (list[0].borcBakiye == "0 TL"))
                                {
                                    string[] textArray4 = new string[] { "<center><strong>", list[0].vkno.ToString(), " - ", list[0].unvan.ToString(), "</strong><br /><br />\x00a0Şirketimizdeki Cari Hesabınızı\x00a0<br /><br /><strong>", list[0].tarih.ToString(), "</strong> tarihi itibari ile <strong>0 TL</strong> olduğunu daha \x00f6nce <br /><br /><strong>\"MUTABIKIZ\"</strong> olarak yanıtladınız.</center>" };
                                    this.ltr_durum.Text = string.Concat(textArray4);
                                }
                                else
                                {
                                    string[] textArray5 = new string[11];
                                    textArray5[0] = "<center><strong>";
                                    textArray5[1] = list[0].vkno.ToString();
                                    textArray5[2] = " - ";
                                    textArray5[3] = list[0].unvan.ToString();
                                    textArray5[4] = "</strong><br /><br />\x00a0Şirketimizdeki Cari Hesabınızı\x00a0<br /><br /><strong>";
                                    textArray5[5] = list[0].tarih.ToString();
                                    textArray5[6] = "</strong> tarihi itibari ile <strong>";
                                    textArray5[7] = alacakBakiye;
                                    textArray5[8] = " ";
                                    textArray5[9] = str6;
                                    textArray5[10] = "</strong> olduğunuzu daha \x00f6nce <br /><br /><strong>\"MUTABIKIZ\"</strong> olarak yanıtladınız.</center>";
                                    this.ltr_durum.Text = string.Concat(textArray5);
                                }
                                string[] textArray6 = new string[] { "Bu mutabakat ile ilgili bir sorun olduğunu d\x00fcş\x00fcn\x00fcyorsanız l\x00fctfen <a href=\"mailto:", gonderen, "\">", gonderen, "</a> mail adresi ile iletişime ge\x00e7iniz." };
                                this.ltr_uyari.Text = string.Concat(textArray6);
                                break;
                            }
                        case 2:
                            {
                                if ((list[0].alacakBakiye == "0 TL") && (list[0].borcBakiye == "0 TL"))
                                {
                                    string[] textArray7 = new string[] { "<center><strong>", list[0].vkno.ToString(), " - ", list[0].unvan.ToString(), "</strong><br /><br />\x00a0Şirketimizdeki Cari Hesabınızı\x00a0<br /><br /><strong>", list[0].tarih.ToString(), "</strong> tarihi itibari ile <strong>0 TL</strong> olduğunu daha \x00f6nce <br /><br /><strong>\"MUTABIK DEĞİLİZ\"</strong> olarak yanıtladınız.</center>" };
                                    this.ltr_durum.Text = string.Concat(textArray7);
                                }
                                else
                                {
                                    string[] textArray8 = new string[11];
                                    textArray8[0] = "<center><strong>";
                                    textArray8[1] = list[0].vkno.ToString();
                                    textArray8[2] = " - ";
                                    textArray8[3] = list[0].unvan.ToString();
                                    textArray8[4] = "</strong><br /><br />\x00a0Şirketimizdeki Cari Hesabınızı\x00a0<br /><br /><strong>";
                                    textArray8[5] = list[0].tarih.ToString();
                                    textArray8[6] = "</strong> tarihi itibari ile <strong>";
                                    textArray8[7] = alacakBakiye;
                                    textArray8[8] = " ";
                                    textArray8[9] = str6;
                                    textArray8[10] = "</strong> olduğunuzu daha \x00f6nce <br /><br /><strong>\"MUTABIK DEĞİLİZ\"</strong> olarak yanıtladınız.</center>";
                                    this.ltr_durum.Text = string.Concat(textArray8);
                                }
                                string[] textArray9 = new string[] { "Bu mutabakat ile ilgili bir sorun olduğunu d\x00fcş\x00fcn\x00fcyorsanız l\x00fctfen <a href=\"mailto:", gonderen, "\">", gonderen, "</a> mail adresi ile iletişime ge\x00e7iniz." };
                                this.ltr_uyari.Text = string.Concat(textArray9);
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
            this.saveFile.Text = "Gönderilliyor";
            this.saveFile.Attributes.Add("style", "background-color:yellow");

            
            CariMutabakatBL tbl = new CariMutabakatBL();
            if (tbl.UpdateStatus(this.mutabakatIDForFile, this.returnMail, this.status) <= 0)
            {
                base.Response.Redirect("404.aspx");
            }
           
            if (this.fileUpload.HasFile)
            {
                string description;
                description = this.txt_description.Text.ToString();

                FileInfo fi = new FileInfo(fileUpload.FileName);
                byte[] documentContent = fileUpload.FileBytes;
                string name = fi.Name;
                string extn = fi.Extension;
                int id = 151;
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
            CariMutabakatBL tbl = new CariMutabakatBL();
            int id;
            if (this.mutabakatIDForFile > 0)
            {
                id = this.mutabakatIDForFile;
            }
            id = 151;
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
    }
}
