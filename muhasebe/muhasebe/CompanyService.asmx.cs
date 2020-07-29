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
    /// CompanyService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class CompanyService : System.Web.Services.WebService
    {
        private eMailBL eMailProcc = new eMailBL();
        private CompanyBL companyBL = new CompanyBL();
        private MutabakatBL mutBL = new MutabakatBL();

        [WebMethod(EnableSession = true)]
        public bool addMail(string ID, string Mail)
        {
            bool flag = false;
            if (base.Session["Companys"] != null)
            {
                int num = int.Parse(ID);
                List<Company> list = (List<Company>)base.Session["Companys"];
                string email = list[num].email;
                email = (email.Length <= 0) ? Mail : (email + ";" + Mail);
                list[num].email = email;
                base.Session["Companys"] = list;
                flag = true;
            }
            return flag;
        }

        [WebMethod(EnableSession = true)]
        public bool deleteMail(string ID, string Mail)
        {
            bool flag = false;
            if (base.Session["Companys"] != null)
            {
                int num = int.Parse(ID);
                List<Company> list = (List<Company>)base.Session["Companys"];
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
                base.Session["Companys"] = list;
                flag = true;
            }
            return flag;
        }

        [WebMethod(EnableSession = true)]
        public Company getDetails(string id)
        {
            Company company = new Company();
            if (base.Session["Companys"] != null)
            {
                int num = int.Parse(id);
                List<Company> list = (List<Company>)base.Session["Companys"];
                company.ID = list[num].ID;
                company.unvan = list[num].unvan;
                company.vkno = list[num].vkno;
                company.belgeSayisi = list[num].belgeSayisi;
                company.malHizmetBedeli = list[num].malHizmetBedeli;
                company.email = list[num].email;
                company.mailGonderim = list[num].mailGonderim;
                company.mutabakatTipi = list[num].mutabakatTipi;
            }
            return company;
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
        public void sendAllMail(string ID)
        {

        }
        [WebMethod(EnableSession = true)]
        public void sendMail(string ID)
        {
            if (base.Session["Companys"] != null)
            {
                int num = int.Parse(ID);
                List<Company> list = (List<Company>)base.Session["Companys"];
                string unvan = list[num].unvan;
                string vkno = list[num].vkno;
                string email = list[num].email;
                string newValue = list[num].belgeSayisi.ToString();
                string malHizmetBedeli = list[num].malHizmetBedeli.ToString().Replace("TL", "₺");
                int donemAy = list[num].donemAy;
                int donemYil = list[num].donemYil;
                int mutabakatTipi = list[num].mutabakatTipi;
                string str6 = base.Session["donem"].ToString();
                char[] separator = new char[] { ';' };
                string[] strArray = email.Split(separator);
                int num5 = 0;
                string str7 = Guid.NewGuid().ToString().Replace("-", "");
                List<Admin> list2 = (List<Admin>)base.Session["Admin"];
                string mail = list2[0].mail;
                string mailPass = list2[0].mailPass;
                string str10 = "imza_" + list2[0].ID.ToString();
                if (strArray.Length != 0)
                {
                    string str11 = Convert.ToString(ReadHtmlFile(base.Server.MapPath("~/mail_files/MAIL_FORMAT.html"))).Replace("[VKNO]", vkno).Replace("[UNVAN]", unvan).Replace("[DONEM]", str6).Replace("[BELGESAYISI]", newValue).Replace("[MALHIZMETBEDELI]", malHizmetBedeli).Replace("[MUTABAKATID]", str7).Replace("[IMZA]", str10);
                    str11 = (mutabakatTipi != 1) ? str11.Replace("[TIP]", "BA") : str11.Replace("[TIP]", "BS");
                    string str12 = str6.Replace(donemYil.ToString() + " - ", "");
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
                        if (this.eMailProcc.send(strArray[index], subject, str11.Replace("[MAIL]", strArray[index]), mail, mail, mailPass))
                        {
                            num5++;
                        }
                        index++;
                    }
                }
                if (num5 > 0)
                {
                    list[num].mailGonderim = true;
                    base.Session["Companys"] = list;
                    List<Company> list3 = this.companyBL.allCompanyEmails(unvan, vkno);
                    if ((list3 == null) || (list3.Count == 0))
                    {
                        this.companyBL.addNewCompany(unvan, vkno, email);
                    }
                    this.mutBL.addNew(unvan, vkno, str6, int.Parse(newValue), malHizmetBedeli, email, 0, str7, mail, mutabakatTipi);
                }
            }
        }
    }
}
