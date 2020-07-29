namespace DAL
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Configuration;
    using System.Data;

    internal class Sql
    {
        private MySqlConnection conn;

        private string provider() => 
            ConfigurationManager.AppSettings["sqlConnTxt"];

        public MySqlConnection connection
        {
            get
            {
                MySqlConnection conn;
                MySqlConnection.ClearAllPools();
                if (!ReferenceEquals(this.conn, null))
                {
                    if (this.conn.State == ConnectionState.Closed)
                    {
                        this.conn.Open();
                    }
                    conn = this.conn;
                }
                else
                {
                    this.conn = new MySqlConnection(this.provider());
                    if (this.conn.State == ConnectionState.Closed)
                    {
                        this.conn.Open();
                    }
                    conn = this.conn;
                }
                return conn;
            }
            set
            {
            }
        }
    }
}

