using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Web;
namespace FourQT.DAL
{
    public abstract class DBHelper
    {
        public static readonly string CONN_STRING = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        //private DbProviderFactory _factory;

        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static int ExecuteNonQuery(String Key, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(string Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static async Task<int> ExecuteNonQueryAsyncNew(string Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, out object retVal, string outParamName, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(string Key, CommandType cmdType, string cmdText, out object retVal, string outParamName, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, out object retVal, string outParamName, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(string Key, CommandType cmdType, string cmdText, out object retVal, string outParamName, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }





        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReaderKey(string Key, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetClientConnectionString(Key));

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }



        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReaderKey(string Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetClientConnectionString(Key));

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }



        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch {
                throw;
            }
        }

        public static object ExecuteScalarkey(string Key, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch
            {
                throw;
            }
        }

        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalarKey(string Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }



        public static DataSet GetDataset(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        /// <summary>
        /// From here Mkey appeared
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet GetDataset(String Key, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }


        public static DataSet GetDataset(CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static DataSet GetDataset(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static async Task<DataSet> GetDatasetAsyncNew(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    IDataReader reader = await cmd.ExecuteReaderAsync();
                    while (!reader.IsClosed)
                    {
                        ds.Tables.Add().Load(reader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public static async Task<int> ExecuteNoQueryAsyncNew(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    result = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public static DataSet GetCachedDataset(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }

        public static DataSet GetCachedDataset(String Key, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }



        public static DataSet GetCachedDataset(CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }


        public static DataSet GetCachedDataset(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }


        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }





        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> lstParam)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (lstParam != null)
            {
                if (lstParam.Count > 0)
                {
                    foreach (SqlParameter parm in lstParam)
                        cmd.Parameters.Add(parm);
                }
            }
        }


        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms, int commandTimeOut)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = commandTimeOut;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> lstParam, int commandTimeOut)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = commandTimeOut;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (lstParam != null)
            {
                if (lstParam.Count > 0)
                {
                    foreach (SqlParameter parm in lstParam)
                        cmd.Parameters.Add(parm);
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(String Key, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static int ExecuteNonQuery(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, out object retVal, string outParamName, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(String Key, CommandType cmdType, string cmdText, out object retVal, string outParamName, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }



        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, out object retVal, string outParamName, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static int ExecuteNonQuery(String Key, CommandType cmdType, string cmdText, out object retVal, string outParamName, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                int val = cmd.ExecuteNonQuery();
                retVal = cmd.Parameters[outParamName].Value;
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms, commandTimeOut);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }





        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms, commandTimeOut);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReaderKey(string Key, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetClientConnectionString(Key));

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static SqlDataReader ExecuteReaderKey(string Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(GetClientConnectionString(Key));

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }


        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalarKey(string Key, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }



        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }


        public static object ExecuteScalarKey(string Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static DataSet GetDataset(CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static DataSet GetDataset(String Key, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static DataSet GetDataset(CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static DataSet GetDataset(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }



        public static DataSet GetCachedDataset(CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }

        public static DataSet GetCachedDataset(String Key, CommandType cmdType, string cmdText, int commandTimeOut, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }

        public static DataSet GetCachedDataset(CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(CONN_STRING))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }


        public static DataSet GetCachedDataset(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms, int commandTimeOut)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms, commandTimeOut);

                // SqlCacheDependency dependency = new SqlCacheDependency(cmd);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();

                //Cache obj = new Cache();
                //obj.Insert("DS", ds, dependency);

                return ds;
            }
        }

        public static string GetClientConnectionString(string mKey)
        {
            string connectionString = "";
            XDocument xdoc = XDocument.Load("keys.xml");

            var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == mKey).FirstOrDefault();
            //var check = xdoc.Elements("connections").Elements("connection").Elements("dkey").Where(x => (string)x.Value == mKey).FirstOrDefault();
            if (check != null)
            {
                //connectionString = check.Element("connectionString").Value;
                connectionString = check.Elements("connectionString").FirstOrDefault().Value;
            }
            return connectionString;
        }

        public static string GetClientConnectionString(string mKey, bool isPortal = false)
        {
            string connectionString = "";
            XDocument xdoc = XDocument.Load("keys.xml");

            var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == mKey).FirstOrDefault();

            if (check != null)
            {
                if (isPortal)
                {
                    connectionString = check.Elements("connectionStringPortal").FirstOrDefault().Value;
                }
                else
                {
                    connectionString = check.Elements("connectionString").FirstOrDefault().Value;
                }                
            }
            return connectionString;
        }

        public static DataSet GetDatasetGeneral(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key,true)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static string GetEmployeeConnectionString(string dKey)
        {
            string connectionString = "";
            XDocument xdoc = XDocument.Load("keys.xml");

            var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == dKey).FirstOrDefault();

            if (check != null)
            {
                var conn= check.Elements("connectionStringEmployee").FirstOrDefault().Value;
                if (conn != null) { connectionString = conn.ToString(); }                
            }
            return connectionString;
        }

        public static DataSet GetDatasetEmployee(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(GetEmployeeConnectionString(dKey)))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static string ExecuteNonQueryEmployee(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            string status = "0";
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(GetEmployeeConnectionString(dKey)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters.Contains("@Status"))
                    {
                        var param = cmd.Parameters["@Status"].Value;
                        if (param != null)
                        {
                            status = param.ToString();
                        }
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return status;
        }

        public static string GetCPConnectionString(string dKey)
        {
            try
            {
                string connectionString = "";
                XDocument xdoc = XDocument.Load("keys.xml");

                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == dKey).FirstOrDefault();
                string? conn = null;

                if (check != null)
                {
                    conn = check.Elements("connectionStringChannelPartner").FirstOrDefault().Value;
                    connectionString = (conn != null ? conn : "");
                }
                return connectionString;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static async Task<string[]> ExecuteNonQueryCP(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            string status = "0";
            string outMsg = "";
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(GetCPConnectionString(dKey)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    await cmd.ExecuteNonQueryAsync();

                    string? temp = null;
                    if (cmd.Parameters.Contains("@Status"))
                    {
                        temp = cmd.Parameters["@Status"].Value.ToString();
                        if (temp != null)
                        {
                            status = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@OutMsg"))
                    {
                        temp = cmd.Parameters["@OutMsg"].Value.ToString();
                        if (temp != null)
                        {
                            outMsg = temp.ToString();
                        }
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new string[] { status, outMsg };
        }

        public static async Task<dynamic> GetDataSetCP(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            string status = "0";
            string outMsg = "";
            int totalRecords = 0;
            DataSet ds=new DataSet();
            
            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(GetCPConnectionString(dKey)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    IDataReader reader =  await cmd.ExecuteReaderAsync();
                    while (!reader.IsClosed)
                    {
                        ds.Tables.Add().Load(reader);
                    }

                    string? temp = null;
                    if (cmd.Parameters.Contains("@Status"))
                    {
                        temp = cmd.Parameters["@Status"].Value.ToString();
                        if (temp != null)
                        {
                            status = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@OutMsg"))
                    {
                        temp = cmd.Parameters["@OutMsg"].Value.ToString();
                        if (temp != null)
                        {
                            outMsg = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@TotalRecords"))
                    {
                        temp = cmd.Parameters["@TotalRecords"].Value.ToString();
                        if (temp != null)
                        {
                            string total = temp.ToString();
                            Int32.TryParse(total, out totalRecords);
                        }
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new dynamic[] { ds , status, outMsg , totalRecords};
        }

        public static async Task<DataSet> GetDatasetGeneralASync(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try {
                using (SqlConnection conn = new SqlConnection(GetClientConnectionString(Key, true)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    IDataReader reader = await cmd.ExecuteReaderAsync();
                    while (!reader.IsClosed)
                    {
                        ds.Tables.Add().Load(reader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public static async Task<string[]> ExecuteNonQueryEmployeeAsync(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            string status = "0";
            string outMsg = "";

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(GetEmployeeConnectionString(dKey)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    await cmd.ExecuteNonQueryAsync();

                    string? temp = null;
                    if (cmd.Parameters.Contains("@Status"))
                    {
                        temp = cmd.Parameters["@Status"].Value.ToString();
                        if (temp != null)
                        {
                            status = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@OutMsg"))
                    {
                        temp = cmd.Parameters["@OutMsg"].Value.ToString();
                        if (temp != null)
                        {
                            outMsg = temp.ToString();
                        }
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new string[] { status, outMsg };
        }

        public static async Task<dynamic> GetDataSetEmployeeAsync(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            string status = "0";
            string outMsg = "";
            DataSet ds = new DataSet();
            int totalRecords = 0;

            try
            {
                SqlCommand cmd = new SqlCommand();
                using (SqlConnection conn = new SqlConnection(GetEmployeeConnectionString(dKey)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    IDataReader reader = await cmd.ExecuteReaderAsync();
                    while (!reader.IsClosed)
                    {
                        ds.Tables.Add().Load(reader);
                    }

                    string? temp = null;
                    if (cmd.Parameters.Contains("@Status"))
                    {
                        temp = cmd.Parameters["@Status"].Value.ToString();
                        if (temp != null)
                        {
                            status = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@OutMsg"))
                    {
                        temp = cmd.Parameters["@OutMsg"].Value.ToString();
                        if (temp != null)
                        {
                            outMsg = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@TotalRecords"))
                    {
                        temp = cmd.Parameters["@TotalRecords"].Value.ToString();
                        if (temp != null)
                        {
                            string total = temp.ToString();
                            Int32.TryParse(total, out totalRecords);
                        }
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new dynamic[] { ds, status, outMsg,totalRecords };
        }



        public static string GetClientConnectionStringHR(string mKey)
        {
            string connectionString = "";
            XDocument xdoc = XDocument.Load("keys.xml");

            var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == mKey).FirstOrDefault();
           
            if (check != null)
            {
                connectionString = check.Elements("connectionStringHR").FirstOrDefault().Value;
            }
            return connectionString;
        }

        public static async Task<DataSet> GetDatasetAsyncHR(String Key, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetClientConnectionStringHR(Key)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    IDataReader reader = await cmd.ExecuteReaderAsync();
                    while (!reader.IsClosed)
                    {
                        ds.Tables.Add().Load(reader);
                    }
                }
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public static async Task<string[]> ExecuteNonQueryAsyncHR(String dKey, CommandType cmdType, string cmdText, List<SqlParameter> cmdParms)
        {
            string status = "0";
            string outMsg = "";

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(GetClientConnectionStringHR(dKey)))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);

                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    await cmd.ExecuteNonQueryAsync();

                    string? temp = null;
                    if (cmd.Parameters.Contains("@Status"))
                    {
                        temp = cmd.Parameters["@Status"].Value.ToString();
                        if (temp != null)
                        {
                            status = temp.ToString();
                        }
                    }

                    if (cmd.Parameters.Contains("@MessageStatus"))
                    {
                        temp = cmd.Parameters["@MessageStatus"].Value.ToString();
                        if (temp != null)
                        {
                            outMsg = temp.ToString();
                        }
                    }

                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return new string[] { status, outMsg };
        }
    } 
}

