namespace BL
{
    using DAL;
    using Entity;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web;

    public class MutabakatBL
    {
        private DAL dl = new DAL();

        public void addNew(string unvan, string vkno, string donem, int belgeSayisi, string malHizmetBedeli, string mailler, int durum, string mutabakatID, string adminMail, int mutabakatTipi)
        {
            this.dl.InputParameterAdd("@unvan", unvan);
            this.dl.InputParameterAdd("@vkno", vkno);
            this.dl.InputParameterAdd("@donem", donem);
            this.dl.InputParameterAdd("@belgeSayisi", belgeSayisi);
            this.dl.InputParameterAdd("@gonderilmeTarihi", DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.UniversalSortableDateTimePattern));
            this.dl.InputParameterAdd("@malHizmetBedeli", malHizmetBedeli);
            this.dl.InputParameterAdd("@mailler", mailler);
            this.dl.InputParameterAdd("@durum", 0);
            this.dl.InputParameterAdd("@mutabakatID", mutabakatID);
            this.dl.InputParameterAdd("@gonderen", adminMail);
            this.dl.InputParameterAdd("@mutabakatTipi", mutabakatTipi);
            this.dl.Execute("INSERT INTO tblMutabakatlar(unvan, vkno, donem, belgeSayisi, gonderilmeTarihi, malHizmetBedeli, mailler, durum, mutabakatID, gonderen, mutabakatTipi) Values(@unvan, @vkno, @donem, @belgeSayisi, @gonderilmeTarihi, @malHizmetBedeli, @mailler, @durum, @mutabakatID, @gonderen, @mutabakatTipi)");
        }

        public void delete(int Id)
        {
            this.dl.InputParameterAdd("@ID", Id);
            this.dl.Execute("Delete From tblMutabakatlar Where ID = @ID");
        }

        public List<Mutabakat> doneminTumMutabakatlari(string donem, int mutabakatTipi)
        {
            this.dl.InputParameterAdd("@donem", donem);
            this.dl.InputParameterAdd("@mutabakatTipi", mutabakatTipi);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblMutabakatlar WHERE donem = @donem AND mutabakatTipi = @mutabakatTipi");
            List<Mutabakat> list = new List<Mutabakat>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    Mutabakat mutabakat2 = new Mutabakat {
                        unvan = reader["unvan"].ToString(),
                        vkno = reader["vkno"].ToString(),
                        belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                        malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                        donem = reader["donem"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString())
                    };
                    mutabakat2.gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    mutabakat2.yanitlayanMail = reader["yanitlayanMail"].ToString();
                    mutabakat2.mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString());
                    Mutabakat item = mutabakat2;
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public List<Mutabakat> filter(string query, List<filterParameters> parameters)
        {
            int num = 0;
            while (true)
            {
                bool flag = num < parameters.Count;
                if (!flag)
                {
                    MySqlDataReader reader = this.dl.RdrData(query);
                    List<Mutabakat> list = new List<Mutabakat>();
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
                            Mutabakat item = new Mutabakat {
                                ID = int.Parse(reader["ID"].ToString()),
                                unvan = reader["unvan"].ToString(),
                                vkno = reader["vkno"].ToString(),
                                belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                                malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                                donem = reader["donem"].ToString(),
                                email = reader["mailler"].ToString(),
                                durum = short.Parse(reader["durum"].ToString()),
                                gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                                yanitlanmaTarihi = str,
                                yanitlayanMail = reader["yanitlayanMail"].ToString(),
                                mutabakatID = reader["mutabakatID"].ToString(),
                                mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString())
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
  
        public List<Mutabakat> mutabakat(string mutabakatID)
        {
            this.dl.InputParameterAdd("@mutabakatID", mutabakatID);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblMutabakatlar Where mutabakatID = @mutabakatID");
            List<Mutabakat> list = new List<Mutabakat>();
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
                    Mutabakat item = new Mutabakat {
                        ID = int.Parse(reader["ID"].ToString()),
                        unvan = reader["unvan"].ToString(),
                        vkno = reader["vkno"].ToString(),
                        belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                        malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                        donem = reader["donem"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        gonderen = reader["gonderen"].ToString(),
                        mutabakatID = reader["mutabakatID"].ToString(),
                        mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString())
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public List<Mutabakat> mutabakatWithID(int mutabakatID)
        {
            this.dl.InputParameterAdd("@ID", mutabakatID);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblMutabakatlar Where ID = @ID");
            List<Mutabakat> list = new List<Mutabakat>();
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
                    Mutabakat item = new Mutabakat
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        unvan = reader["unvan"].ToString(),
                        vkno = reader["vkno"].ToString(),
                        belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                        malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                        donem = reader["donem"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        gonderen = reader["gonderen"].ToString(),
                        mutabakatID = reader["mutabakatID"].ToString(),
                        mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString())
                    };
                    list.Add(item);
                    break;
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }


        public List<Mutabakat> mutabakatlar()
        {
            MySqlDataReader reader = this.dl.RdrData("Select * From tblMutabakatlar");
            List<Mutabakat> list = new List<Mutabakat>();
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
                    Mutabakat item = new Mutabakat {
                        ID = int.Parse(reader["ID"].ToString()),
                        unvan = reader["unvan"].ToString(),
                        vkno = reader["vkno"].ToString(),
                        belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                        malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                        donem = reader["donem"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString())
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        

        public List<Mutabakat> mutabakatlar(int tip)
        {
            this.dl.InputParameterAdd("tip", tip);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblMutabakatlar Where mutabakatTipi = @tip");
            List<Mutabakat> list = new List<Mutabakat>();
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
                    Mutabakat item = new Mutabakat {
                        ID = int.Parse(reader["ID"].ToString()),
                        unvan = reader["unvan"].ToString(),
                        vkno = reader["vkno"].ToString(),
                        belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                        malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                        donem = reader["donem"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString())
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
           
            return this.dl.Execute("Update tblMutabakatlar Set yanitlayanMail = @yanitlayanMail, yanitlanmaTarihi = @yanitlanmaTarihi, durum = @durum, IP = @IP Where ID = @ID");
        }
        public int updateMutabakatToSendAgain(int ID, int belgeSayisi, string malHizmetBedeli, string mailler, int durum, string adminMail)
        {
            this.dl.InputParameterAdd("@gonderen", adminMail);
            this.dl.InputParameterAdd("@belgeSayisi", belgeSayisi);
            this.dl.InputParameterAdd("@gonderilmeTarihi", DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.UniversalSortableDateTimePattern));
            this.dl.InputParameterAdd("@malHizmetBedeli", malHizmetBedeli);
            this.dl.InputParameterAdd("@mailler", mailler);
            this.dl.InputParameterAdd("@durum", durum);   
                 
            this.dl.InputParameterAdd("@ID", ID);

            return this.dl.Execute("Update tblMutabakatlar Set   gonderen = @gonderen, belgeSayisi = @belgeSayisi, gonderilmeTarihi = @gonderilmeTarihi, malHizmetBedeli = @malHizmetBedeli, mailler = @mailler , durum = @durum Where ID = @ID");
        }

        public void updateRejectedMutabakat(string Name, string Extn, byte[] DocumentContent,int ID,string description)
        {
            this.dl.saveDocument(Name, Extn, DocumentContent, ID, description);
        }
        public MySqlDataReader getDocument(int ID)
        {
            this.dl.InputParameterAdd("@MutabakatID", ID);
            return this.dl.RdrData("Select * From documents where MutabakatID = @MutabakatID");

        }
        public List<MutabakatDoc>  getDocumentContext(int ID)
        {
            List<MutabakatDoc> docList = new List<MutabakatDoc>();
      
          
            this.dl.InputParameterAdd("@MutabakatID", ID);
            MySqlDataReader reader=this.dl.RdrData("Select Description,Name,Extn From documents where MutabakatID = @MutabakatID");
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    MutabakatDoc mutabakatDoc = new MutabakatDoc();
                    mutabakatDoc.Description= reader["Description"].ToString();
                    mutabakatDoc.Name = reader["Name"].ToString();
                    mutabakatDoc.Extn= reader["Extn"].ToString();
                  
                    docList.Add(mutabakatDoc);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((docList == null) ? null : docList);
        }       
        public List<Mutabakat> getMutabakatDetail(int ID)
        {
            this.dl.InputParameterAdd("@ID", ID);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblMutabakatlar Where ID = @ID");
            List<Mutabakat> list = new List<Mutabakat>();
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
                    Mutabakat item = new Mutabakat
                    {
                        ID = int.Parse(reader["ID"].ToString()),
                        unvan = reader["unvan"].ToString(),
                        vkno = reader["vkno"].ToString(),
                        belgeSayisi = short.Parse(reader["belgeSayisi"].ToString()),
                        malHizmetBedeli = reader["malHizmetBedeli"].ToString(),
                        donem = reader["donem"].ToString(),
                        email = reader["mailler"].ToString(),
                        durum = short.Parse(reader["durum"].ToString()),
                        gonderilmeTarihi = Convert.ToDateTime(reader["gonderilmeTarihi"].ToString()).ToString("dd.MM.yyyy HH:mm:ss"),
                        yanitlanmaTarihi = str,
                        yanitlayanMail = reader["yanitlayanMail"].ToString(),
                        mutabakatTipi = short.Parse(reader["mutabakatTipi"].ToString())
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }


    }
}

