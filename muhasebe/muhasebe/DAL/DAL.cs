namespace DAL
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class DAL
    {
        private List<MySqlParameter> Parameters = new List<MySqlParameter>();

        public void AddtoQueryParameters(MySqlCommand comm)
        {
            comm.Parameters.AddRange(this.Parameters.ToArray());
        }

        public int Execute(string query)
        {
            int num2;
            try
            {
                MySqlCommand comm = this.WriteToDbQuery(query);
                this.AddtoQueryParameters(comm);
                int num = comm.ExecuteNonQuery();
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
                comm.Connection.Dispose();
                comm.Dispose();
                num2 = num;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Parameters.Clear();
            }
            return num2;
        }

        public object ExecuteFirstRowColumn(string query, CommandType QueryType)
        {
            object obj3;
            try
            {
                MySqlCommand comm = this.WriteToDbQuery(query);
                this.AddtoQueryParameters(comm);
                object obj2 = comm.ExecuteScalar();
                if (comm.Connection.State == ConnectionState.Open)
                {
                    comm.Connection.Close();
                }
                comm.Connection.Dispose();
                comm.Dispose();
                obj3 = obj2;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Parameters.Clear();
            }
            return obj3;
        }

        public object GetParameterValue(string pn)
        {
            using (List<MySqlParameter>.Enumerator enumerator = this.Parameters.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    MySqlParameter current = enumerator.Current;
                    if (current.ParameterName == pn)
                    {
                        return current.Value.ToString();
                    }
                }
            }
            return null;
        }

        public void InputParameterAdd(string parameterName, object value)
        {
            MySqlParameter item = new MySqlParameter {
                ParameterName = parameterName,
                Value = value
            };
            this.Parameters.Add(item);
        }

        public void OutParameterAdd(string parameterName, object value)
        {
            MySqlParameter item = new MySqlParameter {
                ParameterName = parameterName,
                Value = value
            };
            this.Parameters.Add(item);
        }

        public MySqlDataReader RdrData(string query)
        {
            MySqlDataReader reader2;
            try
            {
                MySqlCommand comm = this.WriteToDbQuery(query);
                this.AddtoQueryParameters(comm);
                reader2 = comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.Parameters.Clear();
            }
            return reader2;
        }

        public DataTable TblData(string query, CommandType QueryType)
        {
            DataTable table2;
            try
            {
                MySqlDataReader reader = this.RdrData(query);
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();
                reader.Dispose();
                table2 = table;
            }
            catch (Exception)
            {
                throw;
            }
            return table2;
        }

        private MySqlCommand WriteToDbQuery(string query)
        {
            MySqlCommand command = new Sql().connection.CreateCommand();
            command.CommandText = query;
            return command;
        }
        public void saveDocument(string Name,string Extn,byte[] DocumentContent,int MutabakatID,string Description)
        {

            
            try
            {


                MySqlCommand comm = new Sql().connection.CreateCommand();
                comm.CommandTimeout = 60000;
                comm.CommandText = "SaveDocument";
                comm.CommandType = CommandType.StoredProcedure;
                this.InputParameterAdd("@Name", Name);
                this.InputParameterAdd("@Extn", Extn);
                this.InputParameterAdd("@MutabakatID", MutabakatID);
                this.InputParameterAdd("@DocumentContent", DocumentContent);
                this.InputParameterAdd("@Description", Description);
               
                this.AddtoQueryParameters(comm);   
                comm.ExecuteNonQuery();
                comm.Connection.Dispose();
                comm.Dispose();

            }
            catch
            {
                throw;
            }
            finally
            {
                this.Parameters.Clear();
            }

        }
        public void saveCariDocument(string Name, string Extn, byte[] DocumentContent, int cariID, string Description)
        {


            try
            {

                MySqlCommand comm = new Sql().connection.CreateCommand();
                comm.CommandTimeout = 60000;
                comm.CommandText = "SaveCariDocument";
                comm.CommandType = CommandType.StoredProcedure;
                this.InputParameterAdd("@Name", Name);
                this.InputParameterAdd("@Extn", Extn);
                this.InputParameterAdd("@cariID", cariID);
                this.InputParameterAdd("@DocumentContent", DocumentContent);
                this.InputParameterAdd("@Description", Description);

                this.AddtoQueryParameters(comm);
                comm.ExecuteNonQuery();
                comm.Connection.Dispose();
                comm.Dispose();

            }
            catch
            {
                throw;
            }
            finally
            {
                this.Parameters.Clear();
            }

        }
        public MySqlDataReader findDocument(int MutabakatID)
        {
            try
            {
               
                MySqlCommand comm = new Sql().connection.CreateCommand();
                comm.CommandText = "GetDocument";
                comm.CommandType = CommandType.StoredProcedure;
                this.InputParameterAdd("@MutabakatID", MutabakatID);
                this.AddtoQueryParameters(comm);
                MySqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Close();
                reader.Dispose();
                return reader;
            }
            catch
            {
                throw;
            }
            finally
            {
                this.Parameters.Clear();
               
            }

        }


    }
}

