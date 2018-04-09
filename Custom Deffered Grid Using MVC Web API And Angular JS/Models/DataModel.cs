using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
namespace Custom_Deffered_Grid_Using_MVC_Web_API_And_Angular_JS.Models
{
    public class DataModel
    {
        public string fetchData(int pageOffset)
        {
            string connection = ConfigurationManager.ConnectionStrings["TrialsDBEntities"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("usp_Get_SalesOrderDetailPage", cn);
                cmd.Parameters.Add("@pageoffset", SqlDbType.Int).Value = pageOffset;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cn.Open();                    
                    da.Fill(dt);
                    return GetJson(dt);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        public string GetJson(DataTable dt)
        {
            try
            {
                if (dt == null)
                {
                    throw new ArgumentNullException("dt");
                }
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows =
                  new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName.Trim(), dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}