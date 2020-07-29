using DAL;
using Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
namespace BL
{
  

    public class AdminBL
    {
        private DAL.DAL dl = new DAL.DAL();

        public List<Admin> login(string username, string password)
        {
            this.dl.InputParameterAdd("@User", username);
            this.dl.InputParameterAdd("@Pass", password);
            MySqlDataReader reader = this.dl.RdrData("Select * From tblAdmin WHERE User=@User AND Pass = @Pass");
            List<Admin> list = new List<Admin>();
            if (reader.HasRows)
            {
                while (true)
                {
                    if (!reader.Read())
                    {
                        break;
                    }
                    Admin item = new Admin {
                        ID = int.Parse(reader["ID"].ToString()),
                        Username = reader["User"].ToString(),
                        Password = reader["Pass"].ToString(),
                        NameSurname = reader["NameSurname"].ToString(),
                        mail = reader["mail"].ToString(),
                        mailPass = reader["mailPass"].ToString()
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

