namespace BL
{
    using DAL;
    using Entity;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web;

    public class CariMutabakatBL
    {
        private DAL dl = new DAL();

        public void addNew(string tarih, string unvan, string muhasebeKodu, string hesapKodu, string borc, string alacak, string borcBakiyesi, string alacakBakiyesi, string adminMail, string mutabakatID, string mailler, string vkno)
        {
            this.dl.InputParameterAdd("@tarih", tarih);
            this.dl.InputParameterAdd("@unvan", unvan);
            this.dl.InputParameterAdd("@muhasebeKodu", muhasebeKodu);
            this.dl.InputParameterAdd("@hesapKodu", hesapKodu);
            this.dl.InputParameterAdd("@borc", borc);
            this.dl.InputParameterAdd("@alacak", alacak);
            this.dl.InputParameterAdd("@borcBakiyesi", borcBakiyesi);
            this.dl.InputParameterAdd("@alacakBakiyesi", alacakBakiyesi);
            this.dl.InputParameterAdd("@gonderilmeTarihi", DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.UniversalSortableDateTimePattern));
            this.dl.InputParameterAdd("@gonderen", adminMail);
            this.dl.InputParameterAdd("@durum", 0);
            this.dl.InputParameterAdd("@mutabakatID", mutabakatID);
            this.dl.InputParameterAdd("@mailler", mailler);
            this.dl.InputParameterAdd("@vkno", vkno);
            this.dl.Execute("INSERT INTO tblCariMutabakatlar(tarih, unvan, muhasebeKodu, hesapKodu, borc, alacak, borcBakiyesi, alacakBakiyesi, gonderilmeTarihi, gonderen, durum, mutabakatID, mailler, vkno) Values(@tarih, @unvan, @muhasebeKodu, @hesapKodu, @borc, @alacak, @borcBakiyesi, @alacakBakiyesi, @gonderilmeTarihi, @gonderen, @durum, @mutabakatID, @mailler, @vkno)");
        }

        public void delete(int Id)
        {
            this.dl.InputParameterAdd("@ID", Id);
            this.dl.Execute("Delete From tblCariMutabakatlar Where ID = @ID");
        }

        public List<CariMutabakat> filter(string query, List<filterParameters> parameters)
        {
            int num = 0;
            while (true)
            {
                bool flag = num < parameters.Count;
                if (!flag)
                {
                    MySqlDataReader reader = this.dl.RdrData(query);
                    List<CariMutabakat> list = new List<CariMutabakat>();
                    if (reader.HasRows)
                    {
                        while (true)
                        {
                            flag = reader.Read();
                            if (!flag)
                            {
                                break;
                            }
                            string str = "";
                            try
                            {
                                str = Convert.ToDateTime(reader["yanitlanmaTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss");
                            }
                            catch
                            {
                                str = "";
                            }
                            CariMutabakat item = new CariMutabakat {
                                ID = int.Parse(reader["ID"].ToString()),
                                vkno = reader["vkno"].ToString(),
                                unvan = reader["unvan"].ToString(),
                                muhasebeKodu = reader["muhasebeKodu"].ToString(),
                                hesapKodu = reader["hesapKodu"].ToString(),
                                borc = reader["borc"].ToString(),
                                alacak = reader["alacak"].ToString(),
                                borcBakiye = reader["borcBakiyesi"].ToString(),
                                alacakBakiye = reader["alacakBakiyesi"].ToString(),
                                email = reader["mailler"].ToString(),
                                durum = short.Parse(reader["durum"].ToString()),
                                yanitlanmaTarihi = str,
                                yanitlayanMail = reader["yanitlayanMail"].ToString(),
                                tarih = reader["tarih"].ToString(),
                                gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                                gonderen = reader["gonderen"].ToString()
                            };
                            list.Add(item);
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    return ((list.Count <= 0) ? null : list);
                }
                filterParameters parameters2 = parameters[num];
                this.dl.InputParameterAdd(parameters2.name, parameters2.value);
                num++;
            }
        }

        public List<CariMutabakat> mutabakat(string mutabakatID)
        {
            this.dl.InputParameterAdd("@mutabakatID", mutabakatID);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblCariMutabakatlar Where mutabakatID = @mutabakatID");
            List<CariMutabakat> list = new List<CariMutabakat>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    string str = "";
                    try
                    {
                        str = Convert.ToDateTime(reader["yanitlanmaTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss");
                    }
                    catch
                    {
                        str = "";
                    }
                    CariMutabakat item = new CariMutabakat {
                        ID = int.Parse(reader["ID"].ToString()),
                        vkno = reader["vkno"].ToString(),
                        unvan = reader["unvan"].ToString(),
                        muhasebeKodu = reader["muhasebeKodu"].ToString(),
                        hesapKodu = reader["hesapKodu"].ToString(),
                        borc = reader["borc"].ToString(),
                        alacak = reader["alacak"].ToString(),
                        borcBakiye = reader["borcBakiyesi"].ToString(),
                        alacakBakiye = reader["alacakBakiyesi"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        tarih = reader["tarih"].ToString(),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        gonderen = reader["gonderen"].ToString()
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public List<CariMutabakat> mutabakatlar()
        {
            MySqlDataReader reader = this.dl.RdrData("Select * From tblCariMutabakatlar LIMIT 200");
            List<CariMutabakat> list = new List<CariMutabakat>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    string str = "";
                    try
                    {
                        str = Convert.ToDateTime(reader["yanitlanmaTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss");
                    }
                    catch
                    {
                        str = "";
                    }
                    CariMutabakat item = new CariMutabakat {
                        ID = int.Parse(reader["ID"].ToString()),
                        vkno = reader["vkno"].ToString(),
                        unvan = reader["unvan"].ToString(),
                        muhasebeKodu = reader["muhasebeKodu"].ToString(),
                        hesapKodu = reader["hesapKodu"].ToString(),
                        borc = reader["borc"].ToString(),
                        alacak = reader["alacak"].ToString(),
                        borcBakiye = reader["borcBakiyesi"].ToString(),
                        alacakBakiye = reader["alacakBakiyesi"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        tarih = reader["tarih"].ToString(),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss")
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public List<CariMutabakat> mutabakatlar(string date)
        {
            this.dl.InputParameterAdd("date", date);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblCariMutabakatlar Where tarih = @date");
            List<CariMutabakat> list = new List<CariMutabakat>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    string str = "";
                    try
                    {
                        str = Convert.ToDateTime(reader["yanitlanmaTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss");
                    }
                    catch
                    {
                        str = "";
                    }
                    CariMutabakat item = new CariMutabakat {
                        ID = int.Parse(reader["ID"].ToString()),
                        vkno = reader["vkno"].ToString(),
                        unvan = reader["unvan"].ToString(),
                        muhasebeKodu = reader["muhasebeKodu"].ToString(),
                        hesapKodu = reader["hesapKodu"].ToString(),
                        borc = reader["borc"].ToString(),
                        alacak = reader["alacak"].ToString(),
                        borcBakiye = reader["borcBakiyesi"].ToString(),
                        alacakBakiye = reader["alacakBakiyesi"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString()
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public int UpdateStatus(int ID, string returnMail, int status)
        {
            this.dl.InputParameterAdd("@yanitlayanMail", returnMail);
            this.dl.InputParameterAdd("@yanitlanmaTarihi", DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.UniversalSortableDateTimePattern));
            this.dl.InputParameterAdd("@durum", status);
            this.dl.InputParameterAdd("@IP", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
            this.dl.InputParameterAdd("@ID", ID);
            return this.dl.Execute("Update tblCariMutabakatlar Set yanitlayanMail = @yanitlayanMail, yanitlanmaTarihi = @yanitlanmaTarihi, durum = @durum, IP = @IP Where ID = @ID");
        }
        public void updateRejectedMutabakat(string Name, string Extn, byte[] DocumentContent, int ID, string description)
        {
            this.dl.saveCariDocument(Name, Extn, DocumentContent, ID, description);
        }
        public MySqlDataReader getDocument(int ID)
        {
            this.dl.InputParameterAdd("@cariID", ID);
            return this.dl.RdrData("Select * From caridocuments where cariID= @cariID");

        }
      
        public List<CariMutabakatDoc> getDocumentContext(int ID)
        {
            List<CariMutabakatDoc> docList = new List<CariMutabakatDoc>();


            this.dl.InputParameterAdd("@cariID", ID);
            MySqlDataReader reader = this.dl.RdrData("Select Description,Name,Extn From caridocuments where cariID = @cariID");
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    CariMutabakatDoc cariMutabakatDoc = new CariMutabakatDoc();
                    cariMutabakatDoc.Description = reader["Description"].ToString();
                    cariMutabakatDoc.Name = reader["Name"].ToString();
                    cariMutabakatDoc.Extn = reader["Extn"].ToString();

                    docList.Add(cariMutabakatDoc);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((docList == null) ? null : docList);
        }
        public List<CariMutabakat> getCariMutabakatDetail(int ID)
        {
            this.dl.InputParameterAdd("@ID", ID);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblcarimutabakatlar Where ID = @ID");

            List<CariMutabakat> list = new List<CariMutabakat>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    string str = "";
                    try
                    {
                        str = Convert.ToDateTime(reader["yanitlanmaTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss");
                    }
                    catch
                    {
                        str = "";
                    }
                    CariMutabakat item = new CariMutabakat
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        vkno = reader["vkno"].ToString(),
                        unvan = reader["unvan"].ToString(),
                        muhasebeKodu = reader["muhasebeKodu"].ToString(),
                        hesapKodu = reader["hesapKodu"].ToString(),
                        borc = reader["borc"].ToString(),
                        alacak = reader["alacak"].ToString(),
                        borcBakiye = reader["borcBakiyesi"].ToString(),
                        alacakBakiye = reader["alacakBakiyesi"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        tarih = reader["tarih"].ToString(),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        gonderen = reader["gonderen"].ToString()
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }
        public int updateMutabakatToSendAgain(int ID, string borc, string borcBakiyesi, string alacakBakiyesi,string alacak, string mailler, int durum, string adminMail)
        {
           
        
         
            this.dl.InputParameterAdd("@borc", borc);
            this.dl.InputParameterAdd("@alacak", alacak);
            this.dl.InputParameterAdd("@borcBakiyesi", borcBakiyesi);
            this.dl.InputParameterAdd("@alacakBakiyesi", alacakBakiyesi);
            this.dl.InputParameterAdd("@gonderilmeTarihi", DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.UniversalSortableDateTimePattern));
            this.dl.InputParameterAdd("@gonderen", adminMail);
            this.dl.InputParameterAdd("@durum", durum);
            this.dl.InputParameterAdd("@mailler", mailler);

            this.dl.InputParameterAdd("@ID", ID);

            return this.dl.Execute("Update tblcarimutabakatlar Set   gonderen = @gonderen, alacak = @alacak, alacakBakiyesi = @alacakBakiyesi, borcBakiyesi = @borcBakiyesi, borc = @borc, gonderilmeTarihi = @gonderilmeTarihi, mailler = @mailler , durum = @durum Where ID = @ID");
        }
    }
}

