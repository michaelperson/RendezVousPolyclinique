using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDB.DAL
{
    public class DBConnect : IDBConnect
    {
        private string Cnstr;
        private SqlConnection oConn;
        private SqlCommand oCmd;


        public DBConnect(string Cnstr)
        {
            this.Cnstr = Cnstr;
        }
        public bool Connect()
        {
            try
            {

                oConn = new SqlConnection(Cnstr);
                oConn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                oConn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Insert(String Query,Dictionary<string, object> parametres= null)
        {
            if (Connect())
            {
                oCmd = oConn.CreateCommand();
                oCmd.CommandText = Query;
                try
                {
                    if(parametres!=null)
                    {
                        foreach (KeyValuePair<string,object> item in parametres)
                        {
                            oCmd.Parameters.AddWithValue(item.Key, item.Value);
                        }
                    }
                    oCmd.ExecuteNonQuery();
                    Disconnect();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public List<Dictionary<string, object>> getData(string Query)
        {
            if (Connect())
            {
                SqlDataReader oDr;
                oCmd = oConn.CreateCommand();
                oCmd.CommandText = Query;  //Query = "Select * from...."
                List<Dictionary<string, object>> Enregistrements = new List<Dictionary<string, object>>();
                try
                {
                    oDr = oCmd.ExecuteReader();
                    while (oDr.Read())
                    {
                        Dictionary<string, object> Row = new Dictionary<string, object>();
                        for (int i = 0; i < oDr.FieldCount; i++)
                        {
                            string Key = oDr.GetName(i);
                            object Value = oDr[i];
                            Row.Add(Key, Value);

                        }
                        Enregistrements.Add(Row);


                    }
                    oDr.Close();
                    Disconnect();
                }
                catch (Exception)
                {

                    //throw;
                }


                return Enregistrements;
            }
            return null;
        }
    }
}