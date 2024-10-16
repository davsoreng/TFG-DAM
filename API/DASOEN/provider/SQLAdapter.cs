using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Http;

namespace DASOEN.provider
{
    public class SQLAdapter
    {
        public string IdEmpresa = ConfigurationManager.ConnectionStrings["Empresa"].ConnectionString;
        public SqlConnection conn;
        public string abreConexion(String ConnectionType = "Ges")
        {
            try
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionType].ConnectionString;
                conn.Open();
                return "Bien";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public static void RemoveTimezoneForDataSet(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (object.ReferenceEquals(dc.DataType, typeof(DateTime)))
                    {
                        dc.DateTimeMode = DataSetDateTime.Unspecified;
                    }
                }
            }
        }
        public byte[] TraeImagenes(String SQL, String[] Parameters, String[] Values, String ConnectionType = "Ges")
        {
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            for (int i = 0; i < Parameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + Parameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = Values[i].ToString();
                cmd.Parameters.Add(parameter);
                if (i == 0)
                {
                    SQL = SQL + " where " + Parameters[i].ToString() + "=" + "@" + Parameters[i].ToString();
                }
                else
                {
                    SQL = SQL + " and " + Parameters[i].ToString() + "=" + "@" + Parameters[i].ToString();
                }
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dsDatos = new DataSet();
                adapter.Fill(dsDatos);
                RemoveTimezoneForDataSet(dsDatos);
                //Image rImage = Base64ToImage((byte[])dsDatos.Tables[0].Rows[0].ItemArray[0]);
                return ((byte[])dsDatos.Tables[0].Rows[0].ItemArray[0]); 
                    //rImage;
            }
            catch (Exception ex)
            {
                sMensajeError = "DETALLE ERROR:" + ex.Message + " SQL EJECUTADA: " + SQL;
                return null;
            }
        }
        public Image Base64ToImage(byte[] imageBytes)
        {
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        public String TraeValores(String SQL, String[] Parameters, String[] Values, String ConnectionType = "Ges")
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter writer = new JsonTextWriter(sw);
            try
            {
                abreConexion(ConnectionType);
            }
            catch(Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            for (int i = 0; i < Parameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + Parameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = Values[i].ToString();
                cmd.Parameters.Add(parameter);
                if (i == 0)
                {
                    SQL = SQL + " where " + Parameters[i].ToString() + "=" + "@" + Parameters[i].ToString();
                }
                else
                {
                    SQL = SQL + " and " + Parameters[i].ToString() + "=" + "@" + Parameters[i].ToString();
                }
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dsDatos = new DataSet();
                adapter.Fill(dsDatos);
                RemoveTimezoneForDataSet(dsDatos);
                string rjson = DataTableToJson(dsDatos.Tables[0]);
                return rjson.ToString();
            }
            catch (Exception ex)
            {
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("DETALLE ERROR");
                writer.WriteValue(ex.Message);
                writer.WritePropertyName("SQL EJECUTADA");
                writer.WriteValue(SQL);
                writer.WriteEndObject();
                writer.WriteEndArray();
                return sb.ToString();
            }
        }

        public String TraeValoresSimple(String SQL, String ConnectionType = "Ges")
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter writer = new JsonTextWriter(sw);
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dsDatos = new DataSet();
                adapter.Fill(dsDatos);
                RemoveTimezoneForDataSet(dsDatos);
                string rjson = DataTableToJson(dsDatos.Tables[0]);
                return rjson.ToString();
            }
            catch (Exception ex)
            {
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("DETALLE ERROR");
                writer.WriteValue(ex.Message);
                writer.WritePropertyName("SQL EJECUTADA");
                writer.WriteValue(SQL);
                writer.WriteEndObject();
                writer.WriteEndArray();
                return sb.ToString();
            }
        }

        public string EjecutaProcAlm(string NombreProcedimiento, String[] SetParameters, String[] SetValues, String ConnectionType = "Ges") {
            String SQL = NombreProcedimiento;
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            for (int i = 0; i < SetParameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + SetParameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = SetValues[i].ToString();
                cmd.Parameters.Add(parameter);
            }
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                int NumberOfRows = cmd.ExecuteNonQuery();
                int response = 0;
                if (NumberOfRows > 0) { response = 1; };
                JObject oJson = new JObject(new JProperty("response", response));
                string json = oJson.ToString();
                return json;
            }
            catch (Exception ex)
            {
                sMensajeError = "DETALLE ERROR:" + ex.Message + " SQL EJECUTADA: " + SQL;
                return sMensajeError;
            }
        }
        public string TraerProcAlm(string NombreProcedimiento, String[] SetParameters, String[] SetValues, String ConnectionType = "Ges")
        {
            String SQL = NombreProcedimiento;
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            for (int i = 0; i < SetParameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + SetParameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = SetValues[i].ToString();
                cmd.Parameters.Add(parameter);
            }
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dsDatos = new DataSet();
                adapter.Fill(dsDatos);
                RemoveTimezoneForDataSet(dsDatos);
                string rjson = DataTableToJson(dsDatos.Tables[0]);
                return rjson.ToString();
            }
            catch (Exception ex)
            {
                sMensajeError = "DETALLE ERROR:" + ex.Message + " SQL EJECUTADA: " + SQL;
                return sMensajeError;
            }
        }

        public string TraerProcAlmGen(string NombreProcedimiento, String[] SetParameters, String[] SetValues, String ConnectionType = "Gen")
        {
            String SQL = NombreProcedimiento;
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            for (int i = 0; i < SetParameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + SetParameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = SetValues[i].ToString();
                cmd.Parameters.Add(parameter);
            }
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dsDatos = new DataSet();
                adapter.Fill(dsDatos);
                RemoveTimezoneForDataSet(dsDatos);
                string rjson = DataTableToJson(dsDatos.Tables[0]);
                return rjson.ToString();
            }
            catch (Exception ex)
            {
                sMensajeError = "DETALLE ERROR:" + ex.Message + " SQL EJECUTADA: " + SQL;
                return sMensajeError;
            }
        }

        public String ModificaValores(String Table, String[] SetParameters, String[] SetValues, String[] WhereParameters, String[] WhereValues, String ConnectionType = "Ges")
        {
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            String sMensajeError = "";
            String SQL = "";
            SqlCommand cmd = new SqlCommand(SQL, conn);
            SQL = "Update " + Table + " ";
            for (int i = 0; i < SetParameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + SetParameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = SetValues[i].ToString();
                cmd.Parameters.Add(parameter);
                if (i == 0)
                {
                    SQL = SQL + " SET " + SetParameters[i].ToString() + "=" + "@" + SetParameters[i].ToString();
                }
                else
                {
                    SQL = SQL + " , " + SetParameters[i].ToString() + "=" + "@" + SetParameters[i].ToString();
                }
            }
            
            for (int i = 0; i < WhereParameters.Length; i++)//comenzamos con los parametros
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + WhereParameters[i].ToString();
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = WhereValues[i].ToString();
                cmd.Parameters.Add(parameter);
                if (i == 0)
                {
                    SQL = SQL + " where " + WhereParameters[i].ToString() + "=" + "@" + WhereParameters[i].ToString();
                }
                else
                {
                    SQL = SQL + " and " + WhereParameters[i].ToString() + "=" + "@" + WhereParameters[i].ToString();
                }
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                int NumberOfRows = cmd.ExecuteNonQuery();
                int response = 0;
                if (NumberOfRows > 0) { response = 1; };
                JObject oJson = new JObject( new JProperty("response", response));
                string json = oJson.ToString();
                return json;
            }
            catch (Exception ex)
            {
                sMensajeError = "DETALLE ERROR:" + ex.Message + " SQL EJECUTADA: " + SQL;
                return sMensajeError;
            }
        }
        private static string DataTableToJson2(DataTable dataTable)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var rows = (from DataRow d in dataTable.Rows
                        select dataTable.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => d[col])).ToList();

            //rows.AddRange(from DataRow d in dataTable.Rows
            //select dataTable.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => d[col]));
            return serializer.Serialize(rows).ToString();//.Replace("[","").Replace("]", "");
        }
        private static string DataTableToJson3(DataTable dataTable)
        {
            JObject TempJson = new JObject();
            for(int i= 0; i<dataTable.Rows.Count; i++)
            {
                for (int i2 = 0; i2 < dataTable.Columns.Count; i2++)
                {
                    TempJson.Add(dataTable.Columns[i2].ColumnName.ToString(), dataTable.Rows[i].ItemArray[i2].ToString());
                }
            }
            return TempJson.ToString();
        }
        public string DataTableToJson4(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                JObject oJson = new JObject(new JProperty("response", "NO DATOS"));
                return oJson.ToString();
            }
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append('"' + ds.Tables[0].Columns[j].ColumnName.ToString().Replace("\"", "-inch-") + '"'+':' + '"' + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "-inch-") + '"' + ',');
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append('"' + ds.Tables[0].Columns[j].ColumnName.ToString().Replace("\"", "-inch-") + '"' + ':' + '"' + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "-inch-") + '"');
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
               JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        //=====================================================
        //mod por soriano
        //=====================================================
        public String DataTableToJson(DataTable dt)
        {          
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter writer = new JsonTextWriter(sw);
            if (dt.Rows.Count == 0)
            {
                //JObject oJson = new JObject(new JProperty("response", "NO DATOS"));
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("response");
                writer.WriteValue("NO DATOS");
                writer.WriteEndObject();
                writer.WriteEndArray();
                return sb.ToString();
            }
            DataSet ds = new DataSet();
            ds.Merge(dt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                writer.WriteStartArray();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    writer.WriteStartObject();

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count)
                        {
                            if(ds.Tables[0].Rows[i][j].GetType().Name == "Byte[]")
                            {
                                writer.WritePropertyName(ds.Tables[0].Columns[j].ColumnName.ToString());
                                writer.WriteValue(ds.Tables[0].Rows[i][j]);
                            }
                            else
                            {
                                writer.WritePropertyName(ds.Tables[0].Columns[j].ColumnName.ToString());
                                writer.WriteValue(ds.Tables[0].Rows[i][j].ToString());
                            }
                        }
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();

                return sb.ToString();
            }
            else
            {
                return null;
            }
        }
        public String TraeValoresSimpleImg(String SQL, String ConnectionType = "Ges")
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter writer = new JsonTextWriter(sw);
            try
            {
                abreConexion(ConnectionType);
            }
            catch (Exception Ex)
            {
            }
            SqlCommand cmd = new SqlCommand(SQL, conn);
            String sMensajeError = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = SQL;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dsDatos = new DataSet();
                adapter.Fill(dsDatos);
                RemoveTimezoneForDataSet(dsDatos);
                String rjson = DataTableToJson(dsDatos.Tables[0]);
                return rjson;
            }
            catch (Exception ex)
            {
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("DETALLE ERROR");
                writer.WriteValue(ex.Message);
                writer.WritePropertyName("SQL EJECUTADA");
                writer.WriteValue(SQL);
                writer.WriteEndObject();
                writer.WriteEndArray();
                return sb.ToString();
            }
        }
    }
}
