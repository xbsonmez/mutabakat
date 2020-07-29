namespace BL
{
    using DAL;
    using Entity;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class CompanyBL
    {
        private DAL dl = new DAL();

        public void addNewCompany(string unvan, string vkno, string mailler)
        {
            this.dl.InputParameterAdd("@unvan", unvan);
            this.dl.InputParameterAdd("@vkno", vkno);
            this.dl.InputParameterAdd("@mailler", mailler);
            this.dl.InputParameterAdd("@eklenmeTarihi", DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.UniversalSortableDateTimePattern));
            this.dl.Execute("INSERT INTO tblFirmalar(unvan, vkno, mailler, eklenmeTarihi) Values (@unvan, @vkno, @mailler, @eklenmeTarihi)");
        }

        public List<Company> allCompanyEmails(string unvan, string vkno)
        {
            this.dl.InputParameterAdd("@unvan", unvan);
            this.dl.InputParameterAdd("@vkno", vkno);
            MySqlDataReader reader = this.dl.RdrData("Select mailler From tblFirmalar WHERE unvan=@unvan AND vkno = @vkno");
            List<Company> list = new List<Company>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    Company item = new Company {
                        email = reader["mailler"].ToString()
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public List<Company> allCompanys()
        {
            MySqlDataReader reader = this.dl.RdrData("Select * From tblFirmalar");
            List<Company> list = new List<Company>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    Company item = new Company {
                        ID = int.Parse(reader["ID"].ToString()),
                        vkno = reader["vkno"].ToString(),
                        unvan = reader["unvan"].ToString(),
                        email = reader["mailler"].ToString()
                    };
                    list.Add(item);
                }
            }
            reader.Close();
            reader.Dispose();
            return ((list.Count <= 0) ? null : list);
        }

        public void delete(int Id)
        {
            this.dl.InputParameterAdd("@ID", Id);
            this.dl.Execute("Delete From tblFirmalar Where ID =@ID");
        }

        public void edit(int ID, string mailler)
        {
            this.dl.InputParameterAdd("@mailler", mailler);
            this.dl.InputParameterAdd("@ID", ID);
            this.dl.Execute("Update tblFirmalar Set mailler = @mailler Where ID = @ID");
        }
    }
}

