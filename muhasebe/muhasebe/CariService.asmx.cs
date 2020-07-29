using BL;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.Services;

namespace muhasebe
{
    /// <summary>
    /// CariService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class CariService : System.Web.Services.WebService
    {
        private eMailBL eMailProcc = new eMailBL();
        private CompanyBL companyBL = new CompanyBL();
        private CariMutabakatBL mutBL = new CariMutabakatBL();

        [WebMethod(EnableSession = true)]
        public bool addMail(string ID, string Mail)
        {
            bool flag = false;
            if (base.Session["Caris"] != null)
            {
                int num = int.Parse(ID);
                List<Cari> list = (List<Cari>)base.Session["Caris"];
                string email = list[num].email;
                email = (email.Length <= 0) ? Mail : (email + ";" + Mail);
                list[num].email = email;
                base.Session["Caris"] = list;
                flag = true;
            }
            return flag;
        }

        [WebMethod(EnableSession = true)]
        public bool deleteMail(string ID, string Mail)
        {
            bool flag = false;
            if (base.Session["Caris"] != null)
            {
                int num = int.Parse(ID);
                List<Cari> list = (List<Cari>)base.Session["Caris"];
                string email = list[num].email;
                if (email.Length <= Mail.Length)
                {
                    list[num].email = "";
                }
                else
                {
                    string str2 = email.Replace(Mail, "");
                    if (str2.Substring(0, 1) == ";")
                    {
                        str2 = str2.Substring(1, str2.Length - 1);
                    }
                    if (str2.Substring(str2.Length - 1, 1) == ";")
                    {
                        str2 = str2.Substring(0, str2.Length - 1);
                    }
                    str2 = str2.Replace(";;", ";");
                    list[num].email = str2;
                }
                base.Session["Caris"] = list;
                flag = true;
            }
            return flag;
        }

        [WebMethod(EnableSession = true)]
        public Cari getDetails(string id)
        {
            Cari cari = new Cari();
            if (base.Session["Caris"] != null)
            {
                int num = int.Parse(id);
                List<Cari> list = (List<Cari>)base.Session["Caris"];
                cari.ID = list[num].ID;
                cari.vkno = list[num].vkno;
                cari.unvan = list[num].unvan;
                cari.hesapKodu = list[num].hesapKodu;
                cari.muhasebeKodu = list[num].muhasebeKodu;
                cari.alacak = list[num].alacak;
                cari.alacakBakiye = list[num].alacakBakiye;
                cari.borc = list[num].borc;
                cari.borcBakiye = list[num].borcBakiye;
                cari.mailGonderim = list[num].mailGonderim;
                cari.tarih = list[num].tarih;
                cari.email = list[num].email;
            }
            return cari;
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

        [WebMethod(EnableSession = true)]
        public void sendMail(string ID)
        {
            if (base.Session["Caris"] != null)
            {
                int num = int.Parse(ID);
                List<Cari> list = (List<Cari>)base.Session["Caris"];
                string unvan = list[num].unvan;
                string hesapKodu = list[num].hesapKodu;
                string muhasebeKodu = list[num].muhasebeKodu;
                string email = list[num].email;
                string alacak = list[num].alacak.ToString().Replace("TL", "₺"); 
                string alacakBakiye = list[num].alacakBakiye.ToString().Replace("TL", "₺");
                string borc = list[num].borc.ToString().Replace("TL", "₺"); 
                string borcBakiye = list[num].borcBakiye.ToString().Replace("TL", "₺");
                string tarih = list[num].tarih;
                string vkno = list[num].vkno;
                char[] separator = new char[] { ';' };
                string[] strArray = email.Split(separator);
                bool flag2 = false;
                int num2 = 0;
                string newValue = Guid.NewGuid().ToString().Replace("-", "");
                List<Admin> list2 = (List<Admin>)base.Session["Admin"];
                string mail = list2[0].mail;
                string mailPass = list2[0].mailPass;
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
                        if (this.eMailProcc.send(strArray[index], subject, str15.Replace("[MAIL]", strArray[index]), mail, mail, mailPass))
                        {
                            num2++;
                        }
                        index++;
                    }
                }
                if (num2 > 0)
                {
                    list[num].mailGonderim = true;
                    base.Session["Caris"] = list;
                    List<Company> list3 = this.companyBL.allCompanyEmails(unvan, vkno);
                    if ((list3 == null) || (list3.Count == 0))
                    {
                        this.companyBL.addNewCompany(unvan, vkno, email);
                    }
                    this.mutBL.addNew(tarih, unvan, muhasebeKodu, hesapKodu, borc, alacak, borcBakiye, alacakBakiye, mail, newValue, email, vkno);
                }
            }
        }
    }
}

